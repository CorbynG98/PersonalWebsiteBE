using PersonalWebsiteBE.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PersonalWebsiteBE.Core.Services
{
    public interface IService<TEntity> where TEntity : IDocument
    {
        Task<TEntity> GetByIdAsync(string id);
        Task SaveAsync(string id, TEntity entity);
    }
}
