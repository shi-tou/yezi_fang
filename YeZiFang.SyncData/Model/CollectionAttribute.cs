using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YeZiFang.SyncData.Model
{

    /// <summary>
    /// 实体与数据表的映射
    /// </summary>
    public class CollectionAttribute : Attribute
    {
        /// <summary>
        /// 注入属性值
        /// </summary>
        /// <param name="collectionName">映射到的Collection</param>
        public CollectionAttribute(string collectionName)
        {
            this.CollectionName = collectionName;
        }
        /// <summary>
        /// CollectionName
        /// </summary>
        public string CollectionName { set; get; }

    }
}

