using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<ResultViewModel>
    {
        public UpdateProjectCommand(int id)
        {
            Id = id;
        }
        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }

    }
}
