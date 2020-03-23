using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NoteBookConsole
{
    class NoteBook
    {
        private static Dictionary<int, Note> notes = new Dictionary<int, Note>();

        static void Main(string[] args)
        {
            PrintProgramInfo();
            while (true)
            {
                Console.WriteLine("Enter command:");
                switch (Console.ReadLine().Trim())
                {
                    case "exit":
                        return;
                    case "new":
                        Create();
                        break;
                    case "edit":
                        Edit();
                        break;
                    case "delete":
                        Delete();
                        break;
                    case "show all":
                        ShowAll();
                        break;
                    case "show":
                        Show();
                        break;
                    case "help":
                        PrintProgramInfo();
                        break;
                    default:
                        Console.WriteLine("Unknown command. Please, try again.");
                        break;
                }
            }
        }

        private static void Show()
        {
            Console.WriteLine("Choose a contact.");
            string sur = CustomRead.ReadNullSafeString("Enter surname:");
            string name = CustomRead.ReadNullSafeString("Enter name:");
            string num = CustomRead.ReadNumber("Enter number:");

            int id = sur.GetHashCode() + name.GetHashCode() + num.GetHashCode();
            Note n = notes[id];

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Contact: {n.Surname} {n.Name} {n.MiddleName}");
            Console.WriteLine($"Number: {n.Number}");
            Console.WriteLine($"Country: {n.Country}");
            Console.WriteLine($"Birthday: {n.Birthday.Date.ToShortDateString()}");
            Console.WriteLine($"Organization: {n.Org}");
            Console.WriteLine($"Profession: {n.Prof}");
            Console.WriteLine($"Notes: {n.OtherNotes}");
            Console.ResetColor();
        }

        private static void ShowAll()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("{0,10}   |{1,10}   |{2,10}", "Surname", "Name", "Number");
            Console.ResetColor();
            foreach (var noteWithId in notes)
            {
                Note note = noteWithId.Value;
                Console.WriteLine("{0,10}   |{1,10}   |{2,10}", note.Surname, note.Name, note.Number);
            }
        }

        private static void Edit()
        {
            Console.WriteLine("Choose a contact.");
            string sur = CustomRead.ReadNullSafeString("Enter surname:");
            string name = CustomRead.ReadNullSafeString("Enter name:");
            string num = CustomRead.ReadNumber("Enter number:");

            int id = sur.GetHashCode() + name.GetHashCode() + num.GetHashCode();
            Note n = notes[id];
            notes.Remove(id);

            Console.WriteLine(
                "You can edit fields: surname, name, number, country, organization, profession, birthday and notes.");
            Console.WriteLine("If you've finished, enter \"ok\"");

            while (true)
            {
                Console.WriteLine("Enter field to edit:");
                switch (Console.ReadLine())
                {
                    case "ok":
                        goto EndOfLoop;
                    case "surname":
                        Console.WriteLine($"Old surname: {sur}");
                        sur = CustomRead.ReadNullSafeString("Enter new surname:");
                        n.Surname = sur;
                        break;
                    case "name":
                        Console.WriteLine($"Old name: {name}");
                        name = CustomRead.ReadNullSafeString("Enter new name:");
                        n.Name = name;
                        break;
                    case "number":
                        Console.WriteLine($"Old number: {num}");
                        num = CustomRead.ReadNumber("Enter new number:");
                        n.Number = num;
                        break;
                    case "birthday":
                        Console.WriteLine($"Old birthday: {n.Birthday}");
                        DateTime bth = CustomRead.ReadDate("Enter new birthday:");
                        n.Birthday = bth;
                        break;
                    case "middle name":
                        Console.WriteLine($"Old middle name: {n.MiddleName}");
                        String middleName = CustomRead.ReadString("Enter new middleName:");
                        n.MiddleName = middleName;
                        break;
                    case "country":
                        Console.WriteLine($"Old country: {n.Country}");
                        String country = CustomRead.ReadNullSafeString("Enter new country:");
                        n.Country = country;
                        break;
                    case "organization":
                        Console.WriteLine($"Old organization: {n.Org}");
                        String org = CustomRead.ReadString("Enter new organization:");
                        n.Org = org;
                        break;
                    case "profession":
                        Console.WriteLine($"Old profession: {n.Prof}");
                        String prof = CustomRead.ReadString("Enter new profession:");
                        n.Prof = prof;
                        break;
                    case "notes":
                        Console.WriteLine($"Old notes: {n.OtherNotes}");
                        String otherNotes = CustomRead.ReadString("Enter new notes:");
                        n.OtherNotes = otherNotes;
                        break;
                    default:
                        Console.WriteLine("Unknown field. Try again");
                        continue;
                }

                Console.WriteLine("Field is edited");
            }

            EndOfLoop:
            n.UpdateId();
            notes.Add(n.Id, n);
            Console.WriteLine("Contact is edited.");
        }

        private static void Delete()
        {
            string sur = CustomRead.ReadNullSafeString("Enter surname");
            string name = CustomRead.ReadNullSafeString("Enter name");
            string num = CustomRead.ReadNumber("Enter number");

            int id = sur.GetHashCode() + name.GetHashCode() + num.GetHashCode();
            notes.Remove(id);

            Console.WriteLine("Contact is deleted.");
        }

        private static void Create()
        {
            string sur = CustomRead.ReadNullSafeString("Enter surname (required):");
            string name = CustomRead.ReadNullSafeString("Enter name (required):");
            string mid = CustomRead.ReadString("Enter middle name:");
            string num = CustomRead.ReadNumber("Enter number (required):");
            string country = CustomRead.ReadNullSafeString("Enter country (required):");
            DateTime bth = CustomRead.ReadDate("Enter birthday:");
            string org = CustomRead.ReadString("Enter organization:");
            string prof = CustomRead.ReadString("Enter profession:");
            string othNotes = CustomRead.ReadString("Enter other notes:");

            Note n = new Note(sur, name, num, country);
            n.MiddleName = mid;
            n.Birthday = bth;
            n.Org = org;
            n.Prof = prof;
            n.OtherNotes = othNotes;

            try
            {
                notes.Add(n.Id, n);
            }
            catch (Exception e)
            {
                Console.WriteLine("Same contact exists.");
            }

            Console.WriteLine("Contact is created.");
        }

        private static void PrintProgramInfo()
        {
            Console.WriteLine("Hello! It's notebook. You can use next commands for work:");
            Console.WriteLine(" -- new - to create a new contact;");
            Console.WriteLine(" -- edit - to edit a contact;");
            Console.WriteLine(" -- delete - to delete a contact;");
            Console.WriteLine(" -- show all - to show a list of contacts;");
            Console.WriteLine(" -- show - to show detail info of contact;");
            Console.WriteLine(" -- exit - to finish a work;");
            Console.WriteLine(" -- help - to show this info again.");
            Console.WriteLine("Good luck!");
        }
    }
}