using System;

namespace Core.Shared.Entities.Security
{
    public class CurrentRoles
    {
        #region [ Properties ]

        public Guid SystemId { get; set; }

        public string Code { get; set; }

        #endregion

        #region [ Ctor ]

        public CurrentRoles()
        {

        }

        #endregion
    }
}
