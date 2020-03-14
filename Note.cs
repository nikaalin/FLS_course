using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBookConsole
{
    class Note
    {
        public int Id { get; private set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
        public DateTime Birthday { get; set; }
        public string Org { get; set; }
        public string Prof { get; set; }
        public string OtherNotes { get; set; }

        public Note(string surname, string name, string number, string country)
        {
            Surname = surname;
            Name = name;
            Number = number;
            Country = country;
            Id = GetHashCode();
        }

        public override int GetHashCode()
        {
            return Surname.GetHashCode() + Name.GetHashCode() + Number.GetHashCode();
        }

        public void UpdateId()
        {
            Id = GetHashCode();
        }
    }
}
