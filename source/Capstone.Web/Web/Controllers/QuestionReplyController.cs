using System;

using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Capstone;
using Capstone.Entities;
using Capstone.Entities.Questions;
using Capstone.Services;

namespace Capstone.Web.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[AllowAnonymous]
	[Route("api/questionReplies")]
	public class QuestionReplyController
		: EntityController<QuestionReply, Int32>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="repository"></param>
		public QuestionReplyController(
            IUserService userService,
			IEntityRepository<QuestionReply, int> repository)
				: base(userService, repository) { }
	}
}
