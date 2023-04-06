using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_project.Models
{
    public class ContreOffre
    {
        private int id;
        private AppelOffre appeloffre;
        private Fournisseur fournisseur;
        private double montant;
        private string statut;

        public ContreOffre()
        {
            appeloffre = new AppelOffre();
            fournisseur = new Fournisseur();
        }

        public AppelOffre Appeloffre { get => appeloffre; set => appeloffre = value; }
        public Fournisseur Fournisseur { get => fournisseur; set => fournisseur = value; }
        public double Montant { get => montant; set => montant = value; }
        public int Id { get => id; set => id = value; }
        public string Statut { get => statut; set => statut = value; }
    }
}