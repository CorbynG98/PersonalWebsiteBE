using Google.Cloud.Firestore;
using Newtonsoft.Json;
using PersonalWebsiteBE.Core.Models;
using PersonalWebsiteBE.Core.Repositories;
using PersonalWebsiteBE.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Repository.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IDocument
    {
        protected FirestoreDb FireStoreDb;
        protected CollectionReference Collection;
        public Repository(IFireStoreSettings settings) {
            FireStoreDb = FirestoreDb.Create(settings.ProjectId);
            // Dynamically set collection reference with prefix if present
            if (string.IsNullOrWhiteSpace(settings.Root))
                Collection = FireStoreDb.Collection(typeof(TEntity).ToString().Split('.').LastOrDefault().Trim());
            else
            {
                string collectionName = typeof(TEntity).ToString().Split('.').LastOrDefault().Trim();
                Collection = FireStoreDb.Collection(settings.Root).Document(collectionName).Collection(collectionName);
            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            QuerySnapshot entityQuerySnapshot = await Collection.GetSnapshotAsync();
            List<TEntity> listEntity = new();

            foreach (DocumentSnapshot documentSnapshot in entityQuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    TEntity newEntity = documentSnapshot.ConvertTo<TEntity>();
                    newEntity.Id = documentSnapshot.Id;
                    newEntity.CreatedAt = documentSnapshot.CreateTime.Value.ToDateTime();
                    listEntity.Add(newEntity);
                }
            }
            return listEntity;
        }

        public async Task<TEntity> GetOneAsync(string id)
        {
            DocumentReference docRef = Collection.Document(id);
            DocumentSnapshot documentSnapshot = await docRef.GetSnapshotAsync();

            if (documentSnapshot.Exists)
            {
                TEntity entity = documentSnapshot.ConvertTo<TEntity>();
                entity.Id = documentSnapshot.Id;
                entity.CreatedAt = documentSnapshot.CreateTime.Value.ToDateTime();
                return entity;
            }
            else
            {
                return default;
            }
        }

        public async Task UpdateOneAsync(string id, TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            DocumentReference entityReference = Collection.Document(id);
            await entityReference.SetAsync(entity, SetOptions.Overwrite);
        }

        /// <summary>
        /// Putting a comment here so I know for future.
        /// 
        /// Create the new object and chuck it in the database. Return the id of the created object for later processing
        /// </summary>
        /// <param name="entity">Object we want to create in the database</param>
        /// <returns>Id of the object we just created</returns>
        public async Task<string> CreateOneAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            return (await Collection.AddAsync(entity)).Id;
        }

        public async Task DeleteOneAsync(string id)
        {
            DocumentReference entityReference = Collection.Document(id);
            await entityReference.DeleteAsync();
        }
    }
}
