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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IFireStoreSettings settings) : base(settings) { }

        public async Task<User> GetUserByUsernameAndPassword(string username, string password) {
            Query query = FireStoreDb.Collection(Collection)
                .WhereEqualTo("Username", username)
                .WhereEqualTo("Password", password);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Documents.Count == 1)
            {
                DocumentSnapshot documentSnapshot = querySnapshot.Documents.First();
                User entity = documentSnapshot.ConvertTo<User>();
                entity.Id = documentSnapshot.Id;
                entity.CreatedAt = documentSnapshot.CreateTime.Value.ToDateTime();
                return entity;
            }
            else
            {
                return default;
            }
        }

        public async Task CreateLoginActivityAsync(string id, AuthActivity activity)
        {
            CollectionReference entityReference = FireStoreDb.Collection($"User/{id}/AuthActivity");
            await entityReference.AddAsync(activity);
        }
    }
}
