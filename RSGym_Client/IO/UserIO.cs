using RSGym_Client.Repositories;
using RSGym_Dal.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RSGym_Client.IO
{
    public class UserIO
    {

        public static int GetMenuOption(int maxChoice, string username)
        {
            int option;
            bool isValidOption;

            do
            {
                Console.WriteLine("Please select an option:");
                Console.Write($"{username}>");
                string input = Console.ReadLine();

                isValidOption = int.TryParse(input, out option) && option >= 1 && option <= maxChoice;

                if (!isValidOption)
                {
                    Console.WriteLine($"Invalid menu option. Please enter a number between 1 and {maxChoice}.");
                }

            } while (!isValidOption);

            return option;
        }
        public static string ReadUserName()
        {
            while (true)
            {
                Console.Write("Enter username (4-6 characters): ");
                string input = Console.ReadLine();
                if (Regex.IsMatch(input, @"^[a-zA-Z0-9]{4,6}$"))
                {
                    return input;
                }
                Console.WriteLine("Invalid input. Please enter a username that is 4-6 characters long and contains only alphanumeric characters.");
            }
        }
        public static string ReadPassword()
        {
            
            Console.Write("Enter password (8-12 characters): ");
            string password = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]{8,12}$"))
                    {
                        Console.WriteLine("\nInvalid input. Please enter a password that is 8-12 characters long and contains only alphanumeric characters.");
                        Console.Write("Enter password (8-12 characters): ");
                    }
                    else
                    {
                        return password;
                    }
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (char.IsLetterOrDigit(key.KeyChar) && password.Length < 12)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
        }
        public static Role ReadRole(string username)
        {
            string[] roleOptions = { "\nChoose User Role", "1. Admin", "2. Colab", "3. Client" };
            foreach (var role in roleOptions)
            {
                Console.WriteLine($"{role}");
            }
            int roleChoice = GetMenuOption(roleOptions.Length, username);

            switch (roleChoice)
            {
                case 1:
                    return Role.Admin;
                case 2:
                    return Role.Colab;
                case 3:
                    return Role.Client;
                default:
                    throw new InvalidOperationException("Invalid role choice.");
            }
        }
        public static int ReadClientId(string text)
        {
            int clientId;
            bool isValidInput;

            do
            {
                Console.Write($"Enter {text} ID: ");
                string input = Console.ReadLine();

                isValidInput = int.TryParse(input, out clientId);

                if (!isValidInput)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

            } while (!isValidInput);

            return clientId;
        }
        public static string ReadFullName()
        {
            string firstName, lastName;
            do
            {
                Console.Write("Enter first name: ");
                firstName = Console.ReadLine().Trim();

                Console.Write("Enter last name: ");
                lastName = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("Invalid input. Please enter both first and last name.");
                    continue;
                }

                string fullName = new StringBuilder(firstName)
                                    .Append(" ")
                                    .Append(lastName)
                                    .ToString();

                if (fullName.Length > 100)
                {
                    Console.WriteLine("Full name exceeds maximum length of 100 characters. Please enter a shorter name.");
                    continue;
                }

                return fullName;

            } while (true);
        }
        public static string ReadName()
        {
            Console.Write("Enter the name you are looking for: ");
            return Console.ReadLine().Trim();
        }
        public static DateTime ReadBirthDate()
        {
            DateTime birthDate;
            while (true)
            {
                Console.Write("Please enter your birth date (dd/mm/yyyy): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate))
                {
                    return birthDate;
                }
                Console.WriteLine("Invalid input. Please enter a valid birth date in the format dd/mm/yyyy.");
            }
        }
        public static DateTime ReadDate()
        {
            DateTime currentDate = DateTime.Now;
            DateTime dateRequested;

            while (true)
            {
                Console.Write("Enter the date and hour for your class (yyyy/dd/MM HH:mm): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "yyyy/dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateRequested))
                {
                    if (dateRequested > currentDate)
                    {
                        return dateRequested;
                    }
                    Console.WriteLine("The requested date must be after the current date and time.");
                }
                else
                {
                    Console.WriteLine("Invalid date and time format.");
                }
            }
        }
        public static string ReadPostalCode()
        {
            string postalCode;

            do
            {
                Console.Write("Please enter your postal code (in the format XXXX-XXX): ");
                postalCode = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(postalCode))
                {
                    Console.WriteLine("Postal code cannot be empty.");
                    continue;
                }

                if (!Regex.IsMatch(postalCode, @"^\d{4}-\d{3}$"))
                {
                    Console.WriteLine("Invalid postal code format. Please enter a valid code.");
                    continue;
                }

                int firstDigit = int.Parse(postalCode[0].ToString());
                if (firstDigit < 1 || firstDigit > 9)
                {
                    Console.WriteLine("Invalid postal code. Please enter a code starting with a digit between 1 and 9.");
                    continue;
                }               

                break;
            } while (true);

            return postalCode;
        }
        public static string ReadNif()
        {
            string nifString = "";
            int nif;

            do
            {
                WriteMessage("Please enter your NIF:");
                nifString = Console.ReadLine().Trim();

                if (!int.TryParse(nifString, out nif) || nifString.Length != 9)
                {
                    Console.WriteLine("Invalid NIF, please enter a 9 digit number.");
                    continue;
                }

                if (!Regex.IsMatch(nifString, @"^[1-9]\d{8}$"))
                {
                    WriteMessage("Invalid NIF, please enter a valid Portuguese NIF.");
                    continue;
                }

                if (ClientRepository.NifExists(nifString))
                {
                    Console.WriteLine("A client with the provided NIF already exists!");
                    continue;
                }

                break;

            } while (true);

            return nifString;
        }
        public static string ReadNifPT()
        {
            string nifString = "";
            int nif;

            do
            {
                WriteMessage("Please enter your NIF:");
                nifString = Console.ReadLine().Trim();

                if (!int.TryParse(nifString, out nif) || nifString.Length != 9)
                {
                    Console.WriteLine("Invalid NIF, please enter a 9 digit number.");
                    continue;
                }

                if (!Regex.IsMatch(nifString, @"^[1-9]\d{8}$"))
                {
                    WriteMessage("Invalid NIF, please enter a valid Portuguese NIF.");
                    continue;
                }

                var pt = new PersonalTrainerRepository();
                if (pt.PersonalTrainerExists(nifString))
                {
                    Console.WriteLine("A personal trainer with the provided NIF already exists!\nInsert the correct nif!!");
                    continue;
                }

                break;

            } while (true);

            return nifString;
        }
        public static string ReadNifUpdate()
        {
            string nifString = "";
            int nif;

            do
            {
                WriteMessage("Please enter your NIF:");
                nifString = Console.ReadLine().Trim();

                if (!int.TryParse(nifString, out nif) || nifString.Length != 9)
                {
                    Console.WriteLine("Invalid NIF, please enter a 9 digit number.");
                    continue;
                }

                if (!Regex.IsMatch(nifString, @"^[1-9]\d{8}$"))
                {
                    WriteMessage("Invalid NIF, please enter a valid Portuguese NIF.");
                    continue;
                }

                break;

            } while (true);

            return nifString;
        }
        public static string ReadAddress()
        {
            string address;
            do
            {
                Console.Write("Enter address (up to 200 characters): ");
                address = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(address) || address.Length > 200);

            return address;
        }
        public static string ReadPhoneNumber()
        {
            string phoneNumber;
            do
            {
                Console.Write("Enter phone number (9 digits): ");
                phoneNumber = Console.ReadLine()?.Trim();
            } while (!Regex.IsMatch(phoneNumber, @"^\d{9}$"));

            return phoneNumber;
        }
        public static string ReadEmail()
        {
            const string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            string email;

            do
            {
                Console.Write("Please enter your email address: ");
                email = Console.ReadLine();
            } while (!Regex.IsMatch(email, emailPattern));

            return email;
        }
        public static string ReadNotes()
        {
            string notes;
            do
            {
                Console.Write("Enter your notes (up to 255 characters): ");
                notes = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(notes) || notes.Length > 255);

            return notes;
        }
        public static string ReadLocality()
        {
            string locality;
            do
            {
                Console.Write("Enter your locality (up to 200 characters): ");
                locality = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(locality) || locality.Length > 200);

            return locality;
        }
        public static string ReadPersonalTrainerCode()
        {
            string personalTrainerCode="";
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("Please enter your personal trainer code (4 characters): ");
                personalTrainerCode = Console.ReadLine()?.Trim();
                isValid = !string.IsNullOrEmpty(personalTrainerCode) && personalTrainerCode.Length == 4;
                if (!isValid)
                {
                    Console.WriteLine("Invalid input. Please enter a valid personal trainer code with 4 characters.");
                }
            }
            return personalTrainerCode;
        }
        public static void WriteMessage(string message) {         
            Console.WriteLine($"{message}");         
        }
        public static void CloseOperation()
        {
            Console.WriteLine("Saving your data...");
            Thread.Sleep(1500);
            Console.WriteLine("Your operation was complete press any key to return to previous menu");
            Console.ReadKey();
        }
    }
}
