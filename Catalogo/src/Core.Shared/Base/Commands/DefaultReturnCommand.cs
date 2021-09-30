using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Shared.Base
{
    public class DefaultReturnCommand<T> : IReturnCommand where T : BaseEntity
    {
        public Guid Id { get; set; }
    }
}
