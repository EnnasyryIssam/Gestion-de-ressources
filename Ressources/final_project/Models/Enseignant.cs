using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class Enseignant
    {
        private int id;
        private string nom;
        private string email;
        private string password;
        private string laboratoire;
        private Departement departement;
        private Boolean isChef;

        public Enseignant()
        {

        }

        public Enseignant(string nom, string email, string password, string laboratoire, Departement departement, Boolean isChef)
        {
            this.Nom = nom;
            this.Email = email;
            this.Password = password;
            this.Laboratoire = laboratoire;
            this.Departement = departement;
            this.IsChef = isChef;
        }

        public string Nom { get => nom; set => nom = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Laboratoire { get => laboratoire; set => laboratoire = value; }
        public Departement Departement { get => departement; set => departement = value; }
        public Boolean IsChef { get => isChef; set => isChef = value; }
        public int Id { get => id; set => id = value; }
    }
}