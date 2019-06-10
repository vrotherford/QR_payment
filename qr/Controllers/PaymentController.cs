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
        public void SendPayment(string amount, string merchant_id)
        {
            var response = PaymentService.sendPaymentRequstAsync(amount, merchant_id, ConfigurationManager.AppSettings["paymentRequstUrl"]);
            string a = String.Empty;
        }
        
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }
    }
}