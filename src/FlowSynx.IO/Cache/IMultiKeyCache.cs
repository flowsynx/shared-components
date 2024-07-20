using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSynx.IO.Cache;

public interface IMultiKeyCache<in TPrimaryKey, in TSecondaryKey, TValue>: ICache
{
    TValue? Get(TPrimaryKey primaryKey, TSecondaryKey secondaryKey);
    void Set(TPrimaryKey primaryKey, TSecondaryKey secondaryKey, TValue value);
    int Count(TPrimaryKey primaryKey);
}