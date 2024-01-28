using Microsoft.EntityFrameworkCore;
using Project42.Models;

namespace Project42.Data
{
    public class ChoresContext : DbContext
    {
        public ChoresContext(DbContextOptions<ChoresContext> options)
            : base(options)
        {
        }

        public DbSet<ChoreAssignment> ChoreAssignmentsForWeek { get; set; }
    }
}
