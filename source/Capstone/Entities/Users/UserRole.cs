using System;

namespace Capstone.Entities.Users
{
    /// <summary>
    /// Enumerates the roles of a user in the Capstone application.
    /// </summary>
    [Serializable]
    public enum UserRole
    {
        /// <summary>
        /// The standard user role.
        /// </summary>
        Standard = 0,

        /// <summary>
        /// The administrator user role.
        /// </summary>
        Administrator = 1
    }
}
