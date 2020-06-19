using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoMonitoring.Model.Results
{
    class ResultSettings : Result
    {
        public ResultSettings()
        {

        }

        public ResultSettings(int _code, string _info) : base(_code, _info)
        {

        }
        public Settings settings { get; set; }
    }
}
