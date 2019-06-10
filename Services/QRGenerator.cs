using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class QRGenerator
    {
        public static string Generate(string Url, string path, string id)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            var targetLocation = path;
            var uniqueFileName = string.Format("{0}_{1}{2}",
                "QR_CODE",
                id,
                ".png");
            try
            {
                var Newpath = Path.Combine(targetLocation, uniqueFileName);
                qrCodeImage.Save(Newpath);
                targetLocation = "/Content/Files/" + uniqueFileName;
            }
            catch
            {
                
            }
            return targetLocation;
        }
    }
}
