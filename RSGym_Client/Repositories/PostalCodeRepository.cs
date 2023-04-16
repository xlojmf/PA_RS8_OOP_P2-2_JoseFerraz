using RSGym_Client.Interfaces;
using RSGym_Client.IO;
using RSGym_Dal.DBContext;
using RSGym_Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Client.Repositories
{
    public class PostalCodeRepository : IPostalCodeRepository
    {

        public static List<PostalCode> ListAllPostalCodes()
        {
            using (var context = new RSGymContext())
            {
                return context.PostalCode.OrderBy(p => p.PostalCodeValue).ToList();
            }
        }

        public static bool PostalCodeExists(string postalCodeValue)
        {
            using (var context = new RSGymContext())
            {
                return context.PostalCode.Any(p => p.PostalCodeValue == postalCodeValue);
            }
        }

        public static PostalCode CreatePostalCode(string postalCodeValue)
        {
            //string postalCodeValue = UserIO.ReadPostalCode();
            string locality = UserIO.ReadLocality();

            var newPostalCode = new PostalCode
            {
                PostalCodeValue = postalCodeValue,
                Locality = locality
            };

            using (var context = new RSGymContext())
            {
                context.PostalCode.Add(newPostalCode);
                context.SaveChanges();
            }
            return newPostalCode;
        }
    }

}
