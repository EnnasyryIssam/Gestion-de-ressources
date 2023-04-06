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
    public class ResponsableController : Controller
    {
        public static Boolean isConnected = false;
        public static DbConnection dbConnection = new DbConnection();
        public static GestionResponsable gr = new GestionResponsable(dbConnection.Connection);


        // GET: Enseignants
        public ActionResult Index()
        {
            if (isConnected)
            {
                return View(gr.Responsable);
            }
            else
            {
                return Redirect("~/Responsable/Login");

            }
        }

        public ActionResult Logout()
        {
            gr.LogOut();
            isConnected = false;
            return Redirect("/Responsable/");
        }

        // GET: Responsable/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Responsable/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Responsable/Register
        [HttpPost]
        public ActionResult Register(FormCollection collection, Responsable model)
        {
            if (gr.AddResponsable(model))
            {
                return Redirect("/Responsable/Login");
            }
            else
            {
                return View();
            }
        }

        // GET: Responsable/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Responsable/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Responsable/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Responsable/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Responsable/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Responsable/Login
        [HttpPost]
        public ActionResult Login(FormCollection collection, Responsable model)
        {
            Responsable r = gr.CheckResponsable(model);
            if (r != null)
            {
                gr.Responsable = r;
                isConnected = true;
                return Redirect("/Responsable/");
            }
            else
            {
                return View();
            }

        }
        // GET: Responsable/ListerBesoinsImprimante
        public ActionResult ListerBesoinsImprimante()
        {
            ViewData["listI"] = gr.GetBesoinsI();
            return View();
        }

        [HttpPost]
        public ActionResult ListerBesoinsImprimante(int id, string T, FormCollection collection)
        {
            gr.AcceptorRefuseBesoin(id, T);
            ListerBesoinsImprimante();
            return View();
        }


        // GET: Responsable/ListerBesoinsOrdinateur
        public ActionResult ListerBesoinsOrdinateur()
        {
            ViewData["listO"] = gr.GetBesoinsO();
            return View();
        }

        [HttpPost]
        public ActionResult ListerBesoinsOrdinateur(int id, string T, FormCollection collection)
        {
            gr.AcceptorRefuseBesoin(id, T);
            ListerBesoinsOrdinateur();
            return View();
        }

        public ActionResult AffecterRessourceOrdinateur()
        {
            return View();
        }
        public ActionResult AffecterRessourceImprimante()
        {
            return View();
        }
        public ActionResult ConsulterOrdinateursEnPanne()
        {
            return View();
        }
        public ActionResult ConsulterImprimantesEnPanne()
        {
            return View();
        }

        // GET: Responsable/ConsulterMaterielles
        public ActionResult ConsulterMaterielles()
        {
            ViewData["listOL"] = gr.GetBesoinsAffectesO();
            ViewData["listIL"] = gr.GetBesoinsAffectesI();
            return View();
        }

        // GET: Responsable/ConsulterMateriellesLivres
        public ActionResult ConsulterMateriellesLivres()
        {
            ViewData["listOL"] = gr.GetBesoinsLivresO();
            ViewData["listIL"] = gr.GetBesoinsLivresI();
            return View();
        }

        // POST: Responsable/ConsulterMateriellesLivres
        [HttpPost]
        public ActionResult ConsulterMateriellesLivres(FormCollection collection, Materielle model)
        {
            gr.AffectBesoin(model);
            return Redirect("/Responsable/ConsulterMateriellesLivres");
        }


        // GET: Responsable/ConsulterAppelsOffre
        public ActionResult ConsulterAppelsOffre()
        {
            ViewData["listAO"] = gr.GetAppelsOffre();
            return View();
        }

        // POST: Responsable/ConsulterAppelsOffre
        [HttpPost]
        public ActionResult ConsulterAppelsOffre(int Id, int IdF, int IdCO, FormCollection collection)
        {
            if (Id == -1 && IdF == -1)
            {
                gr.RefuseAppelOffre(IdCO);
            }
            else if (Id != -1 && IdF == -1 && IdCO == -1)
            {
                gr.LivredBesoins(Id);
            }
            else
            {
                gr.AcceptAppelOffre(Id, IdF, IdCO);
            }       
            return Redirect("/Responsable/ConsulterAppelsOffre");
        }

        // GET: Responsable/AjouterAppelOffre
        public ActionResult AjouterAppelOffre()
        {
            ViewData["listO"] = gr.GetBesoinsO();
            ViewData["listI"] = gr.GetBesoinsI();
            return View();
        }

        // POST: Responsable/AjouterAppelOffre
        [HttpPost]
        public ActionResult AjouterAppelOffre(FormCollection collection)
        {
            gr.AddAppelOffre();
            return View();
        }


        // GET: Responsable/AjouterDepartement
        public ActionResult AjouterDepartement()
        {
            return View();
        }

        // GET: Responsable/AjouterDepartement
        [HttpPost]
        public ActionResult AjouterDepartement(FormCollection collection, Departement model)
        {
            if (gr.AddDepartement(model))
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

        public ActionResult GetPanne()
        {
            ViewData["listP"] = gr.GetPanne();
            return View();
        }

        // POST: Enseignant/DeclarerPanne
        [HttpPost]
        public ActionResult GetPanne(FormCollection collection, Panne model)
        {
            //Redirect("/Technicien/ListerPannes");
            gr.TakeDecision(model);
            return Redirect("/Responsable/GetPanne");
        }
    }
}
