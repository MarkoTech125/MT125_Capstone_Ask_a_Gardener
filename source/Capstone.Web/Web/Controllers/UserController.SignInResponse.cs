using System;

namespace Capstone.Web.Controllers
{
    partial class UserController
    {
        /// <summary>
        /// 
        /// </summary>
        public sealed class SignInResponse
        {
            /// <summary>
            /// 
            /// </summary>
            public int UserKey { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string UserToken { get; set; }
        }
    }
}
