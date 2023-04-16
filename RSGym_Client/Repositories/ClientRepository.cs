using RSGym_Client.Interfaces;
using RSGym_Dal.DBContext;
using RSGym_Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Client.Repositories
{
    public class ClientRepository : IClientRepository
    {

        public static Client CreateClient(Client client)
        {
            using (var context = new RSGymContext())
            {
                context.Client.Add(client);
                context.SaveChanges();
                return client;
            }
        }

        public static IList<Client> GetActiveClientsOrderedByName()
        {
            using (var context = new RSGymContext())
            {
                return context.Client.Include("PostalCode")
                                      .Where(c => c.isActive)
                                      .OrderBy(c => c.FullName)
                                      .ToList();
            }
        }

        public static IList<Client> FindClientsByName(string name)
        {
            using (var context = new RSGymContext())
            {
                return context.Client.Include("PostalCode")
                    .Where(c => c.FullName.Contains(name) && c.isActive)
                    .OrderBy(c => c.FullName)
                    .ToList();
            }
        }
        public static bool NifExists(string nif)
        {
            using (var context = new RSGymContext())
            {
                return context.Client.Any(c => c.Nif == nif);
            }
        }
        public static bool ClientExists(int clientId)
        {
            using (var context = new RSGymContext())
            {
                return context.Client.Any(c => c.ClientID == clientId);
            }
        }
        //TODO Faltou verificar se o client tinha ou Requests booked ou nao
        public static void ComuteClientState(int clientId)
        {
            using (var context = new RSGymContext())
            {
                var client = context.Client.FirstOrDefault(c => c.ClientID == clientId);

                if (client != null)
                {
 
                    if (client.isActive)
                    {
                        client.isActive = false;
                    }
                    else
                    {
                        client.isActive = true;
                    }

                    context.SaveChanges();
                }
            }
        }

        public static void UpdateClient(Client client)
        {
            using (var context = new RSGymContext())
            {
                var existingClient = context.Client.FirstOrDefault(c => c.Nif == client.Nif);

                if (existingClient != null)
                {
                    // update the existing client with the new data
                    existingClient.FullName = client.FullName;
                    existingClient.BirthDate = client.BirthDate;
                    existingClient.Address = client.Address;
                    existingClient.PostalCodeID = client.PostalCodeID;
                    existingClient.Phone = client.Phone;
                    existingClient.Email = client.Email;
                    existingClient.Notes = client.Notes;

                    context.SaveChanges();
                }
            }
        }

    }
}
