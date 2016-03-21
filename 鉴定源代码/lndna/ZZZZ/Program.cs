using DBOper;
using Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Util;

namespace ZZZZ
{
    class Program
    {
        static void Main(string[] args)
        {
            User.DeleteRedis(1);
            Console.Read();
        }
    }
}
