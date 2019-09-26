using System;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

using Microsoft;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Tokens;

using Capstone;
using Capstone.Entities;
using Capstone.Entities.Users;

namespace Capstone.Services
{
	/// <summary>
	/// 
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// 
		/// </summary>
		IEntityRepository<User, Int32> Users { get; }
        
		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="user"></param>
		/// <returns></returns>
		bool TryAuthenticate(
			string username,
			string password,
            out User user);
        
        /// <summary>
        /// Attempts to resolve an identity object to a user.
        /// </summary>
        /// <param name="identity">The identity object to resolve.</param>
        /// <returns></returns>
        Task<User> ResolveAsync(IIdentity identity);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        SymmetricSecurityKey GetSecurityKey();
    }
}
