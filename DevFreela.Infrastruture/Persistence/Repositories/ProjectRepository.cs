using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastruture.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _context;
        public ProjectRepository(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project.Id;
        }

        public async Task AddComent(ProjectComment request)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);
            await _context.ProjectComments.AddAsync(request);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id ==  id);

            project.SetAsDeleted();
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _context.Projects
                            .Include(p => p.Client)
                            .Include(p => p.Freelancer)
                            .Where(p => !p.IsDeleted)
                            .ToListAsync();
        }

        public async Task<Project> GetDetailById(int id)
        {
            return await _context.Projects
                        .Include(p => p.Client)
                        .Include(p => p.Freelancer)
                        .Include(p => p.Comments)
                        .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project?> GetById(int id)
        {
            return await _context.Projects
                        .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task Update(Project request)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
            project.Update(request.Title, request.Description, request.TotalCost);
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
