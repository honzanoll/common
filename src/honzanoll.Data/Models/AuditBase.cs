using honzanoll.Data.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace honzanoll.Data.Models
{
    public class AuditBase : IAuditEntity
    {
        #region Properties

        public DateTime Created { get; set; }

        [StringLength(255)]
        public string CreatedBy { get; set; }

        public DateTime? Updated { get; set; }

        [StringLength(255)]
        public string UpdatedBy { get; set; }

        #endregion
    }
}
