using System.ComponentModel.DataAnnotations;

namespace PersonsWebApi.Core.Domain
{
    public class Person
    {
        [Key]
        [StringLength(11)]
        public string Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Firstname cannot be longer than 50 characters.")]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "LastName cannot be longer than 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Range(0, 120, ErrorMessage = "Age must be between 0 and 120.")]
        public byte Age { get; set; }

        [DataType(DataType.Currency)]
        public double Salary { get; set; }
    }
}
