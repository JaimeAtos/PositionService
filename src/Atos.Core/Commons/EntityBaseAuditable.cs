using Atos.Core.Abstractions;
using Atos.Core.Commons;
using System;

namespace Atos.Core.Common
{
    public abstract class EntityBaseAuditable<TKey, TUserKey> : EntityBase<TKey, TUserKey>, IEntityBaseAuditable<TKey, TUserKey>
    {
        public TUserKey? UserModifierId { get; set; }
        public DateTime? DateLastModify { get; set; }
    }
}
