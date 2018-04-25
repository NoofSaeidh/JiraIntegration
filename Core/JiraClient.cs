using Atlassian.Jira;
using Atlassian.Jira.Linq;
using JiraIntegration.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JiraIntegration.Core
{
    public class JiraClient
    {
        private readonly IConfig _config;

        public JiraClient(IConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void Test()
        {
            var jira = Jira.CreateRestClient(_config.JiraAddress, _config.Username, _config.Password);

        }
    }
}
