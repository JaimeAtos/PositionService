using Atos.Core.Abstractions;
using System;

namespace Atos.Core.Commons
{
    /// <summary>
    /// Base clase to create a person
    /// </summary>
    public abstract class Persona<Tkey, TUserKey> : IEntityBase<Tkey, TUserKey>
    {
        public Tkey Id { get; set; }
        public bool State { get; set; }
        public TUserKey UserCreatorId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
