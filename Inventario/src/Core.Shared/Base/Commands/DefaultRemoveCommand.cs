using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Shared.Base
{
    public class DefaultRemoveCommand<T> : IRemoveCommand where T : BaseEntity
    {
        public Guid Id { get; set; }
    }
}
