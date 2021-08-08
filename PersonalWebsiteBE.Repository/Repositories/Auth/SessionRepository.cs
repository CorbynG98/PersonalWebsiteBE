using Google.Cloud.Firestore;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Repositories.Auth;
using PersonalWebsiteBE.Core.Settings;
using PersonalWebsiteBE.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Services.Repositories.Auth
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(IFireStoreSettings settings) : base(settings) { }

        public async Task<Session> GetSessionByTokenAsync(string sessionToken)
        {
            Query query = FireStoreDb.Collection(Collection)
                    .WhereEqualTo("SessionToken", sessionToken);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count == 1)
            {
                DocumentSnapshot documentSnapshot = querySnapshot.Documents.First();
                Session entity = documentSnapshot.ConvertTo<Session>();
                entity.Id = documentSnapshot.Id;
                entity.CreatedAt = documentSnapshot.CreateTime.Value.ToDateTime();
                return entity;
            }
            else
            {
                return default;
            }
        }
    }
}
