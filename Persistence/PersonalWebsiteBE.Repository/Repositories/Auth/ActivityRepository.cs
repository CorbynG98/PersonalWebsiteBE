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

namespace PersonalWebsiteBE.Repository.Repositories.Auth
{
    public class ActivityRepository : Repository<AuthActivity>, IActivityRepository
    {
        public ActivityRepository(IFireStoreSettings settings) : base(settings) { }

        public async Task<List<AuthActivity>> GetAllActivitiesByUserId(string userId, int skip, int limit)
        {
            Query query = Collection
                    .WhereEqualTo(nameof(AuthActivity.UserId), userId)
                    .OrderByDescending(nameof(AuthActivity.ActionedAt))
                    .Offset(skip)
                    .Limit(limit);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            List<AuthActivity> listEntity = new();

            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    AuthActivity newEntity = documentSnapshot.ConvertTo<AuthActivity>();
                    newEntity.Id = documentSnapshot.Id;
                    newEntity.CreatedAt = documentSnapshot.CreateTime.Value.ToDateTime();
                    listEntity.Add(newEntity);
                }
            }
            return listEntity;
        }

        public async Task CreateAuthActivity(AuthActivity activity)
        {
            activity.UpdatedAt = DateTime.UtcNow;
            await Collection.AddAsync(activity);
        }
    }
}
