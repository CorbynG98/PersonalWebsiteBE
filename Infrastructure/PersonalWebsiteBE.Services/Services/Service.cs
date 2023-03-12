using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebsiteBE.Core.Models;
using PersonalWebsiteBE.Core.Repositories;
using PersonalWebsiteBE.Core.Services;

namespace PersonalWebsiteBE.Services.Services
{
    /// <summary>
    /// These are essentially just ways to access save and find and all that from scripts, and maybe some
    /// others once added to this collection. All the services should be using the repository directly and not these helpers
    /// </summary>
    public class Service<TEntity> : IService<TEntity> where TEntity : IDocument
    {
        private IRepository<TEntity> repository;
        public Service(IRepository<TEntity> repository) {
            this.repository = repository;
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await repository.GetOneAsync(id);
        }

        public async Task SaveAsync(string id, TEntity entity)
        {
            await repository.UpdateOneAsync(id, entity);
        }
    }
}
