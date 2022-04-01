
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Server_2._0.Data;
using Server_2._0.Models;
using Server_2._0.Hashing;

namespace Server_2._0.Repository
{
    public class AuthRepository:IAuthRepository
    {
        private readonly IMongoCollection<UserModel> _users;
        private readonly IJwtToken _jwtToken;

        // constructor => contine numele clasei si creeaza obiecte
        public AuthRepository(IDbContext DataContext, IJwtToken JwtToken)
        {
            _users = DataContext.GetUserCollection();
            _jwtToken = JwtToken;
        }
        public async Task<ServiceResponse<string>> Login(string Username, string Password)
        {
            var response = new ServiceResponse<string>();
            var user = await _users.AsQueryable().FirstOrDefaultAsync(x => x.Username == Username);
            if (user==null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!HashingAlgorithms.VerifyHash(Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            else
            {
                response.Data = _jwtToken.CreateToken(user);
                response.Success = true;
                response.Message = "You have successfully logged in!";
            }
            return response;
        }

        public async Task<ServiceResponse<string>> Register(UserModel User ,string Password)
        {
            var response = new ServiceResponse<string>();

            if (await IsUserDuplicateAsync(User.Username))
            {
                // daca exista, atunci nu putem adauga => returneaza raspuns negativ
                response.Success = false;
                response.Errors.Add("Username already taken");
                return response;
            }

            //verificam daca exista utilizator cu acelasi Email inregistrat
            if (await IsEmailDuplicateAsync(User.Email))
            {
                // daca exista, atunci nu putem adauga => returneaza raspuns negativ
                response.Success = false;
                response.Errors.Add("Email already in use");
                return response;
            }


            // creem hash-ul si salt-ul parolei
            HashingAlgorithms.CreateHash(Password, out byte[] PasswordHash, out byte[] PasswordSalt);
            User.PasswordHash = PasswordHash;
            User.PasswordSalt = PasswordSalt;

            //verificam daca exista utilizator cu acelasi Username deja

            try
            {
                //inseram utilizatorul in tabela USER
                await _users.InsertOneAsync(User);
                response.Data = User.Id;
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Data = ex.ToString();
                response.Success = false;
            }
                   
            return response;

        }

      

        // METODA -> verificam daca exista un user cu acelasi username
        public async Task<bool> IsUserDuplicateAsync(string Username) => await _users.AsQueryable()
                .AnyAsync(x => x.Username.ToLower() == Username.ToLower());  // checks for duplicates in the user table 
        
        // METODA -> verificam daca email-ul a mai fost folosit
        public async Task<bool> IsEmailDuplicateAsync(string Email) => await _users.AsQueryable()
                .AnyAsync(x => x.Email.ToLower() == Email.ToLower()); //Checks for duplicates in Email 

      
    }


  
   

}
