using JiraIntegration.Core.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Entities
{
    [JsonConverter(typeof(ToStringJsonConverter))]
    public class PersistentIssue
    {
        public PersistentIssue(JiraIssue value)
        {
            Key = value.Key;
        }

        public PersistentIssue(string key)
        {
            Key = key;
        }

        public PersistentIssue() { }

        public string Key { get; }

        public override string ToString() => Key;

        public override bool Equals(object obj)
        {
            var issue = obj as PersistentIssue;
            return issue != null &&
                   Key == issue.Key;
        }

        public override int GetHashCode()
        {
            return 990326508 + EqualityComparer<string>.Default.GetHashCode(Key);
        }

        public static explicit operator PersistentIssue(JiraIssue value) => new PersistentIssue(value);
        public static implicit operator string(PersistentIssue value) => value.Key;
        public static implicit operator PersistentIssue(string value) => new PersistentIssue(value);
    }
}
