using System;
using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.Models.Entities
{
    public class ContactPost : BaseEntity
    {
        [Required]
        public string Comment { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }

        public string Answer { get; set; }
        public DateTime? AnsweredDate { get; set; }
        public int? AnswerByUserId { get; set; }
    }
}
