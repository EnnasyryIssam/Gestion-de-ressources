using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class AppelOffre
    {
        private int id;
        private Boolean pris;
        private string etat;
        private Fournisseur fournisseur;
        private List<Ordinateur> listO;
        private List<Imprimante> listI;
        private List<ContreOffre> listCO;
        public AppelOffre()
        {
            Fournisseur = new Fournisseur();
        }

        public bool Pris { get => pris; set => pris = value; }
        public Fournisseur Fournisseur { get => fournisseur; set => fournisseur = value; }
        public int Id { get => id; set => id = value; }
        public List<Ordinateur> ListO { get => listO; set => listO = value; }
        public List<Imprimante> ListI { get => listI; set => listI = value; }
        public List<ContreOffre> ListCO { get => listCO; set => listCO = value; }
    }
}