using Atos.Core.Abstractions;
using System;

namespace Atos.Core.Commons
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUserKey"></typeparam>
    public abstract class EntityBase<TKey, TUserKey> : IEntityBase<TKey, TUserKey>
    {
        public TKey Id { get; set; }
        public bool State { get; set; }
        public TUserKey UserCreatorId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}