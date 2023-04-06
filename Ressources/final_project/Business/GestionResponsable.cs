using final_project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace final_project.Business
{
    public class GestionResponsable : IResponsable
    {
        private SqlConnection connection;
        private Responsable responsable;

        public GestionResponsable(SqlConnection sqlConnection)
        {
            this.connection = sqlConnection;
        }

        public Responsable Responsable { get => responsable; set => responsable = value; }

        public bool AcceptBesoin(int id)
        {
            throw new NotImplementedException();
        }

        public bool AddAppelOffre()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO appeloffre (pris) VALUES(0)", connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT TOP 1 * FROM appeloffre ORDER BY id DESC", connection);
                SqlDataReader reading = cmd.ExecuteReader();
                int idAppelOffre = 0;
                while (reading.Read())
                {
                    idAppelOffre = reading.GetInt32(0);
                }
                reading.Close();

                cmd = new SqlCommand("UPDATE materielle SET appeloffre = " + idAppelOffre + " WHERE etat like 'Accepté'", connection);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("UPDATE materielle SET etat = 'En cours' WHERE appeloffre = " + idAppelOffre, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddDepartement(Departement d)
        {
            //if (d.Nom.Contains("'"))
            //{
            //    int index = d.Nom.IndexOf((char)39);
            //    d.Nom = d.Nom.Insert(index, "'");

            //}
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO departement(nom, budget) VALUES('" + d.Nom + "','" + d.Budget + "')", connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AddResponsable(Responsable r)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO responsable VALUES('" + r.Nom + "','" + r.Email +
                "','" + r.Password + "')", connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Responsable CheckResponsable(Responsable res)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM responsable WHERE email like '" + res.Email +
                    "' and password like '" + res.Password + "'", connection);
                SqlDataReader reading = cmd.ExecuteReader();
                Responsable r = new Responsable();
                while (reading.Read())
                {
                    if (reading.GetString(2) == res.Email)
                    {
                        r.Id = reading.GetInt32(0);
                        r.Nom = reading.GetString(1);
                        r.Email = reading.GetString(2);
                        r.Password = reading.GetString(3);
                    }

                }
                reading.Close();
                if (r.Id > 0)
                {
                    return r;
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

        
        public bool DeleteDepartement(int id)
        {
            throw new NotImplementedException();
        }

        public List<AppelOffre> GetAppelsOffreAcceptes()
        {
            List<AppelOffre> listAO = new List<AppelOffre>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT appeloffre.id, appeloffre.pris, fournisseur.id, fournisseur.nom FROM appeloffre INNER JOIN fournisseur ON appeloffre.id = fournisseur.id", connection);
                SqlDataReader reading = cmd.ExecuteReader();
                while (reading.Read())
                {
                    AppelOffre ao = new AppelOffre();
                    Fournisseur f = new Fournisseur();
                    ao.Id = reading.GetInt32(0);
                    if (reading.GetInt32(1) == 0)
                    {
                        ao.Pris = false;
                    }
                    else
                    {
                        ao.Pris = true;
                    }
                    f.Id = reading.GetInt32(2);
                    f.Nom = reading.GetString(3);
                    ao.Fournisseur = f;
                    listAO.Add(ao);
                }
                reading.Close();

                return listAO;
            }
            catch (Exception)
            {
                return listAO;
            }
        }
        public List<AppelOffre> GetAppelsOffre()
        {
            List<AppelOffre> listAO = new List<AppelOffre>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM appeloffre WHERE etat not like 'Livré'", connection);
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


        public List<Imprimante> GetBesoinsI()
        {
            List<Imprimante> listI = new List<Imprimante>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  imprimante.id, marque, vitesse, resolution, etat, enseignant.id, enseignant.nom FROM imprimante INNER JOIN materielle ON materielle.id = imprimante.id INNER JOIN enseignant ON materielle.enseignant = enseignant.id", connection);
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

        public List<Ordinateur> GetBesoinsO()
        {
            List<Ordinateur> listO = new List<Ordinateur>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  ordinateur.id, marque, cpu, ram, disquedur, ecran, etat, enseignant.id, enseignant.nom FROM ordinateur INNER JOIN materielle ON materielle.id = ordinateur.id INNER JOIN enseignant ON materielle.enseignant = enseignant.id", connection);
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

        public List<Departement> GetDepartements()
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            this.Responsable = null;
        }

        public bool RefuseBesoin(int id)
        {
            throw new NotImplementedException();
        }

        public int getIndex(string str, string c)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c[0])
                {
                    return i;
                }
            }
            return -1;
        }

        public void AcceptorRefuseBesoin(int id, string T)
        {
            SqlCommand cmd = new SqlCommand("SELECT etat from materielle WHERE id = " + id, connection);
            SqlDataReader reading = cmd.ExecuteReader();

            int test = 0;

            while (reading.Read())
            {
                if (reading.GetString(0) == "Accepté")
                {
                    test = 1;
                }
                else if (reading.GetString(0) == "Refusé")
                {
                    test = 2;
                }
                else if (reading.GetString(0) == "En attente")
                {
                    test = 3;
                }

            }
            reading.Close();

            if (test == 1 && T == "r")
            {
                SqlCommand cmd2 = new SqlCommand("UPDATE materielle SET etat = 'Refusé' WHERE id = " + id, connection);
                cmd2.ExecuteNonQuery();
            }
            else if (test == 2 && T == "a")
            {
                SqlCommand cmd2 = new SqlCommand("UPDATE materielle SET etat = 'Accepté' WHERE id = " + id, connection);
                cmd2.ExecuteNonQuery();
            }
            else if (test == 3)
            {
                if (T == "r")
                {
                    SqlCommand cmd2 = new SqlCommand("UPDATE materielle SET etat = 'Refusé' WHERE id = " + id, connection);
                    cmd2.ExecuteNonQuery();
                }
                else if (T == "a")
                {
                    SqlCommand cmd2 = new SqlCommand("UPDATE materielle SET etat = 'Accepté' WHERE id = " + id, connection);
                    cmd2.ExecuteNonQuery();
                }
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

        public List<ContreOffre> GetContresOffre(string keyword)
        {
            List<ContreOffre> listCO = new List<ContreOffre>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT contreoffre.id, contreoffre.montant, contreoffre.statut, fournisseur.id, fournisseur.nom FROM contreoffre INNER JOIN fournisseur ON contreoffre.fournisseur = fournisseur.id AND contreoffre.appeloffre = " + keyword, connection);
                SqlDataReader reading = cmd.ExecuteReader();
                while (reading.Read())
                {
                    ContreOffre co = new ContreOffre();
                    Fournisseur f = new Fournisseur();
                    co.Id = reading.GetInt32(0);
                    co.Montant = Double.Parse(reading.GetString(1));
                    co.Statut = reading.GetString(2);
                    f.Id = reading.GetInt32(3);
                    f.Nom = reading.GetString(4);
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

        public bool AcceptAppelOffre(int idAO, int idF, int idCO)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE contreoffre SET statut = 'Accepté' WHERE id = " + idCO, connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("UPDATE appeloffre SET pris = 1, fournisseur = " + idF + " WHERE id = " + idAO, connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("UPDATE materielle SET fournisseur = " + idF + " WHERE appeloffre = " + idAO, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RefuseAppelOffre(int idCO)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE contreoffre SET statut = 'Refusé' WHERE id = " + idCO, connection);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool LivredBesoins(int idAO)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE materielle SET etat = 'Livré' WHERE appeloffre = " + idAO, connection);
                cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Ordinateur> GetBesoinsLivresO()
        {
            List<Ordinateur> listO = new List<Ordinateur>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  ordinateur.id, marque, cpu, ram, disquedur, ecran, etat, enseignant.id, enseignant.nom FROM ordinateur INNER JOIN materielle ON materielle.id = ordinateur.id INNER JOIN enseignant ON materielle.enseignant = enseignant.id AND materielle.etat like 'Livré'", connection);
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

        public List<Imprimante> GetBesoinsLivresI()
        {
            List<Imprimante> listI = new List<Imprimante>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  imprimante.id, marque, vitesse, resolution, etat, enseignant.id, enseignant.nom FROM imprimante INNER JOIN materielle ON materielle.id = imprimante.id INNER JOIN enseignant ON materielle.enseignant = enseignant.id AND materielle.etat like 'Livré'", connection);
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

        public bool AffectBesoin(Materielle m)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE materielle SET etat = 'Affecté' , code = '" + m.Code + "' , datelivraison = '" + m.Datelivraison + "' , dureegarantie = '" + m.Dureegarantie + "' WHERE id = " + m.Id, connection);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Ordinateur> GetBesoinsAffectesO()
        {
            List<Ordinateur> listO = new List<Ordinateur>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  ordinateur.id, marque, cpu, ram, disquedur, ecran, etat, enseignant.id, enseignant.nom, materielle.code, materielle.datelivraison, materielle.dureegarantie FROM ordinateur INNER JOIN materielle ON materielle.id = ordinateur.id INNER JOIN enseignant ON materielle.enseignant = enseignant.id AND materielle.etat like 'Affecté'", connection);
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

        public List<Imprimante> GetBesoinsAffectesI()
        {
            List<Imprimante> listI = new List<Imprimante>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT  imprimante.id, marque, vitesse, resolution, etat, enseignant.id, enseignant.nom, materielle.code, materielle.datelivraison, materielle.dureegarantie FROM imprimante INNER JOIN materielle ON materielle.id = imprimante.id INNER JOIN enseignant ON materielle.enseignant = enseignant.id AND materielle.etat like 'Affecté'", connection);
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

        public List<Panne> GetPanne()
        {
            List<Panne> listP = new List<Panne>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT panne.id, explication, dateApparition, frequence,ordre, panne.materielle, enseignant.nom, materielle.dureegarantie, constat, decision  FROM panne INNER JOIN enseignant ON panne.enseignant = enseignant.id INNER JOIN materielle ON materielle.id = panne.materielle", connection);
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
                    p.Enseignant.Nom = reading.GetString(6);
                    p.Materielle.Dureegarantie = reading.GetString(7);
                    p.Constat = reading.GetString(8);
                    p.Decision = reading.GetString(9);
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

        public bool TakeDecision(Panne p)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE panne SET decision = '" + p.Decision + "' WHERE id = " + p.Id , connection);
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