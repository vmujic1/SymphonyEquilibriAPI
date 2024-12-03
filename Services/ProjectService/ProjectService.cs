using SymphonyEquilibriAPI.Data;
using SymphonyEquilibriAPI.Models;
using SymphonyEquilibriAPI.Models.Project;

namespace SymphonyEquilibriAPI.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly DataContext _dataContext;

        public ProjectService(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<ServiceResponse<Project>> AddProject(AddProjectDto newProject)
        {
            var serviceResponse = new ServiceResponse<Project>();
            var project = new Project
            {
                Name = newProject.Name,
                Industry = newProject.Industry,
            };
            _dataContext.Projects.Add(project);
            await _dataContext.SaveChangesAsync();
            serviceResponse.Data = project;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProjectDto>>> GetAllProjects()
        {
            var serviceResponse = new ServiceResponse<List<GetProjectDto>>();
            try
            {
                var projects = await _dataContext.Projects.Include(p => p.Employees).ToListAsync();
                serviceResponse.Data = projects.Select(p => new GetProjectDto(p)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
