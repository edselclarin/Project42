using Microsoft.EntityFrameworkCore;
using Project42.Models;

namespace Project42.Data
{
    public class ChoreBoardService
    {
        private readonly ChoresContext dbContext_;

        public ChoreBoardService(ChoresContext choresContext)
        {
            dbContext_ = choresContext;
        }

        public Task<List<ChoreAssignment>> GetAsync()
        {
            return dbContext_.ChoreAssignmentsForTheWeek.ToListAsync();
        }
    }
}
