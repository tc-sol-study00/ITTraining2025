using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASPEnshu.Models;

namespace ASPEnshu.Data
{
    public class ASPEnshuContext : DbContext
    {
        public ASPEnshuContext (DbContextOptions<ASPEnshuContext> options)
            : base(options)
        {
        }

        public DbSet<ASPEnshu.Models.Employee> Employee { get; set; } = default!;
    }
}
