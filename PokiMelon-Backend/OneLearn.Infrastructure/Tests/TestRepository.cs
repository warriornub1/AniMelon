using OneLearn.Application.Tests.Interface;
using OneLearn.Domain.Test;
using OneLearn.Infrastructure.Common;
using OneLearn.Infrastructure.Common.DBContexts;

namespace OneLearn.Infrastructure.Tests
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        public TestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
