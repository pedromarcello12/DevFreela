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
        private readonly IProjectRepository _projectRepository;
        public DeleteProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _projectRepository.GetById(request.Id).Result;
            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            project.SetAsDeleted();
            await _projectRepository.Update(project);
            return ResultViewModel.Sucess();
        }
    }
}
