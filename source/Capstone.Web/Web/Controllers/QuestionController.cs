using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Capstone;
using Capstone.Entities;
using Capstone.Entities.Questions;
using System.Security.Claims;
using Capstone.Services;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Capstone.Entities.Users;

namespace Capstone.Web.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[AllowAnonymous]
	[Route("api/questions")]
	public class QuestionController
		: EntityController<Question, Int32>
	{
		private readonly IUserService userService;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="repository"></param>
		public QuestionController(
			IEntityRepository<Question, int> repository,
			IUserService userService)
				: base(userService, repository)
		{
			this.userService = userService;
		}

		public class CreateRequest
		{
			public string Content { get; set; }

			public string ContentExtended { get; set; }
		}

		/// <summary>
		/// 
		/// </summary>
		public class PostRequest
		{
			/// <summary>
			/// 
			/// </summary>
			[Required]
			public string Content { get; set; }

			/// <summary>
			/// 
			/// </summary>
			public string ContentExtended { get; set; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("post")]
		public async Task<IActionResult> PostQuestionAsync(
			[FromForm] PostRequest request)
		{
			if (!ModelState.IsValid)
			{
				// Return a bad request response.
				return BadRequest(ModelState);
			}
			
			// Get the user that sent the request.
			var user = await this.userService
				.ResolveAsync(User.Identity);

			if (user == null)
			{
				return Unauthorized();
			}

			await Repository.CreateAsync(
				new Question()
				{
					AuthorKey = user.Key,
					Content = request.Content,
					ContentExtended = request.ContentExtended,
					Timestamp = DateTime.UtcNow
				});

			return Ok();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityKey"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("{entityKey}/post")]
		public async Task<IActionResult> PostQuestionReplyAsync(
			int entityKey, [FromForm] PostRequest request)
		{
			if (!ModelState.IsValid)
			{
				// Return a bad request response.
				return BadRequest(ModelState);
			}

			// Get the user that sent the request.
			var user = await this.userService
				.ResolveAsync(User.Identity);

			if (user == null)
			{
				return Unauthorized();
			}

			if (user.Role != UserRole.Administrator)
			{
				return Unauthorized();
			}

			var question = (await Repository.GetAsync(
				x => x.Key == entityKey)).FirstOrDefault();

			if (question == null)
			{
				return NotFound();
			}

			question.Replies.Add(new QuestionReply()
			{
				Author = user,
				Question = question,
				Content = request.Content,
				Timestamp = DateTime.UtcNow
			});

			await Repository.UpdateAsync(question);

			return Ok();
		}
	}
}
