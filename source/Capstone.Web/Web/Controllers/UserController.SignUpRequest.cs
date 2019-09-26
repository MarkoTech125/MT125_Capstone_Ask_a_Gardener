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
        public sealed class SignUpRequest
        {
            /// <summary>
            /// 
            /// </summary>
            [Required]
            public string Name { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [Required]
            public string Password { get; set; }
        }
    }
}
