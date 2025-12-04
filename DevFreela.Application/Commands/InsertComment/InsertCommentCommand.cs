using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertComment
{
    public class InsertCommentCommand : IRequest<ResultViewModel>
    {
        public InsertCommentCommand()
        {

        }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }

    }
}
