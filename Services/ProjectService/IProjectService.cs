using SymphonyEquilibriAPI.Models;
using SymphonyEquilibriAPI.Models.Project;

namespace SymphonyEquilibriAPI.Services.ProjectService
{
    public interface IProjectService
    {
        Task<ServiceResponse<Project>> AddProject(AddProjectDto newProject);

        Task<ServiceResponse<List<GetProjectDto>>> GetAllProjects();
    }
}
