using RSGym_Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Dal.Models
{
    public class Client : IPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }
        public int PostalCodeID { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Birth date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Birth date must be in the format dd/mm/yyyy.")]
        public DateTime BirthDate { get; set; }

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

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [MaxLength(255, ErrorMessage = "Notes must be 255 characters or less.")]
        public string Notes { get; set; }
        // Required por ao inserir novo client por defeito o quero activo
        [Required]
        [Column("isActive")]
        public bool isActive { get; set; }

        public PostalCode PostalCode { get; set; }
        public ICollection<Request> Request { get; set; }
    }

}

