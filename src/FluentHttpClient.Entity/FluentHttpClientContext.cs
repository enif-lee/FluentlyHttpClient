﻿using System.Reflection;
using System.Threading.Tasks;
using FluentHttpClient.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace FluentHttpClient.Entity
{
	public class FluentHttpClientContext : DbContext
	{
		public FluentHttpClientContext(DbContextOptions options)
			: base(options)
		{ }

		public DbSet<HttpRequest> HttpRequests { get; set; }

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
			//  dotnet ef migrations add InitialCreate --startup-project ../../test/FluentlyHttpClient.Test.csproj -c FluentHttpClientContext

		//	optionsBuilder.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB;Database=FluentHttpClient;Integrated Security=True;");
		//}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//var cascadeFKs = modelBuilder.Model.GetEntityTypes()
			//	.SelectMany(t => t.GetForeignKeys())
			//	.Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

			//foreach (var fk in cascadeFKs)
			//	fk.DeleteBehavior = DeleteBehavior.Restrict;

			//modelBuilder.ApplyConfiguration(new HttpRequestMapping());
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		public Task Initialize()
		{
			return Database.MigrateAsync();
		}

	}
}