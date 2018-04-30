using JiraIntegration.Core.Entities;
using JiraIntegration.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Serialization
{
    public class DisplaySerializer
    {
        private const string indent = "   ";

        public string Serialize(IJiraIssue issue, DisplayMode mode)
        {
            if (issue == null) return null;
            switch (mode)
            {
                case DisplayMode.Details:
                    return issue.ShortInfo 
                        + Environment.NewLine + Environment.NewLine 
                        + string.Join(Environment.NewLine, Serialize(issue))
                        + Environment.NewLine + Environment.NewLine;
                case DisplayMode.Summary:
                default:
                    return issue.ShortInfo;
            }
        }

        private IEnumerable<string> Serialize(object value)
        {
            var type = value.GetType();
            var propsAll = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                .Select(p => new
                {
                    property = p,
                    display = p.GetCustomAttribute<DisplayPropertyAttribute>()
                    // this is simpler to have all props with attribute and check it further
                        ?? new DisplayPropertyAttribute()
                })
                .Where(p => !p.display.Hidden)
                .OrderBy(p => p.display.Order);


            foreach (var prop in propsAll)
            {
                object resObj;
                var name = prop.display.Name ?? prop.property.Name;
                try
                {
                    resObj = prop.property.GetValue(value);
                }
                catch
                {
                    continue;
                }
                var propString = indent + name + " : ";
                var styles = prop.display.DisplayStyle;
                var resSb = new StringBuilder();
                if (resObj != null)
                {
                    if (styles.HasFlag(DisplayPropertyStyle.FromNewLine))
                    {
                        resSb.AppendLine();
                        resSb.AppendLine();
                    }

                    if (styles.HasFlag(DisplayPropertyStyle.SingleLine))
                    {
                        resSb.AppendLine(resObj.ToString()
                                               .Replace("\r", "")
                                               .Replace("\n", ""));
                    }

                    else
                    {
                        if (styles.HasFlag(DisplayPropertyStyle.DoNotIndent))
                        {
                            resSb.AppendLine(resObj.ToString());
                        }

                        else
                        {
                            resSb.Append(resObj.ToString()
                                               .Replace("\n", "\n" + indent));
                        }
                    }
                    yield return propString + resSb.ToString();
                    // todo: show inner values
                }
                else if (styles.HasFlag(DisplayPropertyStyle.DisplayNull))
                {
                    yield return propString;
                }
            }
        }
    }
}
