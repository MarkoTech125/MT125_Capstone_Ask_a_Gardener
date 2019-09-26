using System;

namespace Capstone.Entities
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	public interface IEntity<TKey>
	{
		/// <summary>
		/// 
		/// </summary>
		TKey Key { get; set; }
	}
}
