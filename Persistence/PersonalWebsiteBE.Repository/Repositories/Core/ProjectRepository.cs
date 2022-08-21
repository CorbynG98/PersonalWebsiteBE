using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Repository.Repositories.Core
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(IFireStoreSettings settings) : base(settings) { }
    }
}
