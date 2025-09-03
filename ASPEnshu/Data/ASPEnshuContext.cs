using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASPEnshu.Models.DTOs;

namespace ASPEnshu.Data
{
    public class ASPEnshuContext : DbContext
    {
        public ASPEnshuContext (DbContextOptions<ASPEnshuContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; } = default!;
    }
}
