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
    public class FournisseurController : Controller
    {
        static Boolean isConnected = false;
        static DbConnection dbConnection = new DbConnection();
        static GestionFournisseur gf = new GestionFournisseur(dbConnection.Connection);


        // GET: Fournisseur
        public ActionResult Index()
        {
            if (isConnected)
            {
                return View(gf.Fournisseur);
            }
            else
            {
                return Redirect("~/Fournisseur/Login");

            }
        }

        // GET: Fournisseur/ConsulterAppelsOffre
        public ActionResult ConsulterAppelsOffre()
        {
            ViewData["listAO"] = gf.GetAppelsOffre();
            return View();
        }

        // POST: Fournisseur/ConsulterAppelsOffre
        [HttpPost]
        public ActionResult ConsulterAppelsOffre(FormCollection collection, ContreOffre model, int idCO = -1)
        {
            if (idCO != -1)
            {
                gf.DeleteContreOffre(idCO);
            }
            else
            {
                gf.AddContreOffre(model);
            }
            return Redirect("/Fournisseur/ConsulterAppelsOffre");
        }


        // GET: Fournisseur/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Fournisseur/Register
        [HttpPost]
        public ActionResult Register(FormCollection collection, Fournisseur model)
        {
            if (gf.AddFournisseur(model))
            {
                return Redirect("/Fournisseur/Login");
            }
            else
            {
                return View();
            }

        }


        // GET: Fournisseur/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Fournisseur/Login
        [HttpPost]
        public ActionResult Login(FormCollection collection, Fournisseur model)
        {
            Fournisseur f = gf.CheckFournisseur(model);
            if (f != null)
            {
                gf.Fournisseur = f;
                isConnected = true;
                return Redirect("/Fournisseur/");
            }
            else
            {
                return View();
            }

        }

        public ActionResult ConsulterPannes()
        {
            ViewData["listP"] = gf.GetPannes();
            return View();
        }

        // GET: Fournisseur/Logout
        public ActionResult Logout()
        {
            gf.LogOut();
            isConnected = false;
            return Redirect("/Fournisseur/");
        }
    }
}
