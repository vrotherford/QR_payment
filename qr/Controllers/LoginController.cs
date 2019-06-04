using Core;
using Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace qr.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Clients client, string ReturnUrl)
        {
            UsersRepository userRepository = new UsersRepository();
            var id = userRepository.checkUser(client.Email, client.Pass);
            if (id != null)
            {
                FormsAuthentication.SetAuthCookie(id.ToString(), true);
                return Redirect(ReturnUrl);
            }
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Clients client)
        {
            UsersRepository userRepository = new UsersRepository();
            userRepository.addUser(client);
            return View();
        }
    }
}