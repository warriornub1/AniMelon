using OneLearn.Application.VoiceTranslation.Interface;
using OneLearn.Domain.VoiceTranslation;
using OneLearn.Infrastructure.Common;
using OneLearn.Infrastructure.Common.DBContexts;

namespace OneLearn.Infrastructure.VoiceTranslation
{
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
