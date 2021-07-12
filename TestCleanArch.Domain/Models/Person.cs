using System;
using System.Text.Json.Serialization;


namespace TestCleanArch.Domain.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public string Email { get; set; }
        public bool SendType { get; set; }
    }
}
