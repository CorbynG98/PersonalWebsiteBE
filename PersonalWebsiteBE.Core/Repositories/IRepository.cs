using Google.Cloud.Firestore;
using PersonalWebsiteBE.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : IDocument
    {
        Task<List<TEntity>> GetManyAsync();
        Task<TEntity> GetOneAsync(string id);
        Task UpdateOneAsync(string id, TEntity entity);
        Task<string> CreateOneAsync(TEntity entity);
        Task DeleteOneAsync(string id);
    }
}
