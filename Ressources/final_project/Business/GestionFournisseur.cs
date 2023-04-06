using final_project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace final_project.Business
{
    public class GestionFournisseur : IFournisseur
    {
        private SqlConnection connection;
        private Fournisseur fournisseur;

        public GestionFournisseur(SqlConnection sqlConnection)
        {
            this.connection = sqlConnection;
        }

        public Fournisseur Fournisseur { get => fournisseur; set => fournisseur = value; }

        public bool AddContreOffre(ContreOffre co)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO contreoffre(montant, appeloffre, fournisseur) VALUES('" +
                    co.Montant + "'," + co.Appeloffre.Id + "," + Fournisseur.Id + ")", connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AddFournisseur(Fournisseur f)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO fournisseur VALUES('" + f.Nom + "','" + f.Lieu +
                "','" + f.Email +"','" + f.Gerant + "','" + f.Password + "', 0)", connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Fournisseur CheckFournisseur(Fournisseur fou)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM fournisseur WHERE email like '" + fou.Email +
                    "' and password like '" + fou.Password + "'", connection);
                SqlDataReader reading = cmd.ExecuteReader();
                Fournisseur f = new Fournisseur();
                while (reading.Read())
                {
                    if (reading.GetString(3) == fou.Email)
                    {
                        f.Id = reading.GetInt32(0);
                        f.Nom = reading.GetString(1);
                        f.Lieu = reading.GetString(2);
                        f.Email = reading.GetString(3);
                        f.Gerant = reading.GetString(4);
                        f.Password = reading.GetString(5);
                        if (reading.GetInt32(6) == 0)
                        {
                            f.Blacklist = false;
                        }
                        else
                        {
                            f.Blacklist = true;
                        }
                    }

                }
                reading.Close();
                if (f.Id > 0)
                {
                    return f;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool DeleteContreOffre(int idCO)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM contreoffre WHERE id = " + idCO, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<AppelOffre> GetAppelsOffre()
        {
            List<AppelOffre> listAO = new List<AppelOffre>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM appeloffre", connection);
                SqlDataReader reading = cmd.ExecuteReader();
                while (reading.Read())
                {
                    AppelOffre ao = new AppelOffre();
                    ao.Id = reading.GetInt32(0);
                    if (reading.GetInt32(1) == 0)
                    {
                        ao.Pris = false;
                    }
                    else
                    {
                        ao.Pris = true;
                    }
                    listAO.Add(ao);
                }
                reading.Close();

                foreach (var item in listAO)
                {
                    item.ListO = GetBesoinsO(item.Id + "");
                    item.ListI = GetBesoinsI(item.Id + "");
                    item.ListCO = GetContresOffre(item.Id + "");
                }

                return listAO;
            }
            catch (Exception)
            {
                return listAO;
            }
        }

        public List<Imprimante> GetBesoinsI(string keyword)
        {
            List<Imprimante> listI = new List<Imprimante>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT imprimante.id, marque, vitesse, resolution, etat, enseignant.id, enseignant.nom FROM imprimante INNER JOIN materielle ON materielle.id = imprimante.id INNER JOIN enseignant ON materielle.enseignant = enseignant.id AND materielle.etat = 'En cours' AND materielle.appeloffre = " + keyword, connection);
                SqlDataReader reading = cmd.ExecuteReader();
                while (reading.Read())
                {
                    Imprimante i = new Imprimante();
                    Enseignant e = new Enseignant();
                    i.Id = reading.GetInt32(0);
                    i.Marque = reading.GetString(1);
                    i.Vitesse = reading.GetString(2);
                    i.Resolution = reading.GetString(3);
                    i.Etat = reading.GetString(4);
                    e.Id = reading.GetInt32(5);
                    e.Nom = reading.GetString(6);
                    i.Enseignant = e;
                    listI.Add(i);
                }
                reading.Close();
                return listI;
            }
            catch (Exception)
            {
                return listI;
            }
        }

        public List<Ordinateur> GetBesoinsO(string keyword)
        {
            List<Ordinateur> listO = new List<Ordinateur>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  ordinateur.id, marque, cpu, ram, disquedur, ecran, etat, enseignant.id, enseignant.nom FROM ordinateur INNER JOIN materielle ON materielle.id = ordinateur.id INNER JOIN enseignant ON materielle.enseignant = enseignant.id AND materielle.etat = 'En cours' AND materielle.appeloffre = " + keyword, connection);
                SqlDataReader reading = cmd.ExecuteReader();
                while (reading.Read())
                {
                    Ordinateur o = new Ordinateur();
                    Enseignant e = new Enseignant();
                    o.Id = reading.GetInt32(0);
                    o.Marque = reading.GetString(1);
                    o.Cpu = reading.GetString(2);
                    o.Ram = reading.GetString(3);
                    o.Disquedur = reading.GetString(4);
                    o.Ecran = reading.GetString(5);
                    o.Etat = reading.GetString(6);
                    e.Id = reading.GetInt32(7);
                    e.Nom = reading.GetString(8);
                    o.Enseignant = e;
                    listO.Add(o);
                }
                reading.Close();
                return listO;
            }
            catch (Exception)
            {
                return listO;
            }
        }

        public List<ContreOffre> GetContresOffre(string keyword)
        {
            List<ContreOffre> listCO = new List<ContreOffre>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT id, montant, statut FROM contreoffre WHERE fournisseur = " + Fournisseur.Id + " AND appeloffre = " + keyword, connection);
                SqlDataReader reading = cmd.ExecuteReader();
                while (reading.Read())
                {
                    ContreOffre co = new ContreOffre();
                    Fournisseur f = new Fournisseur();
                    co.Id = reading.GetInt32(0);
                    co.Montant = Double.Parse(reading.GetString(1));
                    co.Statut = reading.GetString(2);
                    f.Id = Fournisseur.Id;
                    f.Nom = Fournisseur.Nom;
                    co.Fournisseur = f;
                    listCO.Add(co);
                }
                reading.Close();
                return listCO;
            }
            catch (Exception)
            {
                return listCO;
            }
        }

        public List<Panne> GetPannes()
        {
            List<Panne> listP = new List<Panne>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT panne.id, explication, dateApparition, frequence,ordre, materielle,  constat  FROM panne INNER JOIN enseignant ON panne.enseignant = enseignant.id INNER JOIN materielle ON materielle.id = panne.materielle WHERE panne.decision = 'A rendre' AND materielle.fournisseur = " + Fournisseur.Id, connection);
                SqlDataReader reading = cmd.ExecuteReader();
                while (reading.Read())
                {
                    Panne p = new Panne();
                    p.Id = reading.GetInt32(0);
                    p.Explication = reading.GetString(1);
                    p.DateApparition = reading.GetString(2);
                    p.Frequence = reading.GetString(3);
                    p.Ordre = reading.GetString(4);
                    p.Materielle.Id = reading.GetInt32(5);
                    p.Constat = reading.GetString(6);
                    listP.Add(p);
                }
                reading.Close();

                return listP;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public void LogOut()
        {
            this.Fournisseur = null;
        }
    }
}