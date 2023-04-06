using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using final_project.DB;
using final_project.Business;
using final_project.Models;

namespace final_project.Controllers
{
    public class EnseignantController : Controller
    {
        public static Boolean isConnected = false;
        public static DbConnection dbConnection = new DbConnection();
        public static GestionEnseignant ge = new GestionEnseignant(dbConnection.Connection);


        // GET: Enseignants
        public ActionResult Index()
        {
            if (isConnected)
            {
                return View(ge.Enseignant);
            }
            else
            {
                return Redirect("~/Enseignant/Login");
                 
            }
        }

        // GET: Enseignants/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Enseignants/Register
        public ActionResult Register()
        {
            ViewData["listD"] = ge.GetDepartements();
            return View();
        }

        // POST: Enseignants/Register
        [HttpPost]
        public ActionResult Register(FormCollection collection, Enseignant model)
        {
            if (ge.AddEnseignant(model))
            {
                return Redirect("/Enseignant/Login");
            }
            else
            {
                return View();
            }
            
        }

        // GET: Enseignant/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Enseignant/Login
        [HttpPost]
        public ActionResult Login(FormCollection collection, Enseignant model)
        {
            Enseignant e = ge.CheckEnseignant(model);
            if (e != null)
            {
                ge.Enseignant = e;
                isConnected = true;
                return Redirect("/Enseignant/");
            }
            else
            {
                return View();
            }
            
        }

        // GET: Enseignant/Logout
        public ActionResult Logout()
        {
            ge.LogOut();
            isConnected = false;
            return Redirect("/Enseignant/");
        }

        // GET: Enseignant/AjouterBesoinOrdinateur
        public ActionResult AjouterBesoinOrdinateur()
        {
            return View();
        }

        // POST: Enseignant/AjouterBesoinOrdinateur
        [HttpPost]
        public ActionResult AjouterBesoinOrdinateur(FormCollection collection, Ordinateur model)
        {
            if (ge.AddBesoin(model))
            {
                ViewBag.Error = false;
                return View();
            }
            else
            {
                ViewBag.Error = true;
                return View();
            }
        }


        public ActionResult AjouterBesoinImprimante()
        {
            return View();
        }

        // POST: Enseignant/AjouterBesoinImprimante
        [HttpPost]
        public ActionResult AjouterBesoinImprimante(FormCollection collection, Imprimante model)
        {
            if (ge.AddBesoin(model))
            {
                ViewBag.Error = false;
                return View();
            }
            else
            {
                ViewBag.Error = true;
                return View();
            }
        }

        // GET: Enseignant/ListerBesoinsImprimante
        public ActionResult ListerBesoinsImprimante()
        {
            ViewData["listI"] = ge.GetBesoinsI();
            return View();
        }


        // GET: Enseignant/ListerBesoinsOrdinateur
        public ActionResult ListerBesoinsOrdinateur()
        {
            ViewData["listO"] = ge.GetBesoinsO();
            return View();
        }
        [HttpPost]
        public ActionResult ListerBesoinsImprimante(int id, FormCollection collection)
        {
            ge.DeleteBesoinI(id);
            return Redirect("/Enseignant/ListerBesoinsImprimante");
        }

        [HttpPost]
        public ActionResult ListerBesoinsOrdinateur(int id, FormCollection collection)
        {
            ge.DeleteBesoinO(id);
            return Redirect("/Enseignant/ListerBesoinsOrdinateur");
        }
        // GET: Enseignant/DeclarerPanne
        public ActionResult DeclarerPanne()
        {
            ViewData["listOL"] = ge.GetBesoinsAffectesO();
            ViewData["listIL"] = ge.GetBesoinsAffectesI();
            return View();
        }

        // POST: Enseignant/DeclarerPanne
        [HttpPost]
        public ActionResult DeclarerPanne(FormCollection collection, Panne p)
        {
            ge.ReportPanne(p);
            return Redirect("/Enseignant/DeclarerPanne");
        }

    }
}
