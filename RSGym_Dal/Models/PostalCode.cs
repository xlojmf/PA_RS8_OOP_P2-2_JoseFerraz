using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Dal.Models
{
    public class PostalCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostalCodeID { get; set; }

        [Required(ErrorMessage = "A Postal Code is Required")]
        [MaxLength(8)]
        [RegularExpression(@"^\d{4}-\d{3}$", ErrorMessage = "Invalid Postal Code format, correct format is 0000-000")]
        public string PostalCodeValue { get; set; }

        [Required(ErrorMessage = "A Locality is Required")]
        [MaxLength(200)]
        public string Locality { get; set; }

        public virtual ICollection<Client> Client { get; set; }
        public virtual ICollection<PersonalTrainer> PersonalTrainer { get; set; }
    }

}
