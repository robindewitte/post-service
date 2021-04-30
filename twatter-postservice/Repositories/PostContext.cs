using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using twatter_postservice.DataModels;

namespace twatter_postservice.Repositories
{
    public class PostContext: DbContext
    {
        public PostContext(DbContextOptions<PostContext> options)
           : base(options)
        { 
        }

        public DbSet<Post> Posts { get; set; }


    }
}
