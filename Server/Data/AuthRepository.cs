using System.Threading.Tasks;
using MongoDB.Driver;
using Server.Data;
using Server.Models;
using MongoDB.Driver.Linq;

namespace Server.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IMongoCollection<User> _users; 
        public AuthRepository(IDbContext Data)
        {
            _users = Data.GetUserCollection();
        }
        public Task<ServiceResponse<string>> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<string>> Register(User user, string password)
        {
            var response = new ServiceResponse<string>();

            // creem hash-ul si salt-ul parolei
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            
            //verificam daca exista utilizator cu acelasi Username deja
            if(await IsUserDuplicateAsync(user.Username))
            {
                // daca exista, atunci nu putem adauga => returneaza raspuns negativ
                response.Succes = false;
                response.Errors.Add("Username already taken");
                return response;
            }

            //verificam daca exista utilizator cu acelasi Email inregistrat
            if(await IsEmailDuplicateAsync(user.Email))
            {
                // daca exista, atunci nu putem adauga => returneaza raspuns negativ
                response.Succes = false;
                response.Errors.Add("Email already in use");
                return response;
            }

            //inseram utilizatorul in tabela USER
            await _users.InsertOneAsync(user);
            response.Data = user.Id;
            return response;
            
        }

        // metoda de a hash-ui parola
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // folosim SHA512
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // METODA -> verificam daca exista un user cu acelasi username
        // AsQueryable() - pentru a face query-uri pe users
        // AnyAsync() - daca exista field care satisface conditia (mongoDB linq)
        // => - in loc de a scrie return (lambda expression)
        // ToLower() - transforma string-ul in litere mici, pt a face comparatia

        // async -> ca sa mearga asincron -> 
        // (am un fir de executie, dar astept ca acel)
        public async Task<bool> IsUserDuplicateAsync(string Username) => await _users.AsQueryable()
                .AnyAsync(x => x.Username.ToLower() == Username.ToLower());  // checks for duplicates in the user table 
    
        // METODA -> verificam daca exista un user inregistrat cu acelasi email
        // AsQueryable() - pentru a face query-uri pe users
        // AnyAsync() - daca exista field care satisface conditia (mongoDB linq)
        // => - in loc de a scrie return (lambda expression)
        // ToLower() - transforma string-ul in litere mici, pt a face comparatia

        // async -> ca sa mearga asincron -> 
        // (am un fir de executie, dar astept ca acel)
        public async Task<bool> IsEmailDuplicateAsync(string Email) => await _users.AsQueryable()
                .AnyAsync(x => x.Email.ToLower() == Email.ToLower()); //Checks for duplicates in Email 
    }
}