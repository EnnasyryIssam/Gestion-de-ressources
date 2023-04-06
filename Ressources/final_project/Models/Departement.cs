using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class Departement
    {
        private int id;
        private string nom;
        private Double budget;

        public Departement()
        {

        }
        public Departement(string nom, Double budget)
        {
            this.Nom = nom;
            this.Budget = budget;
        }

        public string Nom { get => nom; set => nom = value; }
        public Double Budget { get => budget; set => budget = value; }
        public int Id { get => id; set => id = value; }
    }
}