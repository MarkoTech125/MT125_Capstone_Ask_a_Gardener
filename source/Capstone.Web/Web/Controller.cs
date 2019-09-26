using System;

using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Capstone;
using Capstone.Entities;
using Capstone.Entities.Users;
using Capstone.Services;

namespace Capstone.Web
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [ApiController]
    public abstract class Controller
        : ControllerBase
    {
        private readonly IUserService userService;

        /// <summary>
        /// 
        /// </summary>
        protected IUserService UserService
        {
            get { return this.userService; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected Controller(IUserService userService)
            : base()
        {
            if (userService == null)
            {
                throw new ArgumentNullException();
            }

            this.userService = userService;
        }
    }
}
