using OneLearn.Application.VoiceTranslation.Interface;
using OneLearn.Domain.VoiceTranslation;
using OneLearn.Infrastructure.Common;
using OneLearn.Infrastructure.Common.DBContexts;

namespace OneLearn.Infrastructure.VoiceTranslation
{
    public class PassageRepository : GenericRepository<Passage>, IPassageRepository
    {
        public PassageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
