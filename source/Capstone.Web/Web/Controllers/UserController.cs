using System;
using System.Security;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Tokens;

using Capstone;
using Capstone.Entities;
using Capstone.Entities.Users;
using Capstone.Services;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/users")]
    public partial class UserController
        : EntityController<User, Int32>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="users"></param>
        public UserController(IUserService userService,
            IEntityRepository<User, Int32> repository)
                : base(userService, repository) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync(
            [FromForm] SignInRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = default(User);
                if (UserService.TryAuthenticate(
                    request.Name, request.Password, out user))
                {
                    var tokenClaims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                    var token = new JwtSecurityToken(
                        claims: tokenClaims,
                        notBefore: DateTime.UtcNow,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: new SigningCredentials(
                            UserService.GetSecurityKey(),
                            SecurityAlgorithms.HmacSha256));

                    var tokenHandler = new JwtSecurityTokenHandler();

                    var response = new SignInResponse()
                    {
                        UserKey = user.Key,
                        UserToken = tokenHandler.WriteToken(token)
                    };

                    return new ObjectResult(response);
                }

                return Unauthorized();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync(
            [FromForm] SignUpRequest request)
        {
            if (ModelState.IsValid)
            {
                var userPasswordHash = default(byte[]);
                var userPasswordSalt = default(byte[]);

                PasswordHelper.CalculatePasswordHashAndSalt(request.Password,
                    out userPasswordHash, out userPasswordSalt);

                var user = (await Repository
                    .GetAsync(x => x.Name == request.Name))
                    .FirstOrDefault();

                if (user == null)
                {
                    user = new User()
                    {
                        Name = request.Name,
                        PasswordHash = userPasswordHash,
                        PasswordSalt = userPasswordSalt,
                        Role = UserRole.Standard
                    };

                    Repository.CreateAsync(user).Wait();
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("updatePassword")]
        public async Task<IActionResult> UpdatePasswordAsync(
            [FromForm] UpdatePasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.ResolveAsync(User.Identity);
                if (user != null)
                {
                    // Ensure the user knows their old password as well.
                    if (UserService.TryAuthenticate(
                        user.Name, request.Password, out user))
                    {
                        var userPasswordHash = default(byte[]);
                        var userPasswordSalt = default(byte[]);

                        // Recalculate the password hash and salt.
                        PasswordHelper.CalculatePasswordHashAndSalt(request.PasswordNew,
                            out userPasswordHash, out userPasswordSalt);

                        user.PasswordHash = userPasswordHash;
                        user.PasswordSalt = userPasswordSalt;

                        await UserService.Users.UpdateAsync(user);
                        return Ok();
                    }

                    return Unauthorized();
                }

                // Weird authentication state...
                return StatusCode(500);
            }

            return BadRequest(ModelState);
        }
    }
}
