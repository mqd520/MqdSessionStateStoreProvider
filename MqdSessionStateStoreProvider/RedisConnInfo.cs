using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqdSessionStateStoreProvider
{
    public class RedisConnInfo
    {
        public string Host { get; set; }

        public EReadWriteType Type { get; set; }
    }
}