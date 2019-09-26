using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft;
using Newtonsoft.Json;

using Capstone;
using Capstone.Entities;
using Capstone.Entities.Users;

namespace Capstone.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class Feedback
		: IEntity<Int32>
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Key { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Required]
		public string Content { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int AuthorKey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonIgnore]
		[ForeignKey("AuthorKey")]
		public User Author { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Feedback() { }
	}
}
