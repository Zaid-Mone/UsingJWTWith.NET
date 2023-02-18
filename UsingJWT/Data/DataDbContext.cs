using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsingJWT.Models;

namespace UsingJWT.Data
{
    public class DataDbContext:IdentityDbContext<User>
    {
        public DataDbContext(DbContextOptions<DataDbContext>options):base(options)
        { }
        public DbSet<Book> Books { get; set; }


    }
}
