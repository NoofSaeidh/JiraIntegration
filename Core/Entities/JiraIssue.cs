using Atlassian.Jira;
using JiraIntegration.Core.Serialization;

namespace JiraIntegration.Core.Entities
{
    public class JiraIssue : IJiraIssue
    {
        public JiraIssue(Issue issue)
        {
            FillFromIssue(issue);
        }

        public JiraIssue()
        {
        }

        [DisplayProperty(Hidden = true)]
        public string Key { get; set; }

        public string Assignee { get; set; }

        [DisplayProperty(Order = +256, 
            DisplayStyle = DisplayPropertyStyle.FromNewLine
                         | DisplayPropertyStyle.DoNotIndent
                         | DisplayPropertyStyle.DisplayNull)]
        public string Description { get; set; }

        public string Priority { get; set; }

        public string Project { get; set; }

        public string Reporter { get; set; }

        public string Status { get; set; }

        [DisplayProperty(Hidden = true)]
        public string Summary { get; set; }

        [DisplayProperty(Hidden = true)]
        public virtual string ShortInfo => $"{Key} - {Summary}";

        protected virtual void FillFromIssue(Issue issue)
        {
            if (issue == null) return;

            Key = issue.Key.Value;
            Project = issue.Project;
            Reporter = issue.Reporter;
            Assignee = issue.Assignee;
            Priority = issue.Priority.Name;
            Status = issue.Status.Name;
            Summary = issue.Summary;
            Description = issue.Description;
        }

        public override string ToString() => ShortInfo;

        public static JiraIssue ParseFromProject(Issue issue)
        {
            if (issue == null)
                return new JiraIssue();

            switch (issue.Project)
            {
                case AcIssue.RelatedProject:
                    return new AcIssue(issue);

                default:
                    return new JiraIssue(issue);
            }
        }
    }
}