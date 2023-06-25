using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    public abstract class RedisRepository<T>
    {
        private readonly TimeSpan expiry;

        private static Lazy<ConnectionMultiplexer> LazyConnection;

        public static ConnectionMultiplexer Connection;

        public static IDatabase RedisCache => Connection.GetDatabase();

        public string prefix = "";

        public RedisRepository(string connectionString, string cacheBucket, TimeSpan lifeTime)
        {
            expiry = lifeTime;

            prefix = cacheBucket;

            if (Connection == null)
                Connection = ConnectionMultiplexer.Connect(connectionString);
        }

        public async Task<bool> ContainsKey(string key)
        {
            return await RedisCache.KeyExistsAsync(NormalizeKey(key));
        }

        public async Task<bool> DeleteFromCache(string key)
        {
            return await RedisCache.KeyDeleteAsync(NormalizeKey(key));
        }

        public async Task<T> GetFromCache(string key)
        {
            string json = await RedisCache.StringGetAsync(NormalizeKey(key));

            T value = default(T);

            if (json != null)
                value = JsonConvert.DeserializeObject<T>(json);

            return value;
        }

        public async Task<bool> SaveToCache(string key, object value)
        {
            var json = JsonConvert.SerializeObject(value, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return await RedisCache.StringSetAsync(NormalizeKey(key), json, expiry);
        }

        private string NormalizeKey(string key)
        {
            return $"{prefix}:{key.ToLower()}";
        }
    }
}
