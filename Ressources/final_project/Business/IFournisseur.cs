using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final_project.Models;

namespace final_project.Business
{
    interface IFournisseur
    {
        bool AddFournisseur(Fournisseur f);
        Fournisseur CheckFournisseur(Fournisseur f);
        bool AddContreOffre(ContreOffre co);
        bool DeleteContreOffre(int idCO);
        List<Ordinateur> GetBesoinsO(string keyword);
        List<Imprimante> GetBesoinsI(string keyword);
        List<ContreOffre> GetContresOffre(string keyword);
        List<Panne> GetPannes();
        void LogOut();

    }
}
