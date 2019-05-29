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

        public DateTime EntryDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? DoneDate { get; set; }

        public SupportTicket(string description, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(description) || dueDate <= DateTime.Now)
            {
                throw new ArgumentException("Constructing ticket failed in ticket constructor");
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
