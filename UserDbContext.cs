using Microsoft.EntityFrameworkCore;
using ValidationApp.Models;

namespace ValidationApp
{
	public class UserDbContext : DbContext
	{
		public UserDbContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}
