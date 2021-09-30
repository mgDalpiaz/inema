using System;
using System.Collections;

namespace Core.Shared.Messages
{
    public class ResponsePaginated<T> : ResponseMessage<T> where T : class, IList
    {
        #region [ Properties ]

        public int Page { get; set; }

        public int Rows { get; set; }

        public int TotalRows { get; set; }

        public int PageSize { get; set; }

        #endregion

        #region [ Calculate Properties ]

        public int TotalPage => this.Rows <= 0 ? 1 : ((this.TotalRows / this.PageSize) + Convert.ToInt32(this.TotalRows % this.PageSize > 0));


        #endregion


        #region [ Ctor ]

        public ResponsePaginated(T result)
            : base()
        {
            this.Data = result;
        }

        public ResponsePaginated()
            : base()
        {
        }

        public ResponsePaginated(int page, int rows, int pageSize, int totalRows)
        {
            this.Page = page;
            this.PageSize = pageSize;
            this.Rows = rows;
            this.TotalRows = totalRows;
        }

        public ResponsePaginated(T data, int page, int pageSize, int totalRows)
        {
            this.Page = page;
            this.PageSize = pageSize;
            this.Rows = data.Count;
            this.TotalRows = totalRows;
            this.Data = data;
        }

        #endregion

    }
}
