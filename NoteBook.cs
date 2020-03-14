using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NoteBookConsole
{
    class NoteBook
    {
        static Dictionary<int, Note> notes = new Dictionary<int, Note>();

        static void Main(string[] args)
        {
            PrintProgramInfo();
            while (true)
            {
                switch (Console.ReadLine())
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
                        break;
                    case "show":
                        break;
                    default:
                        Console.WriteLine("Unknown command. Try again");
                        break;
                }
            }
        }

        private static void Show() { }
        private static void ShowAll() { }

        private static void Edit()
        {
            string sur = CustomRead.ReadNullSafeString("Enter surname:");
            string name = CustomRead.ReadNullSafeString("Enter name:");
            string num = CustomRead.ReadNumber("Enter number:");

            int id = sur.GetHashCode() + name.GetHashCode() + num.GetHashCode();
            Note n = notes[id];
            notes.Remove(id);

            PrintEditInfo();
            Console.WriteLine("Enter field to edit");
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "ok":
                        return;
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
                        break;
                }
            }

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
            string sur = CustomRead.ReadNullSafeString("Enter surname:");
            string name = CustomRead.ReadNullSafeString("Enter name:");
            string mid = CustomRead.ReadString("Enter middle name:");
            string num = CustomRead.ReadNumber("Enter number:");
            string country = CustomRead.ReadNullSafeString("Enter country:");
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
            Console.WriteLine("There are info");
        }
        private static void PrintEditInfo()
        {
            Console.WriteLine("There are fields to edit");
        }
    }

}