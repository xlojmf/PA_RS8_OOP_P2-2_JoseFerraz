using RSGym_Client.IO;
using RSGym_Dal.DBContext;
using RSGym_Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Client.SeedDB
{
    public class SeedDatabase
    {
        public static void SeedUsers()
        {
            List<User> users = new List<User>
                {
                new User { Name = "Admin User", UserName = "admin", Password = "admin123", Role = Role.Admin },
                new User { Name = "Collaborator 1", UserName = "colab", Password = "colab123", Role = Role.Colab },
                new User { Name = "Collaborator 2", UserName = "colab1", Password = "colab123", Role = Role.Colab }
                };

            using (var context = new RSGymContext())
            {
                context.User.AddRange(users);
                context.SaveChanges();
            }
        }

        public static void SeedPostalCodes()
        {
            List<PostalCode> postalcodes = new List<PostalCode>
                {
                    new PostalCode { PostalCodeValue = "4700-001", Locality = "Braga" },
                    new PostalCode { PostalCodeValue = "1000-001", Locality = "Lisboa" },
                    new PostalCode { PostalCodeValue = "4000-001", Locality = "Porto" },
                    new PostalCode { PostalCodeValue = "8000-001", Locality = "Faro" },
                    new PostalCode { PostalCodeValue = "9000-001", Locality = "Funchal" }
                };

            using (var context = new RSGymContext())
            {
                context.PostalCode.AddRange(postalcodes);
                context.SaveChanges();
            }
        }

        public static void SeedClients()
        {
            List <Client> clients = new List<Client>
                {
                    new Client { FullName = "José Ferraz", BirthDate = new DateTime(1990, 5, 1),Nif = "231351763",
                        PostalCodeID = 1, Address = "Rua de Barros",
                        Email = "jose.ferraz@gmail.com", Phone = "910000001", isActive= true },
                    new Client { FullName = "Miguel Ferraz", BirthDate = new DateTime(1985, 8, 15),Nif = "231351764",
                        PostalCodeID = 2, Address = "Travessa de Barros",
                        Email = "miguel.ferraz@yahoo.com", Phone = "910000002", isActive= true,
                        Notes = "Likes to eat more than work out" },
                    new Client { FullName = "Santos Ferraz", BirthDate = new DateTime(1975, 10, 10),Nif = "231351765",
                        PostalCodeID = 3, Address = "Avenida de Barros",
                        Email = "santos.ferraz@hotmail.com", Phone = "910000003", isActive= true,
                        Notes = "New to gym, needs guidance." }
                };

            using (var context = new RSGymContext())
            {
                context.Client.AddRange(clients);
                context.SaveChanges();
            }
        }

        public static void SeedPersonalTrainers()
        {
            List<PersonalTrainer> trainers = new List<PersonalTrainer>
                {
                    new PersonalTrainer { FullName = "Ferraz José", PersonalTrainerCode="PT01", Nif = "231351766",
                        PostalCodeID = 1, Address = "123 Main St",
                        Email = "ferraz.jose@gmail.com", Phone = "910000004"},
                    new PersonalTrainer { FullName = "Ferraz Miguel", PersonalTrainerCode="PT02",  Nif = "231351767",
                        PostalCodeID = 2, Address = "456 Elm St",
                        Email = "ferraz.miguel@yahoo.com", Phone = "910000005"}
                };

            using (var context = new RSGymContext())
            {
                context.PersonalTrainer.AddRange(trainers);
                context.SaveChanges();
            }
        }

        public static void SeedRequests()
        {
            List<Request> requests = new List<Request>
                {
                    new Request { ClientID = 1, PersonalTrainerID = 1, Booking = new DateTime(2023, 6, 15, 10, 0, 0), State = RequestState.Booked },
                    new Request { ClientID = 2, PersonalTrainerID = 2, Booking = new DateTime(2023, 6, 16, 18, 0, 0), State = RequestState.Over },
                    new Request { ClientID = 3, PersonalTrainerID = 1, Booking = new DateTime(2023, 6, 18, 14, 0, 0), State = RequestState.Booked, Notes="Cust has medical issues" }
                };

            using (var context = new RSGymContext())
            {
                context.Request.AddRange(requests);
                context.SaveChanges();
            }
        }


    }
}
