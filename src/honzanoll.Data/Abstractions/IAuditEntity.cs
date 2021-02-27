using System;

namespace honzanoll.Data.Abstractions
{
    public interface IAuditEntity
    {
        #region Properties

        DateTime Created { get; set; }

        string CreatedBy { get; set; }

        DateTime? Updated { get; set; }

        string UpdatedBy { get; set; }

        #endregion
    }
}
