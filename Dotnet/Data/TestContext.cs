using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dotnet;

namespace Dotnet.Data
{
    public class TestContext : DbContext
    {
        public TestContext (DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public DbSet<Dotnet.Test> Test { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().HasData(
                new Test{Id = Guid.NewGuid(), Role = "user", Message = "Hello World!"},
                new Test{Id = Guid.NewGuid(), Role = "assistant", Message = "Hello Person!"},
                new Test{Id = Guid.NewGuid(), Role = "user", Message = "Goodbye!"}
            );
        }
    }
}
