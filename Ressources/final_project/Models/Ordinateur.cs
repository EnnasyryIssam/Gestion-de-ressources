using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class Ordinateur : Materielle
    {
        private string marque;
        private string cpu;
        private string ram;
        private string disquedur;
        private string ecran;


        public Ordinateur()
        {
        }

        public Ordinateur(string marque, string cpu, string ram, string disquedur, string ecran, Enseignant enseignant)
        {
            this.Marque = marque;
            this.Cpu = cpu;
            this.Ram = ram;
            this.Disquedur = disquedur;
            this.Ecran = ecran;
        }

        public string Marque { get => marque; set => marque = value; }
        public string Cpu { get => cpu; set => cpu = value; }
        public string Ram { get => ram; set => ram = value; }
        public string Disquedur { get => disquedur; set => disquedur = value; }
        public string Ecran { get => ecran; set => ecran = value; }
    }
}