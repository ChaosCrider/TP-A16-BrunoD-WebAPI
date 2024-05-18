using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TP_A16_BrunoD_WebAPI.Models;

namespace TP_A16_BrunoD_WebAPI.Data
{
    public class TP_A16_BrunoD_WebAPIContext : DbContext
    {
        public TP_A16_BrunoD_WebAPIContext (DbContextOptions<TP_A16_BrunoD_WebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<TP_A16_BrunoD_WebAPI.Models.Ability> Ability { get; set; } = default!;
        public DbSet<TP_A16_BrunoD_WebAPI.Models.Beast> Beast { get; set; } = default!;
    }
}
