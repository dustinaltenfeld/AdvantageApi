using System;
namespace Advantage.API.Models
{
    public class User
    {
        public int Id { get;set; }
        public string Name {get; set;}
        public string Email {get; set;}
        public Gender Gender {get; set;}
        public DateTime? Bday {get; set;}
        public DateTime Registration_Day {get; set;}

    }
}