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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IFireStoreSettings settings) : base(settings) { }

        public async Task<User> GetUserByUsernameAndPassword(string username, string password) {
            Query query = Collection
                .WhereEqualTo(nameof(User.Username), username)
                .WhereEqualTo(nameof(User.Password), password);
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

        public async Task<User> GetByUsernameOnly(string username) {
            Query query = Collection
                .WhereEqualTo(nameof(User.Username), username);
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
    }
}
