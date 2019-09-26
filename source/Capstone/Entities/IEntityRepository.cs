using System;

using System.Threading;
using System.Threading.Tasks;

using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;

namespace Capstone.Entities
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public interface IEntityRepository<T, TKey>
		where T : class, IEntity<TKey>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<T>> GetAsync();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		Task<IEnumerable<T>> GetAsync(
			Expression<Func<T, bool>> expression);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task CreateAsync(T entity);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task UpdateAsync(T entity);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task DeleteAsync(T entity);
	}
}
