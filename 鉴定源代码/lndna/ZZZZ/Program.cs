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
            string sql = "select sum(money_debt) from stock_plan where plan_status_id=3";
            if (!string.IsNullOrWhiteSpace(User.CMP_ID)) sql += (" and cmp_id=" + User.CMP_ID);
            decimal money_debt = DBHelper.GetSumFreeSync<decimal>(sql);

            Console.WriteLine(money_debt);
            Console.Read();
        }
    }
}
