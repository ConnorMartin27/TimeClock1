using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeClock.Models;

namespace TimeClock.Data
{
    public class TimeClockContext : DbContext
    {
        public TimeClockContext (DbContextOptions<TimeClockContext> options)
            : base(options)
        {
        }

        public DbSet<TimeClock.Models.Entry> Entry { get; set; } = default!;
    }
}
