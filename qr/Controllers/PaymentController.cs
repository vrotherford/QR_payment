using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;

namespace qr.Controllers
{
    public class PaymentController : Controller
    {

        [HttpPost]
        public string SendPayment(string amount, string merchant_id)
        {
            var response = PaymentService.sendPaymentRequstAsync(amount, merchant_id, ConfigurationManager.AppSettings["paymentRequstUrl"]);
            string real = System.Web.HttpUtility.UrlDecode(response);
            string checkout_url = HttpUtility.ParseQueryString(real).Get("checkout_url");
            return checkout_url;
        }
        
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }
    }
}