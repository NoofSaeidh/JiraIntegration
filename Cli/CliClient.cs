using JiraIntegration.Core.Entities;
using JiraIntegration.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Cli
{
    public class CliClient
    {
        private readonly DisplaySerializer _serializer;
        
        public CliClient()
        {
            _serializer = new DisplaySerializer();
        }

        public void DisplayIssues(IEnumerable<JiraIssue> issues, DisplayMode mode = DisplayMode.Summary)
        {
            if (issues == null) return;

            foreach (var issue in issues)
            {
                DisplayIssue(issue, mode);
                Console.WriteLine();
            }
        }

        public void DisplayIssue(JiraIssue issue, DisplayMode mode = DisplayMode.Details)
        {
            Console.WriteLine(_serializer.Serialize(issue, mode));
        }
    }
}
