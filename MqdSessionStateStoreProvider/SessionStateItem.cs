using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqdSessionStateStoreProvider
{
    internal class SessionStateItem
    {
        public Dictionary<string, SessionStateItemValue> Dict { get; set; }

        public int Timeout { get; set; }
    }
}
