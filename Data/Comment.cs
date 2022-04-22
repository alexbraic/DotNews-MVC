using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNews.Data
{
    public class Comment
    {
        public int Id { get; set; }
        [DisplayName("Created By")]
        public Guid createdBy { get; set; }

        [Required]
        [DisplayName("Comment Body")]
        public string? commentBody { get; set; }

        [Required]
        [DisplayName("Report Id")]
        public int reportId { get; set; }
        [DisplayName("Date Created")]
        public DateTime? dateCreated { get; set; }
    }
}
