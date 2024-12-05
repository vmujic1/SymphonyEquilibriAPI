using Microsoft.AspNetCore.Mvc;
using SymphonyEquilibriAPI.Dtos.Project;
using SymphonyEquilibriAPI.Models;
using SymphonyEquilibriAPI.Models.Project;
using SymphonyEquilibriAPI.Services.ProjectService;

namespace SymphonyEquilibriAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            this._projectService = projectService;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Project>>> AddProject(AddProjectDto newProject)
        {
            return Ok(await _projectService.AddProject(newProject));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetProjectDto>>>> GetAllProjects()
        {
            return Ok(await _projectService.GetAllProjects());
        }

    }
}
