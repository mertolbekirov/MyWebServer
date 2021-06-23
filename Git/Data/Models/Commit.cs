using System;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class Commit
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(5)]
        public string Description { get; init; }

        [Required]
        public DateTime CreatedOn { get; init; }

        [Required]
        public string CreatorId { get; init; }

        [Required]
        public User Creator { get; init; }

        [Required]
        public string RepositoryId { get; init; }

        [Required]
        public Repository Repository { get; init; }
    }
}