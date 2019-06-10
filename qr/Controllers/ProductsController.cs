using Core;
using Data;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace qr.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        private BasicContext db = new BasicContext();
        // GET: Organizations
        public ActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var queryParams = Request.QueryString;
            var id = queryParams["organizationId"];
            var result = DataSourceLoader.Load(db.products.Where(p => p.OrganizationId.ToString() == id), loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        public ActionResult GetForGroup(DataSourceLoadOptions loadOptions)
        {
            var queryParams = Request.QueryString;
            var id = queryParams["GroupId"];
            var result = DataSourceLoader.Load(db.products.Where(p => p.GroupId.ToString() == id), loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        public ActionResult Insert(string values)
        {
            var filePath = Session["currentFilePath"].ToString();
            Session["currentFilePath"] = "";
            var newProduct= new Products();                             // Create a new item
            JsonConvert.PopulateObject(values, newProduct);           // Populate the item with the values

            newProduct.QRCode = QRGenerator.Generate(Url.Action("Product", 
                "Products", 
                new { id = newProduct.Id.ToString() }), 
                Server.MapPath("~/Content/Files/"), 
                newProduct.Id.ToString());

            newProduct.Photo = filePath;
            if (!TryValidateModel(newProduct))                        // Validate the item
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Error");
            db.products.Add(newProduct);                            // Add the item to the database
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // Update an item in the "Orders" collection
        public ActionResult Update(Guid key, string values)
        {
            var filePath = Session["currentFilePath"].ToString();
            var product = db.products.First(o => o.Id == key); // Find the item to be updated by key
            if(!String.IsNullOrWhiteSpace(filePath))
            {
                product.Photo = filePath;
                Session["currentFilePath"] = "";
            }
            JsonConvert.PopulateObject(values, product);              // Populate the found item with the changed values
            if (!TryValidateModel(product))                           // Validate the updated item
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Error");
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.Created);
        }

        // Remove an item from the "Orders" collection
        public void Delete(Guid key)
        {
            var product = db.products.First(o => o.Id == key); // Find the item to be removed by key
            db.products.Remove(product);                            // Remove the found item
            db.SaveChanges();
        }

        [Authorize]
        public ActionResult Product(string id)
        {
            var product = db.products.FirstOrDefault(p => p.Id == new Guid(id));

            return View(product);
        }
    }
}