using Atos.Core.Abstractions;
using System;

namespace Atos.Core.Commons
{
    public abstract class PersonaAuditable<TKey, TUserKey> : Persona<TKey, TUserKey>, IEntityBaseAuditable<TKey, TUserKey>
    {
        public TUserKey UserModifierId { get; set; }
        public DateTime? DateLastModify { get; set; }
    }
}
