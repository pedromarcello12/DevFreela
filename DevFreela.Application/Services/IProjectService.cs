using Azure;
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastruture.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
    public interface IProjectService
    {
        ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 0);
        ResultViewModel<ProjectViewModel> GetById(int id);
        ResultViewModel<int> Insert(CreateProjectInputModel inputModel);
        ResultViewModel Update(UpdateProjectInputModel inputModel);
        ResultViewModel Delete(int id);
        ResultViewModel Start(int id);
        ResultViewModel Complete(int id);
        ResultViewModel InsertComment(CreateProjectCommentInputModel inputModel);


    }
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _context;
        public ProjectService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel Complete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            project.Complete();
            _context.Projects.Update(project);
            _context.SaveChanges();
            return ResultViewModel.Sucess();
        }

        public ResultViewModel Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChanges();
            return ResultViewModel.Sucess();
        }

        public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 0)
        {
            var projects = _context.Projects
                            .Include(p => p.Client)
                           .Include(p => p.Freelancer)
                           .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                            .Skip(page * size)
                            .Take(size)
                            .ToList();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Sucess(model);
        }

        public ResultViewModel<ProjectViewModel> GetById(int id)
        {
            var project = _context.Projects
                        .Include(p => p.Client)
                        .Include(p => p.Freelancer)
                        .Include(p => p.Comments)
                        .SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");
            }

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Sucess(model);
        }

        public ResultViewModel<int> Insert(CreateProjectInputModel model)
        {
            var project = model.ToEntity();

            _context.Projects.Add(project);
            _context.SaveChanges();

            return ResultViewModel<int>.Sucess(project.Id);
        }

        public ResultViewModel InsertComment(CreateProjectCommentInputModel inputModel)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == inputModel.IdProject);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);

            _context.ProjectComments.Add(comment);

            _context.SaveChanges();
            return ResultViewModel.Sucess();
        }

        public ResultViewModel Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            project.Start();
            _context.Projects.Update(project);
            _context.SaveChanges();
            return ResultViewModel.Sucess();
        }

        public ResultViewModel Update(UpdateProjectInputModel inputModel)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == inputModel.IdProject);

            if (project == null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);

            _context.Projects.Update(project);
            _context.SaveChanges();

            return  ResultViewModel.Sucess();
        }
    }
}
