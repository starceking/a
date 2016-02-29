using System;

namespace Model
{
    public class JsonResultModel<T>
    {
        public JsonResultModel() { }
        public JsonResultModel(bool _success, string _msg, T _data)
        {
            success = _success;
            msg = _msg;
            data = _data;
        }
        public bool success { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }
}
