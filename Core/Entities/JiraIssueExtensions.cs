using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Entities
{
    public static class JiraIssueExtensions
    {
        public static IEnumerable<JiraIssue> ToJiraIssues(this IEnumerable<Issue> issues)
        {
            foreach (var issue in issues)
            {
                yield return new JiraIssue(issue);
            }
        }
    }
}
