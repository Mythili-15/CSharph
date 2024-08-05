using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlineArrayDemo
{
    [System.Runtime.CompilerServices.InlineArray(10)]
    public struct Buffer
    {
        private int _element0;
    }
}
