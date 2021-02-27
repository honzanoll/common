using honzanoll.Data.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace honzanoll.Data.Models
{
    public class ModelBase : AuditBase, IEntity, IAuditEntity
    {
        #region Properties

        [Key]
        public Guid Id { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        #endregion
    }
}
