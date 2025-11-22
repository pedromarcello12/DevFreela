using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastruture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.InsertComment
{
    public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public InsertCommentHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.ProjectId);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            var comment = new ProjectComment(request.Content, request.ProjectId, request.UserId);

            await _context.ProjectComments.AddAsync(comment);

            await _context.SaveChangesAsync();
            return ResultViewModel.Sucess();
        }
    }
}
