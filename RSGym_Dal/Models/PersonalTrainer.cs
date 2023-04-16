using RSGym_Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Dal.Models
{
    public class PersonalTrainer : IPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonalTrainerID { get; set; }

        public int PostalCodeID { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Personal trainer code must be 4 characters long.")]
        public string PersonalTrainerCode { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "NIF is required.")]
        [MaxLength(9, ErrorMessage = "The NIF must have 9 numbers.")]
        [RegularExpression("^[125689]\\d{8}$")]
        public string Nif { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(200, ErrorMessage = "Address must be 200 characters or less.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(9)]
        [RegularExpression("^[0-9]{9}$")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(150)]
        [RegularExpression(@"^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$")]
        public string Email { get; set; }

        public virtual PostalCode PostalCode { get; set; }
        public virtual ICollection<Request> Request { get; set; }
    }

}

