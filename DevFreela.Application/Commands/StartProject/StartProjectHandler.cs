using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _projectRepospitory;
        public StartProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepospitory = projectRepository;
        }
        public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _projectRepospitory.GetById(request.Id).Result;

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            project.Start();
            _projectRepospitory.Update(project);
            return ResultViewModel.Sucess();
        }
    }
}
