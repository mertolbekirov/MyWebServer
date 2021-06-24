using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.ViewModels.Commits
{
    public class CommitAllViewModel
    {
        public string RepositoryName { get; init; }

        public string Description { get; init; }

        public DateTime CreatedOn { get; init; }

        public string Id { get; init; }
    }
}
