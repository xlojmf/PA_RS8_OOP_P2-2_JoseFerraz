using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSGym_Client.IO;
using RSGym_Client.Menus;
using RSGym_Client.Repositories;
using RSGym_Client.SeedDB;
using RSGym_Dal;
using RSGym_Dal.DBContext;
using RSGym_Dal.Models;

namespace RSGym_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Menu.SetUnicodeConsole();

                #region Data Seeding
                
                SeedDatabase.SeedUsers();
                SeedDatabase.SeedPostalCodes();
                SeedDatabase.SeedClients();
                SeedDatabase.SeedPersonalTrainers();
                SeedDatabase.SeedRequests();
                
                #endregion

                Menu.StartMenu();

            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }


        }
    }
}



