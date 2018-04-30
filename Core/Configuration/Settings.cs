using JiraIntegration.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Configuration
{
    public class Settings
    {
        public ICollection<PersistentIssue> Favorites { get; set; }
    }
}
