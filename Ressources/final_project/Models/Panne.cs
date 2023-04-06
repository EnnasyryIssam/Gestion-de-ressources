using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class Panne
    {
        private int id;
        private string explication;
        private string dateApparition;
        private string frequence;
        private string ordre;
        private string constat;
        private string decision;
        private Materielle materielle;
        private Enseignant enseignant;

        public Panne()
        {
            Materielle = new Materielle();
            Enseignant = new Enseignant();
        }

        public int Id { get => id; set => id = value; }
        public string Explication { get => explication; set => explication = value; }
        public string DateApparition { get => dateApparition; set => dateApparition = value; }
        public string Frequence { get => frequence; set => frequence = value; }
        public string Ordre { get => ordre; set => ordre = value; }
        public string Constat { get => constat; set => constat = value; }
        public string Decision { get => decision; set => decision = value; }
        public Materielle Materielle { get => materielle; set => materielle = value; }
        public Enseignant Enseignant { get => enseignant; set => enseignant = value; }
    }
}