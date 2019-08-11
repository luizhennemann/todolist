using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoListPresentation.Models
{
    public class ToDoItemModel
    {
        public int ToDoItemId { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Expected Date")]
        [DateGreaterThanToday]
        public DateTime? ExpectedDate { get; set; }

        public bool Done { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string UserId { get; set; }
    }
}
