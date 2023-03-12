using PersonalWebsiteBE.Core.Constants;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Settings;
using Google.Cloud.Firestore;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Repository.Repositories.Core
{
    public class EmailTemplateRepository : Repository<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(IFireStoreSettings settings) : base(settings) { }

        public async Task<EmailTemplate> GetEmailTemplateByTypeAsync(EmailTemplateTypes type)
        {
            QuerySnapshot querySnapshot = await Collection
                    .WhereEqualTo(nameof(EmailTemplate.TemplateType), type)
                    .Limit(1)
                    .GetSnapshotAsync();

            if (querySnapshot.Documents.Count == 1)
            {
                DocumentSnapshot documentSnapshot = querySnapshot.Documents.First();
                EmailTemplate entity = documentSnapshot.ConvertTo<EmailTemplate>();
                entity.Id = documentSnapshot.Id;
                return entity;
            }
            else
            {
                return default;
            }
        }
    }
}
