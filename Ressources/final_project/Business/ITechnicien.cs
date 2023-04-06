using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final_project.Models;


namespace final_project.Business
{
    public interface ITechnicien
    {
        bool AddTechnicien(Technicien t);
        Technicien CheckTechnicien(Technicien t);
        void AddConstat(Panne p);
        List<Panne> GetPannes();
        void LogOut();
    }
}