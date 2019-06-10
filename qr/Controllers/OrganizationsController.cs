using Core;
using Data;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using qr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace qr.Controllers
{
    public class OrganizationsController : Controller
    {
        // GET: Organization
        private BasicContext db = new BasicContext();
        // GET: Organizations
        public ActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(db.Organizations.Where(o => o.UserId == new Guid(User.Identity.Name)), loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        public ActionResult Insert(string values)
        {
            var newOrganization = new Organizations();                             // Create a new item
            JsonConvert.PopulateObject(values, newOrganization);           // Populate the item with the values
            newOrganization.UserId = new Guid(User.Identity.Name);
            if (!TryValidateModel(newOrganization))                        // Validate the item
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Error");
            db.Organizations.Add(newOrganization);                            // Add the item to the database
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // Update an item in the "Orders" collection
        public ActionResult Update(Guid key, string values)
        {
            var organization = db.Organizations.First(o => o.Id == key); // Find the item to be updated by key
            JsonConvert.PopulateObject(values, organization);              // Populate the found item with the changed values
            if (!TryValidateModel(organization))                           // Validate the updated item
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Error");
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.Created);
        }

        // Remove an item from the "Orders" collection
        public void Delete(Guid key)
        {
            var organization = db.Organizations.First(o => o.Id == key); // Find the item to be removed by key
            db.Organizations.Remove(organization);                            // Remove the found item
            db.SaveChanges();
        }

        [Authorize]
        public ActionResult Products(string id)
        {
            return View(new Guid(id));
        }
    }
}