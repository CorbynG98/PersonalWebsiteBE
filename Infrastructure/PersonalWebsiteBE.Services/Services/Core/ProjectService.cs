using Microsoft.Extensions.Options;
using PersonalWebsiteBE.Core.Models.Auth;
using PersonalWebsiteBE.Core.Models.Core;
using PersonalWebsiteBE.Core.Repositories.Core;
using PersonalWebsiteBE.Core.Services.Core;
using PersonalWebsiteBE.Core.Settings;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsiteBE.Services.Services.Core
{
    public class ProjectService : Service<Project>, IProjectService
    {
        // Injected stuff
        private readonly IProjectRepository projectRepository;

        public ProjectService(IProjectRepository projectRepository) : base(projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<List<Project>> GetAllAsync() {
            return await projectRepository.GetAllAsync();
        }

        public async Task<Project> CreateProjectAsync(Project newProject)
        {
            var newProjectId = await projectRepository.CreateOneAsync(newProject);
            return await projectRepository.GetOneAsync(newProjectId);
        }

        public async Task DeleteProjectAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProjectAsync(string projectId, Project newProject)
        {
            throw new NotImplementedException();
        }
    }
}
