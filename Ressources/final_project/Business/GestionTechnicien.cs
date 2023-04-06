using final_project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace final_project.Business
{
    public class GestionTechnicien : ITechnicien
    {
        private SqlConnection connection;
        private Technicien technicien;

        public GestionTechnicien(SqlConnection sqlConnection)
        {
            this.connection = sqlConnection;
        }

        public Technicien Technicien { get => technicien; set => technicien = value; }
        public void AddConstat(Panne p)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("UPDATE panne SET constat = '" + p.Constat + "' WHERE id = " + p.Id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
        }

        public bool AddTechnicien(Technicien t)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO technicien VALUES('" + t.Nom + "','" + t.Email +
                "','" + t.Password + "')", connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Technicien CheckTechnicien(Technicien tech)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM technicien WHERE email like '" + tech.Email +
                    "' and password like '" + tech.Password + "'", connection);
                SqlDataReader reading = cmd.ExecuteReader();
                Technicien t = new Technicien();
                while (reading.Read())
                {
                    if (reading.GetString(2) == tech.Email)
                    {
                        t.Id = reading.GetInt32(0);
                        t.Nom = reading.GetString(1);
                        t.Email = reading.GetString(2);
                        t.Password = reading.GetString(3);
                    }

                }
                reading.Close();
                if (t.Id > 0)
                {
                    return t;
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

        public List<Panne> GetPannes()
        {
            List<Panne> listP = new List<Panne>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT panne.id, explication, dateApparition, frequence,ordre, materielle, enseignant.nom, constat, decision  FROM panne INNER JOIN enseignant ON panne.enseignant = enseignant.id", connection);
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
                    p.Constat = reading.GetString(7);
                    p.Decision = reading.GetString(8);
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
            this.Technicien = null;
        }
    }
}