using System;

namespace Lab2.Entities
{
    public class Danger
    {
        public Danger(int id, string name, string description, string source, string objective, bool isPrivacyViolation,
            bool isIntegrityViolation, bool isAccessViolation)
        {
            Id = id;
            Name = name;
            Description = description;
            Source = source;
            Objective = objective;
            IsPrivacyViolation = isPrivacyViolation;
            IsIntegrityViolation = isIntegrityViolation;
            IsAccessViolation = isAccessViolation;
            ShortInfo = new ShortInfo(){ Name = this.Name, ID = $"УБИ.{this.Id}"};
        }
        public Danger(string[] param)
        {
            Id = Int32.Parse(param[0]);
            Name = param[1];
            Description = param[2];
            Source = param[3];
            Objective = param[4];
            IsPrivacyViolation = (param[5] != "0");
            IsIntegrityViolation = (param[6] != "0");
            IsAccessViolation = (param[7] != "0");
            ShortInfo = new ShortInfo() {  Name = this.Name, ID = $"УБИ.{this.Id}" };
        }

        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Objective { get; set; }
        public bool IsPrivacyViolation { get; set; }
        public bool IsIntegrityViolation { get; set; }
        public bool IsAccessViolation { get; set; }
        public ShortInfo ShortInfo { get; }
        public override bool Equals(object oth)
        {
            return Id ==((Danger) oth).Id;
        }

        public override int GetHashCode()
        {
            return Id*32;
        }

        public override string ToString()
        {
            return $"{Id} {Name} {Description} {Source} {Objective} {(IsPrivacyViolation?1:0)} {(IsIntegrityViolation?1:0)} {(IsAccessViolation?1:0)}";
        }

        public string GetText()
        {
            return $"Описание: {Description}\n " +
                   $"Источник: {Source} \n" +
                   $"Объект воздейтсвия: {Objective}\n" +
                   $"Нарушение конфиденциальности: {(IsPrivacyViolation ? "да" : "нет")}\n" +
                   $"Нарушение целостности: {(IsIntegrityViolation ? "да" : "Нет")}\n" +
                   $"Нарушение доступности: {(IsAccessViolation ? "да" : "нет")}\n";
        }

    }

    public class ShortInfo
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}