using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Newtonsoft;
using Newtonsoft.Json;

namespace Capstone.Entities.Users
{
    /// <summary>
    /// Represents a user in the Capstone application.
    /// </summary>
    public class User
        : IEntity<Int32>
    {
		/// <summary>
		/// Gets or sets the unique key for the user.
		/// </summary>
		[Key]
		public int Key { get; set; }

        /// <summary>
        /// Gets or sets the name for the user.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the password hash for the user.
        /// </summary>
        [Required]
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the password salt for the user.
        /// </summary>
        [Required]
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the role for the user.
        /// </summary>
        [Required]
        public UserRole Role { get; set; }
    }
}
