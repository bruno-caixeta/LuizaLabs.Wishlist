using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Wishlist.App.ResponseModels
{
    public class ResponseModel<TModel>
    {
        public ResponseModel() { }

        public ResponseModel(TModel data)
        {
            this.data = data;
            error = false;
        }

        public ResponseModel(TModel data, bool error, IList<ErrorInfo> errorInfo)
        {
            this.data = data;
            this.error = error;
            this.errorInfo = errorInfo;
        }

        public ResponseModel(bool error, IList<ErrorInfo> errorInfo)
        {
            data = default(TModel);
            this.error = error;
            this.errorInfo = errorInfo;
        }

        public ResponseModel(TModel data, Meta meta)
        {
            this.data = data;
            this.meta = meta;
        }

        public TModel data { get; set; }
        public bool error { get; set; }
        public IList<ErrorInfo> errorInfo { get; set; }
        public Meta meta { get; set; }

        public void CalculateMeta(int pageNumber, int pageSize, int total)
        {
            meta = new Meta();
            meta.CalculateMeta(pageNumber, pageSize, total);
        }
    }
}
