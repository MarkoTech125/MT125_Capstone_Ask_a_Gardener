using System;

using System.Collections;
using System.Collections.Generic;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capstone.Entities.Users;

using Microsoft;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Newtonsoft;
using Newtonsoft.Json;

namespace Capstone.Entities.Questions
{
	/// <summary>
	/// 
	/// </summary>
	public class Question
		: IEntity<Int32>
	{
		private ILazyLoader repliesLoader;
		private ICollection<QuestionReply> replies;

		/// <summary>
		/// 
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Key { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ContentExtended { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonIgnore]
		public virtual ICollection<QuestionReply> Replies
		{
			get
			{
				return this.repliesLoader
					.Load(this, ref this.replies);
			}
			set { this.replies = value; }
		}

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
        public Question()
		{
			Replies = new List<QuestionReply>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="repliesLoader"></param>
		private Question(ILazyLoader repliesLoader)
		{
			this.repliesLoader = repliesLoader;
			this.replies = null;
		}
	}
}
