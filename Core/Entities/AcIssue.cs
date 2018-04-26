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
        public AcIssue(Issue issue) : base(issue)
        {

        }

        public AcIssue()
        {

        }

        protected override void FillFromIssue(Issue issue)
        {
            if (issue == null) return;
            base.FillFromIssue(issue);
            QaVerifier = issue[AcConst.CustomFields.QaVerifier]?.Value;
        }

        public override string Project
        {
            get => AcConst.AcProject;
            set {  }
        }

        public virtual string QaVerifier { get; set; }
    }
}
