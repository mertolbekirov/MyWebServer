using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Data.Models
{
    public class User
    {
        public User()
        {
            Repositories = new List<Repository>();
            Commits = new List<Commit>();
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Username { get; init; }

        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

        public ICollection<Repository> Repositories { get; init; }

       

        public ICollection<Commit> Commits { get; init; }
    }
}
