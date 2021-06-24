using CarShop.Services;
using Git.Data;
using Git.Data.Models;
using Git.ViewModels.Commits;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext data;
        public CommitsController(ApplicationDbContext data, IPasswordHasher passwordHasher, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse Create(string id)
        {
            var repo = data.Repositories
                .Where(r => r.Id == id)
                .FirstOrDefault();

            if (repo == null)
            {
                return Error("Repo couldn't be found");
            }

            return View(repo);
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CommitCreationFormModel model)
        {
            var errors = validator.ValidateCommit(model);

            if (errors.Any())
            {
                return Error(errors);
            }

            var commit = new Commit
            {
                CreatedOn = DateTime.Now,
                CreatorId = User.Id,
                Description = model.Description,
                RepositoryId = model.Id
            };

            data.Commits.Add(commit);
            data.SaveChanges();

            return Redirect("/Commits/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            var commits = data.Commits
                .Where(c => c.CreatorId == User.Id)
                .Select(c => new CommitAllViewModel
                {
                    CreatedOn = c.CreatedOn,
                    Description = c.Description,
                    RepositoryName = c.Repository.Name,
                    Id = c.Id
                }).ToList();


            return View(commits);
        }

        [Authorize]
        public HttpResponse Delete(string id)
        {
            var commit = data.Commits
                .Where(c => c.Id == id).FirstOrDefault();
            if (commit == null)
            {
                return Error("Commit not found");
            }

            if (commit.CreatorId != User.Id)
            {
                return Error("You are not authorized to delete this commit");
            }

            data.Remove(commit);
            data.SaveChanges();

            return Redirect("/Commits/All");
        }
    }
}
