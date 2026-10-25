// model class 

using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class PhoneBookEntry
    {    
        [Key]
        public string Number { get; set; }              // unique
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} Number: {Number} Address: {Address}";
        }
    }
}