using System.ComponentModel.DataAnnotations;

namespace StudyE_BookWeb.Models
{
    public class Dictionary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
