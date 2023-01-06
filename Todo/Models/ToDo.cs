using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Models
{
    [Table("todo")]
	public class ToDo
	{
		[Key]
		public Guid? Id { get; set; }

		[Required]
		public string? Title { get; set; }

		public bool IsCompleted { get; set; }

		public DateTime? CreatedAt { get; set; }

		public DateTime? CompletedAt { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedAt { get; set; }

		public string? UserId { get; set; }
	}
}

