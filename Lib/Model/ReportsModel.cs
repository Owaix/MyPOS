using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
    public interface ReportsModel
    {
        int Rows { get; set; }
        float RowHeight { get; set; }
        String item { get; set; }
    }
}
