using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class Repository
    {
        public Repository()
        {
            Commits = new List<Commit>();
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Name { get; init; }

        [Required]
        public DateTime CreatedOn { get; init; }

        public bool IsPublic { get; init; }

        [Required]
        public string OwnerId { get; init; }

        [Required]
        public User Owner { get; init; }

        public ICollection<Commit> Commits { get; init; }

        
    }
}