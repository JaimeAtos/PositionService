using System;

namespace Atos.Core.Abstractions
{
    /// <summary>
    /// Definición para las entidades base del patron de repositorio
    /// </summary>
    /// <typeparam name="TKey">Tipo de dato que identificara el registro</typeparam>
    /// <typeparam name="TUserKey">Tipo de datos que identificara el usuario</typeparam>
    public interface IEntityBaseAuditable<TKey, TUserKey> : IEntityBase<TKey, TUserKey>
    {
        /// <summary>
        /// Id del usuario que creo el registro
        /// </summary>
        public TUserKey UserModifierId { get; set; }
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime? DateLastModify { get; set; }
    }
}
