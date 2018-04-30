using Atlassian.Jira;
using Atlassian.Jira.Linq;
using JiraIntegration.Core.Configuration;
using JiraIntegration.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JiraIntegration.Core
{
    public class JiraClient
    {
        private readonly Configurer _configurer;
        private IAuthConfig _authConfig => _configurer.AuthConfig;
        private Settings _settings => _configurer.Settings;
        private readonly Jira _client;
        public JiraClient(Configurer configurer)
        {
            _configurer = configurer ?? throw new ArgumentNullException(nameof(configurer));
            _configurer.CheckInitialization();
            _client = Jira.CreateRestClient(_authConfig.JiraAddress, _authConfig.Username, _authConfig.Password);
        }

        public async Task<JiraIssue> GetIssue(string key)
        {
            return new JiraIssue(await _client.Issues.GetIssueAsync(key));
        }

        public void AddToFavorites(JiraIssue issue)
        {
            if (issue == null)
                throw new ArgumentNullException(nameof(issue));

            var pi = (PersistentIssue)issue;
            if (!_settings.Favorites.Contains(pi))
                _settings.Favorites.Add(pi);
            _configurer.SaveConfigs();
        }

        public void AddToFavorites(string issueKey)
        {
            if (issueKey == null)
                throw new ArgumentNullException(nameof(issueKey));

            if (!_settings.Favorites.Contains(issueKey))
                _settings.Favorites.Add(issueKey);
            _configurer.SaveConfigs();
        }

        public async Task<IEnumerable<JiraIssue>> GetFavorites()
        {
            return (await _client
                .Issues
                .GetIssuesAsync(_settings.Favorites.Select(x => (string)x)))
                .Values
                .ToJiraIssues();
        }
    }
}
