using RSGym_Client.Menus;
using RSGym_Dal.DBContext;
using RSGym_Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RSGym_Client.IO
{
    public class LogUser
    {
        //TODO IMPROVE LOGIN TO RETURN USER AND REFACTOR METHODS
        public static User Login()
        {
            int attempts = 0;
            bool loggedIn = false;

            while (!loggedIn && attempts < 5)
            {
                string username = UserIO.ReadUserName();
                string password = UserIO.ReadPassword();

                using (var context = new RSGymContext())
                {
                    var user = context.User.FirstOrDefault(u => u.UserName == username && u.Password == password);

                    if (user != null)
                    {
                        Console.Clear();
                        Console.Write("\n");
                        Menu.WriteTitle($"Welcome, {user.UserName}!");
                        Thread.Sleep(2000);
                        loggedIn = true;
                        return user;
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password. Please try again.");
                        attempts++;                       
                    }
                }
            }

            if (!loggedIn)
            {
                //Escolhi isto para evitar ataques bruteforce e spam na bd 
                Console.WriteLine("Too many attempts. Exiting application.");
                Environment.Exit(0);
            }
            return null;
        }


    }
}
