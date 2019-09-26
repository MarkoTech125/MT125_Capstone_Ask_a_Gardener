using System;

using System.Threading;
using System.Threading.Tasks;

using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;

using Microsoft;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Entities
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public class EntityRepository<T, TKey>
		: IEntityRepository<T, TKey>
			where T : class, IEntity<TKey>
	{
		private readonly EntityRepositoryContext context;

		/// <summary>
		/// 
		/// </summary>
		protected EntityRepositoryContext Context
		{
			get { return this.context; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public EntityRepository(EntityRepositoryContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException();
			}

			this.context = context;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<T>> GetAsync()
		{
			return await this.context.Set<T>()
				.ToListAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public async Task<IEnumerable<T>> GetAsync(
			Expression<Func<T, bool>> expression)
		{
			return await this.context.Set<T>()
				.Where(expression)
				.ToListAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public async Task CreateAsync(T entity)
		{
			this.context.Set<T>().Add(entity);

			await this.context.SaveChangesAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public async Task UpdateAsync(T entity)
		{
			this.context.Set<T>().Update(entity);

			await this.context.SaveChangesAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public async Task DeleteAsync(T entity)
		{
			this.context.Set<T>().Remove(entity);

			await this.context.SaveChangesAsync();
		}
	}
}
