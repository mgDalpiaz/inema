using System;

namespace Core.Shared.Base
{
    public class DefaultListCommand<T> : IListCommand where T : BaseEntity
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public bool ListAll { get; set; }
        public Guid? UnitId { get; set; }

        public string Order { get; set; }
        public string ColumnOrder { get; set; }

    }
}
