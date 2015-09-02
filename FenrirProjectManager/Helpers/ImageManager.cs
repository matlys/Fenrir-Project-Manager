using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FenrirProjectManager.Helpers
{
    public static class ImageManager
    {
        public static byte[] GetByteArray(Bitmap bitmap)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
        }

        public static Bitmap GetBitmap(byte[] array)
        {
            Bitmap bitmap;
            using (var ms = new MemoryStream(array))
            {
                bitmap = new Bitmap(ms);
            }
            return bitmap;
        }


    }
}
