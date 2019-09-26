using System;
using System.IO;

using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

using Capstone;
using Capstone.Entities;
using Capstone.Entities.Users;
using Capstone.Entities.Questions;
using Capstone.Services;

namespace Capstone.Web
{
	/// <summary>
	/// 
	/// </summary>
	public class ProgramStartup
	{
		/// <summary>
		/// 
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="configuration"></param>
		public ProgramStartup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseStaticFiles();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseCors(opts =>
			{
				opts.AllowAnyOrigin();
				opts.AllowAnyMethod();
				opts.AllowAnyHeader();
			});

			app.UseAuthentication();
			app.UseMvc();
			
			using (var scope = app.ApplicationServices.CreateScope())
			{
				// Get the database context.
				var databaseContext = scope.ServiceProvider
					.GetService<EntityRepositoryContext>();
				
				// Get the database.
				var database = databaseContext.Database;

				database.EnsureDeleted();
				database.EnsureCreated();

				databaseContext.Seed();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			
			ConfigureDatabaseServices(services);
			
			// Configure user authorization.
			services.AddScoped<IUserService, UserService>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                // TODO: Enable this for production.
                x.RequireHttpsMetadata = false;

                x.SaveToken = true;
                x.TokenValidationParameters =
                    new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = UserService.GetSecurityKey(),

                        ValidateIssuer = false,
                        ValidateAudience = false,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
            });
		}

		/// <summary>
		/// Configures database-related services at program startup.
		/// </summary>
		/// <param name="services"></param>
		private void ConfigureDatabaseServices(
			IServiceCollection services)
		{
			// Configure database services.
			services.AddDbContext<EntityRepositoryContext>(opt =>
				opt.UseInMemoryDatabase("Entities"));

			// Configure entity repository implementations.
			services.AddScoped<
				IEntityRepository<Question, Int32>,
				EntityRepository<Question, Int32>>();

			services.AddScoped<
				IEntityRepository<QuestionReply, Int32>,
				EntityRepository<QuestionReply, Int32>>();

			services.AddScoped<
				IEntityRepository<Feedback, Int32>,
				EntityRepository<Feedback, Int32>>();

            services.AddScoped<
                IEntityRepository<User, Int32>,
                EntityRepository<User, Int32>>();
		}
	}
}
