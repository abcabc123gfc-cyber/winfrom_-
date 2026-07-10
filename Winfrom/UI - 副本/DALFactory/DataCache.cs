using System.Web;

namespace DALFactory
{
    /// <summary>
	/// 缓存操作类，其他的缓存技术：https://blog.csdn.net/wybshyy/article/details/142630116
	/// </summary>
	public class DataCache
    {
        /// <summary>
        /// 获取当前应用程序指定cacheKey的cache值
        /// </summary>
        /// <param name="cacheKey">缓存的键</param>
        /// <returns>cache值</returns>
        public static object GetCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定cacheKey的cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string cacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject);
        }
    }
}
