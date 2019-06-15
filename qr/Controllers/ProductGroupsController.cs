using Core;
using Data;
using Data.Repos;
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
    public class ProductGroupsController : Controller
    {

        private BasicContext db = new BasicContext();
        private ProductsRepository productsRepository = new ProductsRepository();
        // GET: Organizations
        public ActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var queryParams = Request.QueryString;
            var id = queryParams["organizationId"];
            var result = DataSourceLoader.Load(db.productGroups.Where(p => p.OrganizationId.ToString() == id), loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        public ActionResult Insert(string values)
        {
            var filePath = Session["currentFilePath"].ToString();
            Session["currentFilePath"] = "";
            var newProductGroup = new ProductGroups();                             // Create a new item
            JsonConvert.PopulateObject(values, newProductGroup);           // Populate the item with the values
            newProductGroup.Photo = filePath;
            if (!TryValidateModel(newProductGroup))                        // Validate the item
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Error");
            db.productGroups.Add(newProductGroup);                            // Add the item to the database
            db.SaveChanges();
            newProductGroup.QRCode = QRGenerator.Generate(Url.Action("ProductList",
                "ProductGroups",
                new { id = newProductGroup.Id.ToString() }, Request.Url.Scheme),
                Server.MapPath("~/Content/Files/"),
                newProductGroup.Id.ToString());
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // Update an item in the "Orders" collection
        public ActionResult Update(Guid key, string values)
        {
            var filePath = Session["currentFilePath"].ToString();
            Session["currentFilePath"] = "";
            var productGroup = db.productGroups.First(o => o.Id == key); // Find the item to be updated by key
            productGroup.Photo = filePath;
            JsonConvert.PopulateObject(values, productGroup);              // Populate the found item with the changed values
            if (!TryValidateModel(productGroup))                           // Validate the updated item
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Error");
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.Created);
        }

        // Remove an item from the "Orders" collection
        public void Delete(Guid key)
        {
            var productGroup = db.productGroups.First(o => o.Id == key); // Find the item to be removed by key
            db.productGroups.Remove(productGroup);                            // Remove the found item
            db.SaveChanges();
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ProductList(string id)
        {
            var model = productsRepository.GetByProductGroup(new Guid(id));
            return View(model);
        }
    }
}