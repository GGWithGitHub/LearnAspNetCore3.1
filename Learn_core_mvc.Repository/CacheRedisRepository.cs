using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    public class CacheRedisRepository<T> : RedisRepository<T>, ICacheRepository<T>
    {
        public CacheRedisRepository(string connectionString, string cacheBucket, TimeSpan lifeTime) : base(connectionString, cacheBucket, lifeTime)
        {

        }

        public async Task<bool> Delete(string key)
        {
            return await DeleteFromCache(key);
        }

        public async Task<bool> KeyExists(string key)
        {
            return await ContainsKey(key);
        }

        public async Task<T> Retrieve(string key)
        {
            return await GetFromCache(key);
        }

        public async Task<bool> Save(string key, T obj)
        {
            return await SaveToCache(key, obj);
        }
    }
}
