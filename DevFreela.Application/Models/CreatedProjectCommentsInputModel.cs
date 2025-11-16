using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class CreatedProjectCommentsInputModel
    {
        public required string Content { get; set; }

        public int IdProject { get; set; }
        public int IdUser { get; set; }
        public ProjectComment ToEntity()
            => new ProjectComment(Content, IdProject, IdUser);
    }
}
