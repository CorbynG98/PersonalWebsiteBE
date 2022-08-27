using PersonalWebsiteBE.Core.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Core.Services.Core
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllAsync();

        Task<Project> CreateProjectAsync(Project newProject);

        Task DeleteProjectAsync(string projectId);

        Task UpdateProjectAsync(string projectId, Project newProject);
    }
}
