using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final_project.Models;

namespace final_project.Business
{
    interface IEnseignant
    {
        bool AddEnseignant(Enseignant e);
        Enseignant CheckEnseignant(Enseignant e);
        bool AddBesoin(Ordinateur o);
        bool AddBesoin(Imprimante i);
        bool DeleteBesoinI(int id);
        bool DeleteBesoinO(int id);
        List<Ordinateur> GetBesoinsO();
        List<Imprimante> GetBesoinsI();
        bool ReportPanne(Panne p);
        List<Departement> GetDepartements();
        List<Ordinateur> GetBesoinsAffectesO();
        List<Imprimante> GetBesoinsAffectesI();
        void LogOut();

    }
}
