﻿using Server_2._0.Models;

namespace Server_2._0.Services
{
    public class UserService : IUserService
    {
        private static List<UserModel> users = new List<UserModel>
        {
            new UserModel(),
            new UserModel { Id = "1", Username = "Sam"}
        };

        // Implementare metoda ADD USERS
        public async Task<ServiceResponse<List<UserModel>>> AddUser(UserModel newUser)
        {
            /*  creem un obiect 'serviceResponse' si setam data corespunzator
                astfel va deveni noul 'ServiceResponse' cu o LISTA de USERI */
            var serviceResponse = new ServiceResponse<List<UserModel>>();
            //  Adaugam un UserModel NOU
            users.Add(newUser);
            //  ne setam 'Data' din 'serviceResponse' catre lista de 'users'
            serviceResponse.Data = users;
            //  returnam 'serviceResponse'-ul nostru
            return serviceResponse;
        }

        // Implementare metoda GET ALL USERS
        public async Task<ServiceResponse<List<UserModel>>> GetAllUsers()
        {
            /*  creem un obiect 'serviceResponse' si setam data corespunzator
                astfel va deveni noul 'ServiceResponse' cu o LISTA de USERI */
            var serviceResponse = new ServiceResponse<List<UserModel>>();
            //  ne setam 'Data' din 'serviceResponse' catre lista de 'users'
            serviceResponse.Data = users;
            //  returnam 'serviceResponse'-ul nostru
            return serviceResponse;
        }

        // Implementare metoda GET USERS BY ID
        public async Task<ServiceResponse<UserModel>> GetUserById(string id)
        {
                /*  creem un obiect 'serviceResponse' si setam data corespunzator
                    astfel va deveni noul 'ServiceResponse' cu un singur UserModel,
                    !! (de data asta nu mai avem lista ca in celelalte metode) */
                var serviceResponse = new ServiceResponse<UserModel>();
                //  'Data' din 'serviceResponse' este rezultatul a ce este mai jos
                //      lamda expresion [u => u.Id == id] -> u: UserModel
                //      'u' = UserModel,  '=>' = unde,  'u.Id == id' = Id-ul userului sa fie egal cu id
                //      'FirstOrDefault' + lambda expresion => functia cauta in fiecare UserModel
                //  si returneaza userul cu acelasi id ca cel furnizat
                serviceResponse.Data = users.FirstOrDefault(u => u.Id == id);
                //  returnam 'serviceResponse'-ul nostru
                return serviceResponse;
           
        }


    }
}
