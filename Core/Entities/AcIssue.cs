using Atlassian.Jira;
using JiraIntegration.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Entities
{
    public class AcIssue : JiraIssue
    {
        public const string RelatedProject = "AC";

        public AcIssue(Issue issue) : base(issue)
        {

        }

        public AcIssue()
        {

        }

        public string QaVerifier { get; set; }

        protected override void FillFromIssue(Issue issue)
        {
            if (issue == null) return;
            base.FillFromIssue(issue);
            QaVerifier = issue[AcConst.CustomFields.QaVerifier]?.Value;
        }
    }
}
