using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class Besoin
    {
        private int id;
        private AppelOffre appeloffre;
        private Materielle materielle;

        public Besoin()
        {

        }
        public int Id { get => id; set => id = value; }
        public AppelOffre Appeloffre { get => appeloffre; set => appeloffre = value; }
        public Materielle Materielle { get => materielle; set => materielle = value; }

    }
}