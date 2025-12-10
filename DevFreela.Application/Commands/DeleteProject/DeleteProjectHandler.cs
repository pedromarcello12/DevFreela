using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastruture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
    {
        public const string PROJECT_NOT_FOUND_MESSAGE = "Projeto não existe.";

        private readonly IProjectRepository _repository;
        public DeleteProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Error(PROJECT_NOT_FOUND_MESSAGE);
            }

            project.SetAsDeleted();
            _projectRepository.Update(project);
            return ResultViewModel.Sucess();
        }
    }
}
