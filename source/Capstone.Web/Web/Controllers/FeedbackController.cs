using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

using Capstone;
using Capstone.Entities;
using Capstone.Services;

namespace Capstone.Web.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Route("api/feedback")]
	public partial class FeedbackController
		: Controller
	{
		private readonly IEntityRepository<Feedback, Int32> repository;

		/// <summary>
		/// 
		/// </summary>
		protected IEntityRepository<Feedback, Int32> Repository
		{
			get { return this.repository; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userService"></param>
		/// <param name="repository"></param>
		public FeedbackController(IUserService userService,
			IEntityRepository<Feedback, Int32> repository)
				: base(userService)
		{
			if (repository == null)
			{
				throw new ArgumentNullException();
			}

			this.repository = repository;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("post")]
		public async Task<IActionResult> PostFeedbackAsync(
			[FromForm] PostFeedbackRequest request)
		{
			if (ModelState.IsValid)
			{
				var user = await UserService.ResolveAsync(User.Identity);
				if (user != null)
				{
					var entity = new Feedback()
					{
						Content = request.Content,
						AuthorKey = user.Key
					};

					await Repository.CreateAsync(entity);
					return Ok();
				}

				return Unauthorized();
			}

			return BadRequest(ModelState);
		}
	}
}
