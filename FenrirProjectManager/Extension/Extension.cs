using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace FenrirProjectManager.Extension
{
    public static class Extension
    {
        public static string ToHashCode(this string password)
        {
            //APNtuBdP/L36fFRZ3ByWuHqs8CA7HSxQn86rJVlSjd/sbiprXkCa2VJJ1HIZtDQLSA==
            string hashed = Crypto.Hash(password, "MD5");
            string sha256 = Crypto.SHA256(password);
            string sha1 = Crypto.SHA1(password);
            string salt = Crypto.GenerateSalt();
            string hashedPassword = Crypto.HashPassword(password);
            return hashedPassword;
        }
    }
}
