using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CompleteProject
{

    public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _projectRepository;
        public CompleteProjectHandler(IProjectRepository project)
        {
            _projectRepository = project;
        }
        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            project.Complete();
            _projectRepository.Update(project);
            return ResultViewModel.Sucess();
        }
    }
}
