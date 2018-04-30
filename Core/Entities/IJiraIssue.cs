using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Entities
{
    public interface IJiraIssue
    {
        string Key { get; set; }
        string ShortInfo { get; }
    }
}
