using System;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft;
using Newtonsoft.Json;

using Capstone;
using Capstone.Entities;
using Capstone.Entities.Users;

namespace Capstone.Entities.Questions
{
	/// <summary>
	/// 
	/// </summary>
	public class QuestionReply
		: IEntity<Int32>
	{
		/// <summary>
		/// 
		/// </summary>
		[Key]
		public int Key { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int QuestionKey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonIgnore]
		[ForeignKey("QuestionKey")]
		public Question Question { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Timestamp { get; set; }

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
		public QuestionReply() { }
	}
}
