using Core.Shared.Base;
using System;

namespace Core.Shared
{
    /// <summary>
    /// Interface usada para ser extendida nas Interface finais de Update, Remove, Return
    /// </summary>
    public interface IChangeCommand
    {
        #region [ Properties ]

        Guid Id { get; set; }

        #endregion
    }
}
