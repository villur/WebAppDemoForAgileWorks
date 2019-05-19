using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationDemoForAgileworks.Models
{
    public class SupportTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd-hh-mm}")]
        public DateTime EntryDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd-hh-mm}")]
        public DateTime DueDate { get; set; }

        public DateTime? DoneDate { get; set; }

        public SupportTicket(string description, DateTime dueDate)
        {
            if (description == null || description.Length < 1 || dueDate <= DateTime.Now)
            {
                throw new ArgumentException();
            }
            else
            {
                Description = description;
                EntryDate = DateTime.Now;
                DueDate = dueDate;
            }
        }

        public void MarkDone()
        {
            DoneDate = DateTime.Now;
        }
        public SupportTicket()
        {

        }

    }
}
