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
    public class TechnicienController : Controller
    {
        public static Boolean isConnected = false;
        public static DbConnection dbConnection = new DbConnection();
        public static GestionTechnicien gt = new GestionTechnicien(dbConnection.Connection);

        // GET: Techniciens
        public ActionResult Index()
        {
            if (isConnected)
            {
                return View(gt.Technicien);
            }
            else
            {
                return Redirect("~/Technicien/Login");

            }
        }

        public ActionResult Logout()
        {
            gt.LogOut();
            isConnected = false;
            return Redirect("/Technicien/");
        }

        // GET: Techniciens/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Techniciens/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Techniciens/Edit/5
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

        // GET: Techniciens/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Techniciens/Delete/5
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

        public ActionResult ListerPannes()
        {
            ViewData["listP"] = gt.GetPannes();
            return View();
        }

        // POST: Enseignant/DeclarerPanne
        [HttpPost]
        public ActionResult ListerPannes(FormCollection collection, Panne model)
        {
            //Redirect("/Technicien/ListerPannes");
            gt.AddConstat(model);
            return Redirect("/Technicien/ListerPannes");
        }
        public ActionResult Profil()
        {
            return View();
        }
        public ActionResult ModifierProfil()
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
        public ActionResult Register(FormCollection collection, Technicien model)
        {
            if (gt.AddTechnicien(model))
            {
                return Redirect("/Technicien/Login");
            }
            else
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
        public ActionResult Login(FormCollection collection, Technicien model)
        {
            Technicien t = gt.CheckTechnicien(model);
            if (t != null)
            {
                gt.Technicien = t;
                isConnected = true;
                return Redirect("/Technicien/");
            }
            else
            {
                return View();
            }

        }
    }
}
