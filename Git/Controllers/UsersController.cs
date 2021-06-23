using Git.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;


namespace Git.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return this.View();
        }

        public HttpResponse Register()
        {
            return this.View();
        }
    }
}
