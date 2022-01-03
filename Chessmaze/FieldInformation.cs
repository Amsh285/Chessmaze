using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessmaze
{
    public class FieldInformation
    {
        public FieldType Type { get; set; }
        public bool IsDiagonal { get; set; }

        public FieldInformation()
        {
            Type = FieldType.Empty;
            IsDiagonal = false;
        }
    }
}
