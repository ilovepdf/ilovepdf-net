using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILovePDF.Model.Enum
{
    public enum ConformanceValues
    {
        [Description("pdfa-1b")]
        Pdfa1b,
        [Description("pdfa-1a")]
        pdfa1a,
        [Description("pdfa-2b")]
        pdfa2b,
        [Description("pdfa-2u")]
        pdfa2u,
        [Description("pdfa-2a")]
        pdfa2a,
        [Description("pdfa-3b")]
        pdfa3b,
        [Description("pdfa-3u")]
        pdfa3u,
        [Description("pdfa-3a")]
        pdfa3a,
    }
}
