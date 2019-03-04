using System;
using System.Collections.Generic;
using System.Text;

namespace BatsBadmintonFixtures
{
    public interface ICacheable
    {
        string CacheItemName { get; set; }
        void SaveToCache(string cacheName, string jsonItem);
        string GetCache(string cacheName);
    }
}
