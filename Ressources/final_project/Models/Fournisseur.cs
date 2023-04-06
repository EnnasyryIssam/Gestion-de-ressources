using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class Fournisseur
    {
        private int id;
        private string nom;
        private string lieu;
        private string email;
        private string gerant;
        private string password;
        private bool blacklist;

        public Fournisseur()
        {

        }

        public string Nom { get => nom; set => nom = value; }
        public string Lieu { get => lieu; set => lieu = value; }
        public string Email { get => email; set => email = value; }
        public string Gerant { get => gerant; set => gerant = value; }
        public string Password { get => password; set => password = value; }
        public int Id { get => id; set => id = value; }
        public bool Blacklist { get => blacklist; set => blacklist = value; }
    }
}