using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class Imprimante : Materielle
    {
        private string marque;
        private string vitesse;
        private string resolution;

        public Imprimante()
        {
        }

        public string Marque { get => marque; set => marque = value; }
        public string Vitesse { get => vitesse; set => vitesse = value; }
        public string Resolution { get => resolution; set => resolution = value; }
    }
}