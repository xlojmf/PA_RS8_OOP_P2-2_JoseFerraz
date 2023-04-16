using RSGym_Client.IO;
using RSGym_Client.Repositories;
using RSGym_Dal.DBContext;
using RSGym_Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RSGym_Client.Menus
{
    public class Menu
    {
        #region Starting Menu
        public static void StartMenu()
        {
            // Declaring Menus
            string[] loginMenuOptions = {"\n1. Login", "2. Exit\n" };
            string[] mainMenuOptions = {"\n1. Manage Requests", "2. Manage Clients",
            "3. Manage Personal Trainers"};

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                WriteTitle("Welcome to the RSGym Management Dashboard");
                ShowMenuOptions(loginMenuOptions);
                int option = UserIO.GetMenuOption(2, "bash");

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        WriteTitle("User Login");
                        var user = LogUser.Login();

                        bool isLoggedIn = true;

                        while (isLoggedIn)
                        {
                            Console.Clear();
                            WriteTitle("RSGym Management Dashboard");
                            ShowMenuOptions(mainMenuOptions);

                            switch (user.Role)
                            {
                                case Role.Admin:
                                    Console.WriteLine("4. User Management");
                                    Console.WriteLine("5. Logout\n");
                                    break;
                                case Role.Colab:
                                    Console.WriteLine("5. Logout\n");
                                    break;
                                default:
                                    throw new InvalidOperationException($"Invalid user role: {user.Role}");
                            }
                            int mainOption = UserIO.GetMenuOption(5, user.UserName);

                            switch (mainOption)
                            {
                                case 1:
                                    WriteTitle("Requests Dashboard");
                                    DisplayRequestModule(user);
                                    break;

                                case 2:
                                    WriteTitle("Clients Dashboard");
                                    DisplayClientModule(user);
                                    break;

                                case 3:
                                    WriteTitle("Personal Trainers Dashboard");
                                    DisplayPersonalTrainerModule(user);
                                    break;

                                case 4:
                                    if (user.Role == Role.Colab)
                                    {
                                        Console.WriteLine("Access denied.");
                                        break;
                                    }
                                    WriteTitle("User Management Dashboard");
                                    DisplayUserModule(user);
                                    break;

                                case 5:
                                    DisplayLogout();
                                    isLoggedIn = false;
                                    break;

                                default:
                                    Console.WriteLine("Invalid option. Please try again.");
                                    break;
                            }
                        }
                        break;

                    case 2:
                        Console.WriteLine("Exiting...");
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

        }
        #endregion

        #region User Module
        private static void DisplayUserModule(User user)
        {
            bool clientsMenuExit = false;
            while (!clientsMenuExit)
            {
                Console.Clear();
                WriteTitle("Administration Menu");
                Console.WriteLine("\n1. Create New User");
                Console.WriteLine("2. Update User Password");
                Console.WriteLine("3. List all users");
                Console.WriteLine("4. Return to Main Menu\n");

                int clientsMenuChoice = UserIO.GetMenuOption(4, user.UserName);
                switch (clientsMenuChoice)
                {
                    case 1:
                        UserRepository.CreateUser(user.UserName);
                        break;
                    case 2:
                        UpdatePassword();
                        break;
                    case 3:
                        ShowListUsers();
                        break;
                    case 4:
                        clientsMenuExit = true;
                        break;
                }
            }
        }
        private static void ShowListUsers()
        {
            List<User> listUsers = UserRepository.ListAllUsersByUsername();
            foreach (User user in listUsers)
            {
                Console.WriteLine($"Username:{user.UserName} | Name:{user.Name} | Role:{user.Role}");
            }
            PressKey();
        }
        private static void UpdatePassword()
        {
            string username = UserIO.ReadUserName();
            string password = UserIO.ReadPassword();
            bool flag = UserRepository.ChangePassword(username, password);
            if (flag)
            {
                UserIO.CloseOperation();
            }
            else
            {
                Console.WriteLine("Ups something went wrong.");
            }
        }

        #endregion

        #region Client Module
        private static void DisplayClientModule(User user)
        {
            bool clientsMenuExit = false;
            while (!clientsMenuExit)
            {
                Console.Clear();
                WriteTitle("Clients Dashboard");
                Console.WriteLine("\n1. Create New Client");
                Console.WriteLine("2. Update Client");
                Console.WriteLine("3. List all Clients");
                Console.WriteLine("4. Change Client State");
                Console.WriteLine("5. Return to Main Menu\n");

                int clientsMenuChoice = UserIO.GetMenuOption(5, user.UserName);
                switch (clientsMenuChoice)
                {
                    case 1:
                        Client client = CreateClient();
                        ClientRepository.CreateClient(client);
                        UserIO.CloseOperation();
                        break;
                    case 2:
                        UpdateClient();
                        break;
                    case 3:
                        ListClients();
                        break;
                    case 4:
                        int clientId = UserIO.ReadClientId("client");
                        ClientRepository.ComuteClientState(clientId);
                        UserIO.CloseOperation();
                        break;
                    case 5:
                        clientsMenuExit = true;
                        break;
                }
            }
        }
        private static Client CreateClient()
        {
            string fullname = UserIO.ReadFullName();
            string nif = UserIO.ReadNif();
            DateTime date= UserIO.ReadBirthDate();
            string address = UserIO.ReadAddress();
            string readpostalCode = UserIO.ReadPostalCode();

            //TODO LIMPAR PARA O REPOSITORIO DE CP
            int postalCodeID;
            using (var context = new RSGymContext())
            {
                var postalCode = context.PostalCode.FirstOrDefault(p => p.PostalCodeValue == readpostalCode);
                if (postalCode == null)
                {
                    Console.WriteLine("No matching postal code found in our database, let's create one");
                    postalCode = PostalCodeRepository.CreatePostalCode(readpostalCode);
                }
                postalCodeID = postalCode.PostalCodeID;
            }

            string phone = UserIO.ReadPhoneNumber();
            string email = UserIO.ReadEmail();
            string notes = UserIO.ReadNotes();



            var client = new Client
            {
                FullName = fullname,
                Nif = nif,
                BirthDate = date,
                Address = address,
                PostalCodeID = postalCodeID,
                Phone = phone,
                Email = email,
                Notes = notes,
                isActive = true
            };

            return client;
        }
        public static void UpdateClient()
        {
            
            string name = UserIO.ReadName();

            IList<Client> clients = ClientRepository.FindClientsByName(name);
            if (clients.Count == 0)
            {
                Console.WriteLine("No clients found with the provided name.");
                return;
            }

            Console.WriteLine("Found the following clients:");
            foreach (Client client in clients)
            {
                Console.WriteLine($"Name:{client.FullName} - NIF:({client.Nif})");
            }

            Client selectedClient = null;
            do
            {
                string nif = UserIO.ReadNifUpdate();
                selectedClient = clients.FirstOrDefault(c => c.Nif == nif);
                if (selectedClient == null)
                {
                    Console.WriteLine("Invalid NIF, please try again.");
                }
            } while (selectedClient == null);


            Console.WriteLine($"Updating client: {selectedClient.FullName} ({selectedClient.Nif})");
            selectedClient.BirthDate = UserIO.ReadBirthDate();
            selectedClient.Address = UserIO.ReadAddress();
            string readpostalCode = UserIO.ReadPostalCode();
            using (var context = new RSGymContext())
            {
                var postalCode = context.PostalCode.FirstOrDefault(p => p.PostalCodeValue == readpostalCode);
                if (postalCode == null)
                {
                    Console.WriteLine("No matching postal code found in our database, let's create one");
                    postalCode = PostalCodeRepository.CreatePostalCode(readpostalCode);
                }
                selectedClient.PostalCodeID = postalCode.PostalCodeID;
            }
            selectedClient.Phone = UserIO.ReadPhoneNumber();
            selectedClient.Email = UserIO.ReadEmail();
            selectedClient.Notes = UserIO.ReadNotes();

            ClientRepository.UpdateClient(selectedClient);

            UserIO.CloseOperation();
        }
        private static void ListClients()
        {
            IList<Client> listClients = ClientRepository.GetActiveClientsOrderedByName();
            Console.WriteLine("Clients list:");
            foreach (Client client in listClients)
            {
                Console.WriteLine($"ClientID:{client.ClientID} | Name:{client.FullName} | Nif:{client.Nif} | Birthdate:{client.BirthDate} | Address:{client.Address} | PostalCode{client.PostalCode.PostalCodeValue} {client.PostalCode.Locality} | Phone:{client.Phone} | Email:{client.Email} | Notes:{client.Notes} | Active:{client.isActive}\n");
            }
            PressKey();
        }
        #endregion

        #region Request Module
        private static void DisplayRequestModule(User user)
        {
            bool requestsMenuExit = false;
            while (!requestsMenuExit)
            {
                Console.Clear();
                WriteTitle("Requests Menu");
                Console.WriteLine("\n1. List all Requests");
                Console.WriteLine("2. Create a Request");
                Console.WriteLine("3. Return to Main Menu\n");

                int requestsMenuChoice = UserIO.GetMenuOption(3, user.UserName);
                switch (requestsMenuChoice)
                {
                    case 1:
                        ListRequests();
                        break;
                    case 2:
                        Request request = CreateRequest();
                        var req = new RequestRepository();
                        req.CreateRequest(request);
                        UserIO.CloseOperation();
                        break;
                    case 3:
                        requestsMenuExit = true;
                        break;
                }
            }
        }
        private static void ListRequests()
        {
            var requestRepository = new RequestRepository();
            IList<Request> requests = requestRepository.GetAllRequestsByStateAndDate();
            Console.WriteLine("Request list:");
            foreach (Request request in requests)
            {
                Console.WriteLine($"Request ID:{request.RequestID} | State:{request.State} | Booking:{request.Booking} | Client:{request.Client.FullName} | Phone:{request.Client.Phone} | Personal Trainer{request.PersonalTrainer.FullName} | Phone:{request.PersonalTrainer.Phone}\n");
            }
            PressKey();
        }
        private static Request CreateRequest()
        {
            
            var pt = new PersonalTrainerRepository();
            int personalTrainerID;

            while (true)
            {
                personalTrainerID = UserIO.ReadClientId("personal trainer");
                if (pt.PTExists(personalTrainerID))
                {
                    break;
                }
                Console.WriteLine($"Personal trainer with ID {personalTrainerID} does not exist in the database. Please try again.");
            }

            int clientID = UserIO.ReadClientId("client");
            DateTime date = UserIO.ReadDate();
            string notes = UserIO.ReadNotes();

            var request = new Request
            {
                PersonalTrainerID = personalTrainerID,
                ClientID = clientID,
                Booking = date,
                State = RequestState.Booked,
                Notes = notes
            };

            return request;
        }
        #endregion

        #region Personal Trainers Model
        private static void DisplayPersonalTrainerModule(User user)
        {
            bool trainersMenuExit = false;
            while (!trainersMenuExit)
            {
                Console.Clear();
                Console.WriteLine("Personal Trainers Dashboard");
                Console.WriteLine("\n1. List all Personal Trainers");
                Console.WriteLine("2. Create a Personal Trainer");
                Console.WriteLine("3. Return to Main Menu\n");

                int trainersMenuChoice = UserIO.GetMenuOption(3, user.UserName);
                switch (trainersMenuChoice)
                {
                    case 1:
                        ListPersonalTrainers();
                        break;
                    case 2:
                        PersonalTrainer newpt = CreatePersonalTrainer();
                        var pt = new PersonalTrainerRepository();
                        pt.CreatePersonalTrainer(newpt);
                        UserIO.CloseOperation();
                        break;
                    case 3:
                        trainersMenuExit = true;
                        break;
                }
            }
        }
        private static PersonalTrainer CreatePersonalTrainer()
        {
            string fullname = UserIO.ReadFullName();
            string personalTrainerCode = UserIO.ReadPersonalTrainerCode();
            string nif = UserIO.ReadNifPT();
            string address = UserIO.ReadAddress();
            string readpostalCode = UserIO.ReadPostalCode();

            //TODO LIMPAR PARA O REPOSITORIO DE CP
            int postalCodeID;
            using (var context = new RSGymContext())
            {
                var postalCode = context.PostalCode.FirstOrDefault(p => p.PostalCodeValue == readpostalCode);
                if (postalCode == null)
                {
                    Console.WriteLine("No matching postal code found in our database, let's create one");
                    postalCode = PostalCodeRepository.CreatePostalCode(readpostalCode);
                }
                postalCodeID = postalCode.PostalCodeID;
            }

            string phone = UserIO.ReadPhoneNumber();
            string email = UserIO.ReadEmail();

            var personalTrainer = new PersonalTrainer
            {
                PersonalTrainerCode = personalTrainerCode,
                FullName = fullname,
                Nif = nif,
                Address = address,
                PostalCodeID = postalCodeID,
                Phone = phone,
                Email = email
            };

            return personalTrainer;
        }
        private static void ListPersonalTrainers()
        {
            var personalTrainerRepository = new PersonalTrainerRepository();
            IList<PersonalTrainer> listPersonalTrainer = personalTrainerRepository.GetAllPersonalTrainers();
            Console.WriteLine("Request list:");
            foreach (PersonalTrainer pt in listPersonalTrainer)
            {
                Console.WriteLine($"ID:{pt.PersonalTrainerID} | Trainer Code:{pt.PersonalTrainerCode} | Personal Trainer:{pt.FullName} | Phone:{pt.Phone} | NIF:{pt.Nif} | Phone:{pt.Phone}");
            }
            PressKey();
        }

        #endregion

        #region Utils
        private static void DisplayLogout()
        {
            Console.WriteLine("Logging out...");
            Thread.Sleep(2000); // Wait for 2 seconds
        }
        public static void ShowMenuOptions(string[] options)
        {
            foreach (string option in options)
            {
                Console.WriteLine($"{option}");
            }
        }
        public static void PressKey()
        {
            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();
        }
        public static void SetUnicodeConsole()
        {
            Console.OutputEncoding = Encoding.UTF8; // System.text
        }
        public static void WriteTitle(string title)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine(title);
            Console.WriteLine(new string('-', 50));
            Console.Write("\n");
        }

        #endregion
    }
}
