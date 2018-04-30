using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Serialization
{
    public enum DisplayMode
    {
        Summary,
        Details
    }

    [Flags]
    public enum DisplayPropertyStyle
    {
        Default                 = 0b_0000_0000,
        DisplayNull             = 0b_0000_0001,
        FromNewLine             = 0b_0000_0010,
        SingleLine              = 0b_0000_0100,
        DoNotIndent             = 0b_0000_1000,
        ShowInnerValues         = 0b_0001_0000,
        ShowInnerCollection     = 0b_0010_0000 | ShowInnerValues,
    }
}
