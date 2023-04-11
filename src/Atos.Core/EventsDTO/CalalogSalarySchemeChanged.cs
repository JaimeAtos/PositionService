using System;

namespace Atos.Core.EventsDTO
{
    /// <summary>
    /// 
    /// </summary>
    public record CalalogSalarySchemeChanged
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid SalarySchemeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
}
