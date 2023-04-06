using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class Materielle
    {
        private int id;
        private string code;
        private string datelivraison;
        private string dureegarantie;
        private string etat;
        private Boolean enpanne = false;
        private Enseignant enseignant;
        private Fournisseur fournisseur;
        private AppelOffre appelOffre;

        public Materielle()
        {
        }


        public int Id { get => id; set => id = value; }
        public string Code { get => code; set => code = value; }
        public string Datelivraison { get => datelivraison; set => datelivraison = value; }
        public string Dureegarantie { get => dureegarantie; set => dureegarantie = value; }
        public Fournisseur Fournisseur { get => fournisseur; set => fournisseur = value; }
        public string Etat { get => etat; set => etat = value; }
        public bool Enpanne { get => enpanne; set => enpanne = value; }
        public Enseignant Enseignant { get => enseignant; set => enseignant = value; }
        public AppelOffre AppelOffre { get => appelOffre; set => appelOffre = value; }
    }
}