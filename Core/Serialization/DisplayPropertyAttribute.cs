using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Serialization
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DisplayPropertyAttribute : Attribute
    {
        public DisplayPropertyAttribute()
        {

        }

        public DisplayPropertyAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public DisplayPropertyStyle DisplayStyle { get; set; }
        public bool Hidden { get; set; }
        public int Order { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HiddenPropertyAttribute : DisplayPropertyAttribute
    {
        public HiddenPropertyAttribute()
        {
            Hidden = true;
        }
    }
}
