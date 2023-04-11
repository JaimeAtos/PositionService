using System;

namespace Atos.Core.EventsDTO
{
    public record CatalogLevelChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid LevelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
}

