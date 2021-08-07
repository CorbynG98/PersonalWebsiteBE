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
        protected string Collection;
        public Repository(IFireStoreSettings settings) {
            FireStoreDb = FirestoreDb.Create(settings.ProjectId);
            Collection = typeof(TEntity).ToString().Split('.').LastOrDefault().Trim();
        }

        public async Task<List<TEntity>> GetManyAsync()
        {
            Query entityQuery = FireStoreDb.Collection(Collection);
            QuerySnapshot entityQuerySnapshot = await entityQuery.GetSnapshotAsync();
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
            DocumentReference docRef = FireStoreDb.Collection(Collection).Document(id);
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
            DocumentReference entityReference = FireStoreDb.Collection(Collection).Document(id);
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
            CollectionReference entityReference = FireStoreDb.Collection(Collection);
            return (await entityReference.AddAsync(entity)).Id;
        }

        public async Task DeleteOneAsync(string id)
        {
            DocumentReference entityReference = FireStoreDb.Collection(Collection).Document(id);
            await entityReference.DeleteAsync();
        }
    }
}
