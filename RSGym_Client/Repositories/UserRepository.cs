using RSGym_Client.Interfaces;
using RSGym_Client.IO;
using RSGym_Dal.DBContext;
using RSGym_Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RSGym_Client.Repositories
{
    public class UserRepository : IUserRepository
    {

        public static List<User> ListAllUsersByUsername()
        {
            using (var context = new RSGymContext())
            {
                return context.User.OrderBy(u => u.UserName).ToList();
            }
        }
        public static bool ChangePassword(string username, string newPassword)
        {
            using (var context = new RSGymContext())
            {
                var user = context.User.FirstOrDefault(u => u.UserName == username);
                if (user == null)
                {
                    return false;
                }

                user.Password = newPassword;
                context.SaveChanges();
                return true;
            }
        }
        public static void CreateUser(string currentuser)
        {
            string username;
            do
            {
                username = UserIO.ReadUserName();
                if (UserExists(username))
                {
                    Console.WriteLine("Username already exists. Please choose another.");
                }
            } while (UserExists(username));
            string name = UserIO.ReadFullName();
            string password = UserIO.ReadPassword();
            Role role = UserIO.ReadRole(currentuser);

            var newUser = new User
            {
                Name = name,
                UserName = username,
                Password = password,
                Role = role
            };

            ConfirmUser(newUser);

            using (var context = new RSGymContext())
            {
                context.User.Add(newUser);
                context.SaveChanges();
            }
            UserIO.CloseOperation();
        }
        public static bool UserExists(string username)
        {
            using (var context = new RSGymContext())
            {
                var user = context.User.FirstOrDefault(u => u.UserName == username);
                return user != null;
            }

        }
        public static void ConfirmUser(User user)
        {
            Console.Clear();
            Console.WriteLine($"UserName:{user.UserName}\nName:{user.Name}\nPassword:{user.Password}\nRole:{user.Role}");
            // confirm if the user wants to create the user or not
            Console.WriteLine("Are you sure you want to create this user? (Y/N)");
            if (Console.ReadKey().Key != ConsoleKey.Y)
            {
                Console.WriteLine("\nUser creation cancelled.");
                return;
            }
        }

    }


}
