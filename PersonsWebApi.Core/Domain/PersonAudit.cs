using System.ComponentModel.DataAnnotations;

namespace PersonsWebApi.Core.Domain
{
    public class PersonAudit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(11)]
        public string PersonId { get; set; }

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

        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public int LogInstance { get; set; }
    }
}
