using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Models
{
 public class ApiContext : DbContext
 {
     public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
     public DbSet<User> Users { get; set;}
     public DbSet<Gender> Genders { get; set;}
     public DbSet<Server> Servers { get; set;}
 }  
}