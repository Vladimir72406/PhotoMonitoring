using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoMonitoring.Model.Results
{
    class Result
    {
        public int code { get; set; }

        public string info { get; set; }

        public Result(int _code, string _info)
        {
            this.code = _code;
            this.info = _info;
        }

        public Result()
        {

        }
    }
}
