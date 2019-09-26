using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Controllers
{
    partial class UserController
    {
        /// <summary>
        /// 
        /// </summary>
		public sealed class UpdatePasswordRequest
        {
			/// <summary>
            /// 
            /// </summary>
            [Required]
			public string Password { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Required]
            public string PasswordNew { get; set; }
        }
    }
}
