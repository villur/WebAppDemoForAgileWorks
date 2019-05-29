using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDemoForAgileworks.Models
{
    public class SupportTicketViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd-hh-mm}")]
        public DateTime EntryDate { get; set; }

        [DisplayFormat(DataFormatString = "{yyyy-MM-dd-hh-mm}")]
        public DateTime DueDate { get; set; }
        public DateTime? DoneDate { get; set; }



    }
}
