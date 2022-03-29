using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Services.UserService{
    public class UserService : IUserService
    {
        private static List<User> users = new List<User>
        {
            new User(),
            new User { Id = "1", Username = "Sam"}
        };

        // Implementare metoda ADD USERS
        public async Task<ServiceResponse<List<User>>> AddUser(User newUser)
        {
            /*  creem un obiect 'serviceResponse' si setam data corespunzator
                astfel va deveni noul 'ServiceResponse' cu o LISTA de USERI */
            var serviceResponse = new  ServiceResponse<List<User>>();
            //  Adaugam un USER NOU
            users.Add(newUser);
            //  ne setam 'Data' din 'serviceResponse' catre lista de 'users'
            serviceResponse.Data = users;
            //  returnam 'serviceResponse'-ul nostru
            return serviceResponse;
        }

        // Implementare metoda GET ALL USERS
        public async Task<ServiceResponse<List<User>>> GetAllUsers()
        {
            /*  creem un obiect 'serviceResponse' si setam data corespunzator
                astfel va deveni noul 'ServiceResponse' cu o LISTA de USERI */
            var serviceResponse = new  ServiceResponse<List<User>>();
            //  ne setam 'Data' din 'serviceResponse' catre lista de 'users'
            serviceResponse.Data = users;
            //  returnam 'serviceResponse'-ul nostru
            return serviceResponse;
        }

        // Implementare metoda GET USERS BY ID
        public async Task<ServiceResponse<User>> GetUserById(string id)
        {
            /*  creem un obiect 'serviceResponse' si setam data corespunzator
                astfel va deveni noul 'ServiceResponse' cu un singur User,
                !! (de data asta nu mai avem lista ca in celelalte metode) */
            var serviceResponse = new ServiceResponse<User>();
            //  'Data' din 'serviceResponse' este rezultatul a ce este mai jos
                //      lamda expresion [u => u.Id == id] -> u: user
                //      'u' = user,  '=>' = unde,  'u.Id == id' = Id-ul userului sa fie egal cu id
                //      'FirstOrDefault' + lambda expresion => functia cauta in fiecare user
                //  si returneaza userul cu acelasi id ca cel furnizat
            serviceResponse.Data = users.FirstOrDefault(u => u.Id == id);
            //  returnam 'serviceResponse'-ul nostru
            return serviceResponse;
        }

        
    }
}