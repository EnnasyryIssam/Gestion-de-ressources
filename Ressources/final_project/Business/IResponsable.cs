using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final_project.Models;

namespace final_project.Business
{
    interface IResponsable
    {
        bool AddResponsable(Responsable r);
        Responsable CheckResponsable(Responsable r);
        bool AddDepartement(Departement d);
        List<Departement> GetDepartements();
        bool DeleteDepartement(int id);
        bool AddAppelOffre();
        List<AppelOffre> GetAppelsOffreAcceptes();
        List<AppelOffre> GetAppelsOffre();
        void AcceptorRefuseBesoin(int id, String T);
        List<Ordinateur> GetBesoinsO();
        List<Ordinateur> GetBesoinsO(string keyword);
        List<Imprimante> GetBesoinsI();
        List<Imprimante> GetBesoinsI(string keyword);
        List<ContreOffre> GetContresOffre(string keyword);
        List<Panne> GetPanne();
        bool TakeDecision(Panne p);
        bool AcceptAppelOffre(int idAO, int idF, int idCO);
        bool RefuseAppelOffre(int idCO);
        bool LivredBesoins(int idAO);
        List<Ordinateur> GetBesoinsLivresO();
        List<Ordinateur> GetBesoinsAffectesO();
        List<Imprimante> GetBesoinsLivresI();
        List<Imprimante> GetBesoinsAffectesI();
        bool AffectBesoin(Materielle m);
        void LogOut();

    }
}
