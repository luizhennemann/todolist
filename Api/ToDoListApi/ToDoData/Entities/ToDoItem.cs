using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoData.Entities
{
    public class ToDoItem
    {
        [Key]
        [Required]
        public int ToDoItemId { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpectedDate { get; set; }

        public bool Done { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
