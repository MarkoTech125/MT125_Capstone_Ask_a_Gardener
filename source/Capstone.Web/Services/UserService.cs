using System;

using System.Security;
using System.Security.Principal;

using System.Text;

using System.Linq;

using Microsoft;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Tokens;

using Capstone;
using Capstone.Entities;
using Capstone.Entities.Users;
using Capstone.Entities.Questions;
using System.Threading.Tasks;

namespace Capstone.Services
{
	/// <summary>
	/// 
	/// </summary>
	public class UserService
		: IUserService
	{
        /// <summary>
        /// 
        /// </summary>
        public const string SecurityKey = "#secret_key_123#";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(SecurityKey));
        }

        private readonly IEntityRepository<User, int> users;

		/// <summary>
		/// 
		/// </summary>
		public IEntityRepository<User, int> Users
		{
			get { return this.users; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="users"></param>
		public UserService(IEntityRepository<User, int> users)
		{
			if (users == null)
			{
				throw new ArgumentNullException();
			}

			this.users = users;
		}
        
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="password"></param>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool TryAuthenticate(string name, string password, out User user)
		{
			if ((name == null) || (password == null))
			{
				throw new ArgumentNullException();
			}

			// Get the user.
			user = this.users
				.GetAsync(x => x.Name.Equals(name))
				.Result.FirstOrDefault();
			
			if (user != null)
			{
				// Verify the password hash and salt for the user.
				if (PasswordHelper.VerifyPasswordHashAndSalt(
					password, user.PasswordHash, user.PasswordSalt))
				{
					return true;
				}
			}

			user = null;
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="identity"></param>
		/// <returns></returns>
		public async Task<User> ResolveAsync(IIdentity identity)
		{
			if (identity == null)
			{
				return null;
			}

			var users = await Users.GetAsync(
				x => x.Name == identity.Name);

			return users.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        SymmetricSecurityKey IUserService.GetSecurityKey()
        {
            return GetSecurityKey();
        }
    }
}
