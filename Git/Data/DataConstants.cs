using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Data
{
    public class DataConstants
    {
        public const int UsernameMinLength = 5;
        public const int UsernameMaxLength = 20;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 20;

        public const int RepoMinLength = 3;
        public const int RepoMaxLength = 10;
        public const string Private = "Private";
        public const string Public = "Public";

        public const int CommitMinLength = 5;
    }
}
