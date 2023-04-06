using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class Technicien
    {
        private int id;
        private string nom;
        private string email;
        private string password;

        public Technicien()
        {

        }

        public string Nom { get => nom; set => nom = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public int Id { get => id; set => id = value; }
    }
}