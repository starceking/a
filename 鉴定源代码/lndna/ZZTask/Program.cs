using DBOper;
using System.Configuration;
using System.Collections.Generic;
using Util;
using System;

namespace ZZTask
{
    class Program
    {
        static readonly string TASK_TYPE = ConfigurationManager.AppSettings["TaskType"];

        static void Main(string[] args)
        {
            Redis.Init();
        }
    }
}
