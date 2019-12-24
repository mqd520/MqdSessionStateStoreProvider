using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace MqdSessionStateStoreProvider
{
    internal static class RedisUtils
    {
        private static readonly PooledRedisClientManager pool = null;


        static RedisUtils()
        {
            List<string> drHosts = new List<string>();
            List<string> dHosts = new List<string>();

            var ls = MqdSessionStateStoreProviderConfig.ConnectInfos;
            foreach (RedisConnInfo item in ls)
            {
                if (item.Type == EReadWriteType.ReadOnly)
                {
                    dHosts.Add(item.Host);
                }
                else if (item.Type == EReadWriteType.ReadWrite)
                {
                    drHosts.Add(item.Host);
                }
            }

            pool = new PooledRedisClientManager(drHosts.ToArray(), dHosts.ToArray(), new RedisClientManagerConfig()
            {
                AutoStart = true,
                MaxReadPoolSize = MqdSessionStateStoreProviderConfig.MaxRedisReadPool,
                MaxWritePoolSize = MqdSessionStateStoreProviderConfig.MaxRedisWritePool
            });
        }

        public static void SetString(string key, string value, int timeout)
        {
            IRedisClient client = pool.GetClient();
            client.Set<string>(key, value, DateTime.Now.AddMinutes(timeout));
        }

        public static string GetString(string key)
        {
            string result = "";

            IRedisClient client = pool.GetReadOnlyClient();

            try
            {
                result = client.Get<string>(key);
            }
            finally
            {
                client.Dispose();
            }

            return result;
        }

        public static void SetExpire(string key, int timeout)
        {
            IRedisClient client = pool.GetClient();

            try
            {
                client.ExpireEntryAt(key, DateTime.Now.AddMinutes(timeout));
            }
            finally
            {
                client.Dispose();
            }
        }

        public static void Remove(string key)
        {
            IRedisClient client = pool.GetClient();

            try
            {
                client.Remove(key);
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
