using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

using Capstone;
using Capstone.Entities;
using Capstone.Services;

namespace Capstone.Web.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public abstract class EntityController<T, TKey>
		: Controller
			where T : class, IEntity<TKey>
	{
		private readonly IEntityRepository<T, TKey> repository;
		
		/// <summary>
		/// 
		/// </summary>
		protected IEntityRepository<T, TKey> Repository
		{
			get { return this.repository; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="repository"></param>
		public EntityController(IUserService userService,
            IEntityRepository<T, TKey> repository)
			    : base(userService)
		{
			if (repository == null)
			{
				throw new ArgumentNullException();
			}

			this.repository = repository;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetAllEntitiesAsync()
		{
			var entities = await this.repository.GetAsync();
			return Ok(entities);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityKey"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{entityKey}")]
		public ActionResult GetEntity(TKey entityKey)
		{
			var entity = GetEntityByKey(entityKey).Result;
			if (entity == null)
			{
				return NotFound();
			}

			return Ok(entity);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult CreateEntity([FromForm] T entity)
		{
			this.repository.CreateAsync(entity).Wait();

			return CreatedAtAction(
				nameof(GetEntity),
				new { entityKey = entity.Key },
				entity);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityKey"></param>
		/// <param name="entity"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("{entityKey}")]
		public ActionResult UpdateEntity(TKey entityKey, [FromForm] T entity)
		{
			if (!entityKey.Equals(entity.Key))
			{
				return BadRequest();
			}

			this.repository.UpdateAsync(entity).Wait();
			return NoContent();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityKey"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{entityKey}")]
		public ActionResult DeleteEntity(TKey entityKey)
		{
			var entity = GetEntityByKey(entityKey).Result;
			if (entity == null)
			{
				return NotFound();
			}

			this.repository.DeleteAsync(entity).Wait();
			return NoContent();
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityKey"></param>
		/// <returns></returns>
		private async Task<T> GetEntityByKey(TKey entityKey)
		{
			var entities = await this.repository
				.GetAsync(x => x.Key.Equals(entityKey));

			return entities.FirstOrDefault();
		}
	}
}
