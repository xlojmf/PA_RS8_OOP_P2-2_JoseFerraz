using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Dal.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(6)]
        [RegularExpression(@"^[a-zA-Z0-9]{4,6}$")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(12)]
        [RegularExpression(@"^[a-zA-Z0-9]{8,12}$")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A user Role is required.")]
        public Role Role { get; set; }
    }

    public enum Role
    {
        Admin,
        Colab,
        Client
    }

}
