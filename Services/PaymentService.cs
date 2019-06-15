using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;

namespace Services
{
    public class PaymentService
    {
        private static HttpClient client = new HttpClient();

        public static string sendPaymentRequstAsync(string amount, string merchant_id, string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            amount += "00";
            string order_id = String.Format("test_{0}", Guid.NewGuid().ToString());
            string signature = generateSignature(amount, merchant_id, "test payment", order_id);
            var values = new Dictionary<string, string>
            {
                { "order_id", order_id },
                { "order_desc", "test payment" },
                {"currency", "RUB" },
                {"amount", amount },
                {"signature", signature },
                {"merchant_id", merchant_id }
            };

            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync(url, content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        private static string generateSignature(string amount, string merchant_id, string order_desc, string order_id)
        {
            string signature = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                "test",
                amount,
                "RUB",
                merchant_id,
                order_desc,
                order_id);
            signature = Hash(signature);
            return signature;
        }

        private static string Hash(string input)
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }
    }
}
