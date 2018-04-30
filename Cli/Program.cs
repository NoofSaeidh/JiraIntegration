using JiraIntegration.Core;
using JiraIntegration.Core.Common;
using JiraIntegration.Core.Configuration;
using JiraIntegration.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Alba.CsConsoleFormat;

namespace JiraIntegration.Cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var configClient = new Configurer(AppDomain.CurrentDomain.BaseDirectory))
            {
                configClient.ReadConfigs();
                var jira = new JiraClient(configClient);
                var cli = new CliClient();
                var favorites = await jira.GetFavorites();

                cli.DisplayIssues(favorites);
                cli.DisplayIssues(favorites, DisplayMode.Details);
            }
        }


    }
}
