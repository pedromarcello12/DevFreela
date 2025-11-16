using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastruture.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return ResultViewModel<List<Skill>>.Sucess(skills);
        }
        public ResultViewModel Insert(CreateSkillInputModel model)
        {
            var skill = new Skill(model.Description);

            _context.Skills.Add(skill);
            _context.SaveChanges();
            return ResultViewModel.Sucess();
        }
    }
}
