using System;

namespace honzanoll.Data.Abstractions
{
    public interface IEntity
    {
        #region Properties

        Guid Id { get; set; }

        string Title { get; set; }

        #endregion
    }
}
