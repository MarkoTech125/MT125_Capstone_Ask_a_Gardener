using System;

using Capstone;
using Capstone.Entities;
using Capstone.Entities.Users;
using Capstone.Entities.Questions;

namespace Capstone.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public static class EntityRepositoryContextExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public static void Seed(this EntityRepositoryContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException();
			}

			SeedUsers(context);
			SeedQuestions(context);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		private static void SeedUsers(EntityRepositoryContext context)
		{
			var userPasswordHash = default(byte[]);
			var userPasswordSalt = default(byte[]);

			PasswordHelper.CalculatePasswordHashAndSalt("admin",
				out userPasswordHash, out userPasswordSalt);

			context.Set<User>().Add(
				new User
				{
					Name = "admin",
					PasswordHash = userPasswordHash,
					PasswordSalt = userPasswordSalt,
					Role = UserRole.Administrator
				});

			PasswordHelper.CalculatePasswordHashAndSalt("user",
				out userPasswordHash, out userPasswordSalt);

			context.Set<User>().Add(
				new User()
				{
					Name = "user",
					PasswordHash = userPasswordHash,
					PasswordSalt = userPasswordSalt,
					Role = UserRole.Standard
				});

			context.SaveChanges();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		private static void SeedQuestions(EntityRepositoryContext context)
		{
			var questionAuthor = context.Set<User>().Find(2);
			var questionReplyAuthor = context.Set<User>().Find(1);

			context.Set<Question>().Add(
				new Question()
				{
					Content = "How should I care for my lawn in the Fall?",
					ContentExtended = String.Empty,
					Timestamp = DateTime.UtcNow,
					Author = questionAuthor,
					Replies = new QuestionReply[]
					{
						new QuestionReply()
						{
							Content = "The fall is an important time for fertilizing, aerating, moving and raking. Using a winter fertilizer will help your grass survive the cold and come back strong. Aerating in the fall helps promote root growth in the spring. Make sure to rake any clippings and leaves before the snow arrives, as that will become a breeding ground for disease in the spring.",
							Timestamp = DateTime.UtcNow,
							Author = questionReplyAuthor
						}
					}
				});

			context.Set<Question>().Add(
				new Question()
				{
					Content = "How often should I water my lawn?",
					ContentExtended = String.Empty,
					Timestamp = DateTime.UtcNow,
					Author = questionAuthor,
					Replies = new QuestionReply[]
					{
						new QuestionReply()
						{
							Content = "It's best to water your lawn (under regular conditions)  once a week in the early morning. It's best to avoid afternoon and evening watering, because it causes quick evaporation. This can make your lawn vulnerable to disease.",
							Timestamp = DateTime.UtcNow,
							Author = questionReplyAuthor
						}
					}
				});

			context.Set<Question>().Add(
				new Question()
				{
					Content = "How will heat and drought affect my treatments and lawn?",
					ContentExtended = String.Empty,
					Timestamp = DateTime.UtcNow,
					Author = questionAuthor,
					Replies = new QuestionReply[]
					{
						new QuestionReply()
						{
							Content = "Under drought conditions, your lawn will go dormant and turn dry and brown in order to conserve water. It does not mean that it's dead, it will come back when the drought is over. Water only bi-weakly and minimize the foot traffic, and wait to schedule treatments until there is more rain.",
							Timestamp = DateTime.UtcNow,
							Author = questionReplyAuthor
						}
					}
				});

			context.Set<Question>().Add(
				new Question()
				{
					Content = "What is the difference between annuals and perennials?",
					ContentExtended = String.Empty,
					Timestamp = DateTime.UtcNow,
					Author = questionAuthor,
					Replies = new QuestionReply[]
					{
						new QuestionReply()
						{
							Content = "Annuals grow, bloom and die all in one season. They are typically used in areas where we want a lot of colour, as they will flower all season. Perennials live for many years, they grow and bloom and after winter they come back from the roots. They usually flower for only a part of the season.",
							Timestamp = DateTime.UtcNow,
							Author = questionReplyAuthor
						}
					}
				});

			context.Set<Question>().Add(
				new Question()
				{
					Content = "Shade or sun - what do I do?",
					ContentExtended = String.Empty,
					Timestamp = DateTime.UtcNow,
					Author = questionAuthor,
					Replies = new QuestionReply[]
					{
						new QuestionReply()
						{
							Content = "You should first determine what you have; is it a shade most of the day, is it burning hot most of the day, or is it a combination of sun and shade? Once you know your conditions, you should choose the plants that are best fit. For example, petunias are great flowers for sun gardens, while most ferns prefer shade. If your house is facing south or southwest, make sure you have good organic matter in the soil and are using mulch to help keep your flowers watered.",
							Timestamp = DateTime.UtcNow,
							Author = questionReplyAuthor
						}
					}
				});

			context.Set<Question>().Add(
				new Question()
				{
					Content = "How do I water less?",
					ContentExtended = String.Empty,
					Timestamp = DateTime.UtcNow,
					Author = questionAuthor,
					Replies = new QuestionReply[]
					{
						new QuestionReply()
						{
							Content = "In order to use less water, make sure to water only in the morning or evening, making sure to water deeply in order to get the roots to grow down.",
							Timestamp = DateTime.UtcNow,
							Author = questionReplyAuthor
						}
					}
				});

			context.Set<Question>().Add(
				new Question()
				{
					Content = "What things should I consider when starting a vegetable garden?",
					ContentExtended = String.Empty,
					Timestamp = DateTime.UtcNow,
					Author = questionAuthor,
					Replies = new QuestionReply[]
					{
						new QuestionReply()
						{
							Content = "In general, you should find an area that will receive at least five to six hours of direct sunlight daily. Decide which vegetables and the amount of each you want to include in your garden. Make a list of all the vegetables your family enjoys (there's no use growing a vegetable if it won't get eaten). Then, put a number beside each variety indicating the number of plants required to feed you and your family.",
							Timestamp = DateTime.UtcNow,
							Author = questionReplyAuthor
						}
					}
				});

			context.Set<Question>().Add(
				new Question()
				{
					Content = "Which plants can be started from seed?",
					ContentExtended = String.Empty,
					Timestamp = DateTime.UtcNow,
					Author = questionAuthor,
					Replies = new QuestionReply[]
					{
						new QuestionReply()
						{
							Content = "The following plants should be started from seed: beans, beets, carrots, corn, peas and radishes. When growing plants from seed, follow the instructions on the seed pack.",
							Timestamp = DateTime.UtcNow,
							Author = questionReplyAuthor
						}
					}
				});

			context.Set<Question>().Add(
				new Question()
				{
					Content = "How should I water my vegetables?",
					ContentExtended = String.Empty,
					Timestamp = DateTime.UtcNow,
					Author = questionAuthor,
					Replies = new QuestionReply[]
					{
						new QuestionReply()
						{
							Content = "Vegetables are thirsty! Water them thoroughly with a mild fertilizer to give them a good start. Thereafter, water whenever the soil begins to dry. Water early in the day by soaking the soil. Do not just sprinkle the foliage with water.",
							Timestamp = DateTime.UtcNow,
							Author = questionReplyAuthor
						}
					}
				});

			context.SaveChanges();
		}
	}
}
