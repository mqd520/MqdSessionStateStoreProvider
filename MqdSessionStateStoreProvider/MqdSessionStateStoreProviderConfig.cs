using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqdSessionStateStoreProvider
{
    public static class MqdSessionStateStoreProviderConfig
    {
        private static IEnumerable<RedisConnInfo> _connectInfos;

        private static int _maxRedisReadPool = 20;
        private static int _maxRedisWritePool = 50;


        static MqdSessionStateStoreProviderConfig()
        {
            _connectInfos = new List<RedisConnInfo>() {
                new RedisConnInfo { Host = "123456@192.168.0.60:6379", Type = EReadWriteType.ReadWrite},
                new RedisConnInfo { Host = "123456@192.168.0.60:6379", Type = EReadWriteType.ReadOnly}
            };
        }

        public static IEnumerable<RedisConnInfo> ConnectInfos
        {
            get
            {
                return _connectInfos;
            }
        }

        public static int MaxRedisReadPool
        {
            get
            {
                return _maxRedisReadPool;
            }
        }

        public static int MaxRedisWritePool
        {
            get
            {
                return _maxRedisWritePool;
            }
        }
    }
}
