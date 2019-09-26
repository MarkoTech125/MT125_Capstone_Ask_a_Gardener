using System;

using Microsoft;
using Microsoft.EntityFrameworkCore;

using Capstone;
using Capstone.Entities;
using Capstone.Entities.Users;
using Capstone.Entities.Questions;

namespace Capstone.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class EntityRepositoryContext
		: DbContext
	{
		/// <summary>
		/// 
		/// </summary>
		public DbSet<Question> Questions { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DbSet<QuestionReply> QuestionReplies { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DbSet<Feedback> Feedback { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="options"></param>
		public EntityRepositoryContext(
			DbContextOptions<EntityRepositoryContext> options)
				: base(options) { }
	}
}
