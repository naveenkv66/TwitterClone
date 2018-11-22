using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private PersonManager personManager = new PersonManager();

        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel objUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var person = this.personManager.Login(objUser.UserName, objUser.Password);
                    if (person != null)
                    {
                        Session["LoginModel"] = person;
                        Session["UserName"] = person.User_Id.ToString();
                        return RedirectToAction("Tweet", "Twitter");
                    }
                    else
                    {
                        AddErrors("UserName or Password is wrong..");
                    }
                }
                catch (Exception)
                {
                    AddErrors("Problem with our end. Please try again later..");
                }
            }
            return View(objUser);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Person
                {
                    User_Id = model.UserName,
                    Password = model.Password,
                    FullName = model.FullName,
                    Email = model.Email,
                    Joined = DateTime.Now,
                    Active = true
                };
                try
                {
                    var successMsg = personManager.Save(user);
                    if (successMsg == "success")
                    {
                        Session["LoginModel"] = user;
                        Session["UserName"] = user.User_Id.ToString();
                        return RedirectToAction("Tweet", "Twitter");
                    }
                    else
                    {
                        AddErrors(successMsg);
                    }
                }
                catch (Exception ex)
                {
                    AddErrors("Problem with our end. Please try again later..");
                }
            }

            return View(model);
        }

        //
        // GET: /Account/Update
        [AllowAnonymous]
        public ActionResult Update()
        {
            UpdateViewModel model = null;
            if (Session["LoginModel"] != null)
            {
                Person personModel = (Person)Session["LoginModel"];
                model = new UpdateViewModel
                {
                    UserName = personModel.User_Id,
                    Password = personModel.Password,
                    FullName = personModel.FullName,
                    Email = personModel.Email
                };

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        //
        // POST: /Account/Update
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateViewModel model, string action)
        {
            if (ModelState.IsValid)
            {
                var user = new Person
                {
                    User_Id = model.UserName,
                    Password = model.Password,
                    FullName = model.FullName,
                    Email = model.Email,
                    Active = action == "Update" ? true : false
                };
                try
                {
                    var successMsg = personManager.Update(user);
                    if (successMsg == "success")
                    {
                        Session["LoginModel"] = user;
                        Session["UserName"] = user.User_Id.ToString();
                        if (action == "Update")
                        {
                            return RedirectToAction("Tweet", "Twitter");
                        }
                        else
                        {
                            Session["LoginModel"] = null;
                            Session["UserName"] = null;
                            return RedirectToAction("Login", "Account");
                        }
                    }
                    else
                    {
                        AddErrors(successMsg);
                    }

                }
                catch (Exception)
                {
                    AddErrors("Problem with our end. Please try again later..");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session["UserName"] = null;
            Session["LoginModel"] = null;
            return RedirectToAction("Login", "Account");
        }

        private void AddErrors(string error)
        {
            ModelState.AddModelError("", error);
        }
    }
}