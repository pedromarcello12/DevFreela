using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _projectRepository;
        public UpdateProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var exists = await _projectRepository.Exists(request.Id);


            if (!exists)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }
            var project = await _projectRepository.GetById(request.Id);
            project.Update(request.Title, request.Description,request.TotalCost);
            _projectRepository.Update(project);
            return ResultViewModel.Sucess();
        }
    }
}
