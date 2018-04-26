using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Common
{

    [Serializable]
    public class JiraIntegrationException : Exception
    {
        public JiraIntegrationException() { }
        public JiraIntegrationException(string message) : base(message) { }
        public JiraIntegrationException(string message, Exception inner) : base(message, inner) { }
    }
}
