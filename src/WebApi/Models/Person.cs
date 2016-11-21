using System;

namespace WebApi.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public decimal Salary { get; set; }
    }
}
