using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services
{
    public interface ISkillService
    {
        ResultViewModel<List<Skill>> GetAll(string search ="");
        ResultViewModel Insert(CreateSkillInputModel model);
    }
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _context;
        public SkillService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel<List<Skill>> GetAll(string search = "")
        {
            var skills = _context.Skills.ToList();
            return ResultViewModel<List<Skill>>.Success(skills);
        }
        public ResultViewModel Insert(CreateSkillInputModel model)
        {
            var skill = new Skill(model.Description);

            _context.Skills.Add(skill);
            _context.SaveChanges();
            return ResultViewModel.Success();
        }
    }
}
