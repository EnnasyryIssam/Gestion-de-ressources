using final_project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace final_project.Business
{
    public class GestionEnseignant : IEnseignant
    {
        private SqlConnection connection;
        private Enseignant enseignant;

        public GestionEnseignant(SqlConnection sqlConnection)
        {
            this.connection = sqlConnection;
        }

        public Enseignant Enseignant { get => enseignant; set => enseignant = value; }

        public bool AddBesoin(Ordinateur o)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO materielle (enseignant) VALUES(" + Enseignant.Id + ")", connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT TOP 1 * FROM materielle ORDER BY id DESC", connection);
                SqlDataReader reading = cmd.ExecuteReader();
                int idM = 0;

                while (reading.Read())
                {
                    idM = reading.GetInt32(0);
                }
                reading.Close();

                cmd = new SqlCommand("INSERT INTO ordinateur (id, marque, cpu, ram, disquedur, ecran) VALUES(" + idM + ",'" + o.Marque + "','" + o.Cpu +
                "','" + o.Ram + "','" + o.Disquedur + "','" + o.Ecran + "')", connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool AddBesoin(Imprimante i)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO materielle (enseignant) VALUES(" + Enseignant.Id + ")", connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT TOP 1 * FROM materielle ORDER BY id DESC", connection);
                SqlDataReader reading = cmd.ExecuteReader();
                int idM = 0;

                while (reading.Read())
                {
                    idM = reading.GetInt32(0);
                }
                reading.Close();

                cmd = new SqlCommand("INSERT INTO imprimante (id, marque, vitesse, resolution) VALUES(" + idM + ",'" + i.Marque + "','" + i.Vitesse +
                    "','" + i.Resolution + "')", connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AddEnseignant(Enseignant e)
        {
            try
            {
                int isChef = 0;
                if (e.IsChef)
                {
                    isChef = 1;
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO enseignant VALUES('" + e.Nom + "','" + e.Email +
                "','" + e.Password + "','" + e.Laboratoire + "'," + e.Departement.Id + "," + isChef + ")", connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Enseignant CheckEnseignant(Enseignant ens)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM enseignant WHERE email like '" + ens.Email +
                    "' and password like '" + ens.Password + "'", connection);
                SqlDataReader reading = cmd.ExecuteReader();
                Enseignant e = new Enseignant();
                Departement d = new Departement();
                int idDep = 0;
                while (reading.Read())
                {
                    if (reading.GetString(2) == ens.Email)
                    {
                        e.Id = reading.GetInt32(0);
                        e.Nom = reading.GetString(1);
                        e.Email = reading.GetString(2);
                        e.Password = reading.GetString(3);
                        e.Laboratoire = reading.GetString(4);
                        idDep = reading.GetInt32(5);
                        if (reading.GetInt32(6) == 0)
                        {
                            e.IsChef = false;
                        }
                        else
                        {
                            e.IsChef = true;
                        }

                    }

                }
                reading.Close();

                cmd = new SqlCommand("SELECT * FROM departement WHERE id = " + idDep, connection);
                SqlDataReader reading2 = cmd.ExecuteReader();
                while (reading2.Read())
                {
                    if (reading2.GetInt32(0) == idDep)
                    {
                        d.Id = reading2.GetInt32(0);
                        d.Nom = reading2.GetString(1);
                        d.Budget = Double.Parse(reading2.GetString(2));
                    }

                }
                reading2.Close();
                e.Departement = d;

                if (e.Id > 0)
                {
                    return e;
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

        public bool DeleteBesoinO(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM ordinateur WHERE id = " + id, connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM materielle WHERE id = " + id, connection);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteBesoinI(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM imprimante WHERE id = " + id, connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("DELETE FROM materielle WHERE id = " + id, connection);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetBesoins()
        {
            throw new NotImplementedException();
        }

        public List<Imprimante> GetBesoinsAffectesI()
        {
            List<Imprimante> listI = new List<Imprimante>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  imprimante.id, marque, vitesse, resolution, etat, enseignant.id, enseignant.nom, materielle.code, materielle.datelivraison, materielle.dureegarantie, materielle.enpanne FROM imprimante INNER JOIN materielle ON materielle.id = imprimante.id INNER JOIN enseignant ON materielle.enseignant = enseignant.id AND materielle.enseignant = " + Enseignant.Id + " AND materielle.etat like 'Affecté'", connection);
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
                    i.Code = reading.GetString(7);
                    i.Datelivraison = reading.GetString(8);
                    i.Dureegarantie = reading.GetString(9);
                    if(reading.GetInt32(10)  == 1)
                    {
                        i.Enpanne = true;
                    }
                    else
                    {
                        i.Enpanne = false;
                    }
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

        public List<Ordinateur> GetBesoinsAffectesO()
        {
            List<Ordinateur> listO = new List<Ordinateur>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  ordinateur.id, marque, cpu, ram, disquedur, ecran, etat, enseignant.id, enseignant.nom, materielle.code, materielle.datelivraison, materielle.dureegarantie, materielle.enpanne FROM ordinateur INNER JOIN materielle ON materielle.id = ordinateur.id INNER JOIN enseignant ON materielle.enseignant = enseignant.id AND  materielle.enseignant = " + Enseignant.Id + " AND materielle.etat like 'Affecté'", connection);
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
                    o.Code = reading.GetString(9);
                    o.Datelivraison = reading.GetString(10);
                    o.Dureegarantie = reading.GetString(11);
                    if (reading.GetInt32(12) == 1)
                    {
                        o.Enpanne = true;
                    }
                    else
                    {
                        o.Enpanne = false;
                    }
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

        public List<Imprimante> GetBesoinsI()
        {
            List<Imprimante> listI = new List<Imprimante>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  imprimante.id, marque, vitesse, resolution, etat FROM imprimante INNER JOIN materielle ON materielle.id = imprimante.id AND materielle.enseignant = " + Enseignant.Id, connection);
                SqlDataReader reading = cmd.ExecuteReader();
                while (reading.Read())
                {
                    Imprimante i = new Imprimante();
                    i.Id = reading.GetInt32(0);
                    i.Marque = reading.GetString(1);
                    i.Vitesse = reading.GetString(2);
                    i.Resolution = reading.GetString(3);
                    i.Etat = reading.GetString(4);

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

        public List<Ordinateur> GetBesoinsO()
        {
            List<Ordinateur> listO = new List<Ordinateur>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  ordinateur.id, marque, cpu, ram, disquedur, ecran, etat FROM ordinateur INNER JOIN materielle ON materielle.id = ordinateur.id AND materielle.enseignant = " + Enseignant.Id, connection);
                SqlDataReader reading = cmd.ExecuteReader();
                while (reading.Read())
                {
                    Ordinateur o = new Ordinateur();
                    o.Id = reading.GetInt32(0);
                    o.Marque = reading.GetString(1);
                    o.Cpu = reading.GetString(2);
                    o.Ram = reading.GetString(3);
                    o.Disquedur = reading.GetString(4);
                    o.Ecran = reading.GetString(5);
                    o.Etat = reading.GetString(6);

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

        public List<Departement> GetDepartements()
        {
            List<Departement> listD = new List<Departement>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM departement", connection);
                SqlDataReader reading = cmd.ExecuteReader();
                while (reading.Read())
                {
                    Departement d = new Departement();
                    d.Id = reading.GetInt32(0);
                    d.Nom = reading.GetString(1);
                    d.Budget = Double.Parse(reading.GetString(2));
                    listD.Add(d);
                }
                reading.Close();

                return listD;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public void LogOut()
        {
            this.Enseignant = null;
        }

        public bool ReportPanne(Panne p)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO panne(explication, dateApparition, frequence, ordre, materielle, enseignant) VALUES('" + p.Explication + "','" + p.DateApparition + "','" + p.Frequence +
                "','" + p.Ordre + "'," + p.Materielle.Id + "," + Enseignant.Id + ")", connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("UPDATE materielle SET enpanne = 1 WHERE id = " + p.Materielle.Id, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}