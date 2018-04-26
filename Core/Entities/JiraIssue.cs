using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Entities
{
    public class JiraIssue
    {
        public JiraIssue(Issue issue)
        {
            FillFromIssue(issue);
        }

        public JiraIssue() { }


        protected virtual void FillFromIssue(Issue issue)
        {
            if (issue == null) return;

            Key = issue.Key.Value;
            Project = issue.Project;
            Reporter = issue.Reporter;
        }

        public virtual string Key { get; set; }
        public virtual string Project { get; set; }
        public virtual string Reporter { get; set; }
        public virtual string Assignee { get; set; }
        public virtual string Priority { get; set; }

    }
}
