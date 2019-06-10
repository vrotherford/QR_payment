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
        //private static HttpClient client = new HttpClient();

        public static string sendPaymentRequstAsync(string amount, string merchant_id, string url)
        {
            //client.Timeout = TimeSpan.FromMinutes(1);
            string signature = generateSignature(amount, merchant_id, "test payment", "test1");
            var values = new Dictionary<string, string>
            {
                { "order_id", "test1" },
                { "order_desc", "test payment" },
                {"currency", "RUB" },
                {"amount", amount },
                {"signature", signature },
                {"merchant_id", merchant_id }
            };

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Timeout = 4000; //ms
            var itemToSend = JsonConvert.SerializeObject(values);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(itemToSend);
                streamWriter.Flush();
                streamWriter.Dispose();
            }

            // Send the request to the server and wait for the response:  
            using (var response = request.GetResponse())
            {
                // Get a stream representation of the HTTP web response:  
                using (var stream = response.GetResponseStream())
                {
                    var reader = new StreamReader(stream);
                    var message = reader.ReadToEnd();
                    return message;
                }
            }

            //var content = new FormUrlEncodedContent(values);
            //var response = client.PostAsync(url, content).Result;
            //return await response.Content.ReadAsStringAsync();
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
