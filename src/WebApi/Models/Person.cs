using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        [Required]
        public decimal Salary { get; set; }
    }
}
