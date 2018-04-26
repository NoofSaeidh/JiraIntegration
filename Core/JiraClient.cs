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
        private readonly ConfigClient _configClient;
        private IAuthConfig _authConfig => _configClient.AuthConfig;
        private Settings _settings => _configClient.Settings;
        private readonly Jira _client;
        public JiraClient(ConfigClient configClient)
        {
            _configClient = configClient ?? throw new ArgumentNullException(nameof(configClient));
            _configClient.CheckInitialization();
            _client = Jira.CreateRestClient(_authConfig.JiraAddress, _authConfig.Username, _authConfig.Password);
        }

        public void Test()
        {
            var tmp = _client.Issues.GetIssueAsync("AC-104862").Result;
            var relations = tmp.GetIssueLinksAsync().Result;
        }
    }
}
