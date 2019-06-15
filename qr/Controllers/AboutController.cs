using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;

namespace qr.Controllers
{
    public class AboutController : Controller
    {
        private UsersRepository clientsRepoitory = new UsersRepository();
        // GET: About
        public ActionResult Index(string userID)
        {
            var client = clientsRepoitory.GetUser(new Guid(userID));
            return PartialView(client);
        }
    }
}