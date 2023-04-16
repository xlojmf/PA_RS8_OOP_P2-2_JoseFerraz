using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGym_Dal.Models
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }

        public int ClientID { get; set; }

        public int PersonalTrainerID { get; set; }

        [Required(ErrorMessage = "A date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime Booking { get; set; }

        [Required(ErrorMessage = "Request needs a state")]
        [EnumDataType(typeof(RequestState))]
        public RequestState State { get; set; }

        [MaxLength(255)]
        public string Notes { get; set; }

        [ForeignKey("ClientID")]
        public virtual Client Client { get; set; }

        [ForeignKey("PersonalTrainerID")]
        public virtual PersonalTrainer PersonalTrainer { get; set; }
    }

    public enum RequestState
    {
        Booked,
        Over,
        Canceled
    }

}
