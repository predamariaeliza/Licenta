using System.Collections.Generic;

namespace Server.Models
{
    /*  <T> reprezinta un service response, si este
        the actual type of the data we want to return
    */
    public class ServiceResponse<T>
    {
        //  proprietati:
        /*  T Data -> informatia pe care vrem sa o returnam */
        public T Data { get; set; }
        
        /*  '= true' -> setam Succes sa fie default adevarat */
        public bool Succes { get; set; } = true;
        
        /*      trimite un mesaj catre frontend, de genul: 
            "utilizatorul a fost creat"
                Daca apare vreo eroare, aici se va transmite 
        */
        public string Message { get; set; } = null;

        // lista de errori
        public List<string> Errors { get; set; } = new List<string>();
    }
}