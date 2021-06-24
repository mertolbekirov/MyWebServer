using Git.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;
using Git.Services;
using System.ComponentModel.DataAnnotations;
using Git.Data;
using CarShop.Services;
using System.Linq;
using Git.Data.Models;

namespace Git.Controllers
{

    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly ApplicationDbContext data;

        public UsersController(
            IValidator validator,
            IPasswordHasher passwordHasher,
            ApplicationDbContext data)
        {
            this.validator = validator;
            this.passwordHasher = passwordHasher;
            this.data = data;
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterViewModel model)
        {
            var errors = validator.ValidateUser(model);

            if (this.data.Users.Any(u => u.Username == model.Username))
            {
                errors.Add($"User with '{model.Username}' username already exists.");
            }

            if (this.data.Users.Any(u => u.Email == model.Email))
            {
                errors.Add($"User with '{model.Email}' e-mail already exists.");
            }

            if (errors.Any())
            {
                return Error(errors);
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = passwordHasher.HashPassword(model.Password),
            };

            data.Users.Add(user);
            data.SaveChanges();

            return Redirect("/Users/Login");
        }

        [HttpPost]
        public HttpResponse Login(UserLoginViewModel model)
        {
            var hashedPassword = this.passwordHasher.HashPassword(model.Password);

            var userId = data.Users
                .Where(u => u.Password == hashedPassword && u.Username == model.Username)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error("Username or password are not correct");
            }

            this.SignIn(userId);

            return Redirect("/Repositories/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return Redirect("/");
        }
    }
}
