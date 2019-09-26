using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	partial class FeedbackController
	{
		/// <summary>
		/// 
		/// </summary>
		public sealed class PostFeedbackRequest
		{
			/// <summary>
			/// 
			/// </summary>
			[Required]
			public string Content { get; set; }
		}
	}
}
