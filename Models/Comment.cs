using System;
using System.ComponentModel.DataAnnotations;

namespace NUCCITWebApp.Models
{
    public class Comment
    {
        public int? Id { get; set; } // Unique identifier for the comment (nullable)

        [StringLength(20, ErrorMessage = "Role cannot exceed 20 characters.")]
        public string? Role { get; set; } // The role of the user making the comment (nullable)

        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string? CommentText { get; set; } // The content of the comment (nullable)

        [DataType(DataType.Date)]
        public DateTime? DatePosted { get; set; } // The date the comment was posted (nullable)
    }
}
