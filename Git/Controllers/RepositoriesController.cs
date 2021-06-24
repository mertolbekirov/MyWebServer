using CarShop.Services;
using Git.Data;
using Git.Data.Models;
using Git.ViewModels.Repositories;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Linq;

namespace Git.Controllers
{
    using static DataConstants;
    public class RepositoriesController : Controller
    {

        private readonly IValidator validator;
        private readonly ApplicationDbContext data;

        public RepositoriesController(ApplicationDbContext data, IPasswordHasher passwordHasher, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }


        public HttpResponse All()
        {
            var repositoriesQuery = this.data
               .Repositories
               .AsQueryable();

            if (this.User.IsAuthenticated)
            {
                repositoriesQuery = repositoriesQuery
                    .Where(r => r.IsPublic || r.OwnerId == this.User.Id);
            }
            else
            {
                repositoriesQuery = repositoriesQuery
                    .Where(r => r.IsPublic);
            }

            var repositores = repositoriesQuery
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new RepositoryListingViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Owner = r.Owner.Username,
                    CreatedOn = r.CreatedOn.ToLocalTime().ToString("F"),
                    Commits = r.Commits.Count()
                })
                .ToList();

            return View(repositores);
        }


        public HttpResponse Create()
        {
            if (!User.IsAuthenticated)
            {
                return Error("You are not authenticated to create repos");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Create(RepositoryCreationFormModel model)
        {
            var errors = validator.ValidateRepository(model);

            if (errors.Any())
            {
                return Error(errors);
            }

            var repo = new Repository
            {
                CreatedOn = DateTime.Now,
                IsPublic = model.RepositoryType == Public ? true : false,
                Name = model.Name,
                OwnerId = User.Id
            };

            data.Repositories.Add(repo);
            data.SaveChanges();

            return Redirect("All");
        }
    }
}
