using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Capstone
{
	/// <summary>
	/// 
	/// </summary>
	public static class PasswordHelper
	{
		/// <summary>
		/// 
		/// </summary>
		public const int PasswordHashLength = 64;

		/// <summary>
		/// 
		/// </summary>
		public const int PasswordSaltLength = 128;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="password"></param>
		/// <param name="passwordHash"></param>
		/// <param name="passwordSalt"></param>
		public static void CalculatePasswordHashAndSalt(
			string password,
			out byte[] passwordHash,
			out byte[] passwordSalt)
		{
			if (password == null)
			{
				throw new ArgumentNullException();
			}
			
			using (var algorithm = new HMACSHA512())
			{
				passwordSalt = algorithm.Key;
				passwordHash = algorithm.ComputeHash(
					Encoding.UTF8.GetBytes(password));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="password"></param>
		/// <param name="passwordHash"></param>
		/// <param name="passwordSalt"></param>
		/// <returns></returns>
		public static bool VerifyPasswordHashAndSalt(
			string password,
			byte[] passwordHash,
			byte[] passwordSalt)
		{
			if (password == null)
			{
				throw new ArgumentNullException();
			}

			if (passwordHash.Length != PasswordHashLength)
			{
				throw new ArgumentException();
			}

			if (passwordSalt.Length != PasswordSaltLength)
			{
				throw new ArgumentException();
			}

			using (var algorithm = new HMACSHA512(passwordSalt))
			{
				var hash = algorithm.ComputeHash(
					Encoding.UTF8.GetBytes(password));
				
				for (var i = 0; i < PasswordHashLength; i++)
				{
					if (hash[i] != passwordHash[i])
					{
						return false;
					}
				}

				return true;
			}
		}
	}
}
