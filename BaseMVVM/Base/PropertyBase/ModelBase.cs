using System;
using System.Collections.Generic;
using System.Text;

namespace Base.PropertyBase
{
    [Serializable]
    public abstract class ModelBase : PropChanged
    {
        public abstract string SqL { get; }
        public abstract string SqlCount { get; }
        public abstract string SqlInsert { get; }
        public abstract string SqlUpdate { get; }
        public abstract string SqlHardDelete { get; }
    }
}
