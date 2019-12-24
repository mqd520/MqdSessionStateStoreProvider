using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqdSessionStateStoreProvider
{
    internal class SessionStateItemValue
    {
        public object Value { get; set; }

        public Type Type { get; set; }
    }
}
