﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProjekt.Models;
using Owin;
using System.Security.Claims;
using BlogProjekt.KlasyPomocnicze;
using System.Web.Security;

namespace BlogProjekt.Controllers
{
    [AllowAnonymous]
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            if(ModelState.IsValid)
            {
                user.dataAktualizacji = DateTime.Now;
                user.dataStworzenia = DateTime.Now;
                user.isAdmin = false;

                ObsługaBazyDanych.dodajNowegoUzytkownika(user);

                return RedirectToAction("Index", "Main");
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.Email==null || model.Password == null)
                {
                    ModelState.AddModelError("", "Pole email i haslo nie mogą być puste!");
                    return View();
                }
                return View();
            }
            if (model.Email.Equals("") || model.Password.Equals(""))
            {
                ModelState.AddModelError("", "Pole email i haslo nie mogą być puste!");
                return View();
            }

            var user = ObsługaBazyDanych.sprawdzCzyUzytkownikIsniteje(model.Email, model.Password);
            if(!(user is null))
            {
                FormsAuthentication.SetAuthCookie(user.username, false);
                return RedirectToAction("Index", "Main");
            }

            ModelState.AddModelError("", "Niepoprawny email lub hasło");
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Main");
        }
    }
}