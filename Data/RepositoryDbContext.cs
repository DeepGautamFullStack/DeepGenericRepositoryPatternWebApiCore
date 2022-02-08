using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeepGenericRepositoryPatternWebApiCore.Models;

    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext (DbContextOptions<RepositoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<DeepGenericRepositoryPatternWebApiCore.Models.Customer> Customer { get; set; }

        public DbSet<DeepGenericRepositoryPatternWebApiCore.Models.Order> Order { get; set; }
    }
