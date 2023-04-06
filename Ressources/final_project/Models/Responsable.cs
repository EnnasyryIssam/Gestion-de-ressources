using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class Responsable
    {
        private int id;
        private string nom;
        private string email;
        private string password;

        public Responsable()
        {

        }
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
    }
}