using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public interface IUserService
    {
        ResultViewModel<UserViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateUserInputModel model);
        ResultViewModel InsertSkills(UserSkillsInputModel model);
        ResultViewModel InsertProfilePicture(int id, IFormFile file);

    }
    public class UserService : IUserService
    {

        private readonly DevFreelaDbContext _context;
        public UserService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel<UserViewModel> GetById(int id)
        {
            var user = _context.Users
                        .Include(u => u.Skills)
                        .ThenInclude(u => u.Skill)
                        .SingleOrDefault(u => u.Id == id);

            if (user is null)
            {
                return ResultViewModel<UserViewModel>.Error("Usuário não encontrado");
            }

            var model = UserViewModel.FromEntity(user);
            return ResultViewModel<UserViewModel>.Success(model);
        }
        public ResultViewModel<int> Insert(CreateUserInputModel model)
        {
            var user = new User(model.FullName, model.Email, model.BirthDate);

            _context.Users.Add(user);
            _context.SaveChanges();
            return ResultViewModel<int>.Success(user.Id);
        }
        public ResultViewModel InsertSkills(UserSkillsInputModel model)
        {
            var userSkills = model.SkillIds.Select(s => new UserSkill(model.Id, s)).ToList();

            _context.UserSkills.AddRange(userSkills);
            _context.SaveChanges();
            return ResultViewModel.Success();
        }
        public ResultViewModel InsertProfilePicture(int id, IFormFile file)
        {
            var description = $"FIle: {file.FileName}, Size: {file.Length}";
            return ResultViewModel.Success();
        }
    }
}
