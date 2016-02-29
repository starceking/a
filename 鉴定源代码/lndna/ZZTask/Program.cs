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

            switch (TASK_TYPE)
            {
                case "News": News.GetNews(); break;//9:21
                case "StockCancel": StockPlan.Cancel(); break;//9:30
                case "AutoStop": StockPlan.AutoStop(); break;//9:25
                case "AutoDelayRemind"://9:30
                    StockPlan.AutoDelayRemind();
                    if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SimData"]))
                        Mobile.SendSmsSync(Mobile.ADMIN_MOBILE, "费用不足-任务执行完毕");
                    break;
                case "AutoPreSell"://14:30
                    StockPlan.AutoPreSell();
                    if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SimData"]))
                        Mobile.SendSmsSync(Mobile.ADMIN_MOBILE, "自动平仓-任务执行完毕");
                    break;
                case "AutoDelay"://15:01
                    StockPlan.AutoDelay();
                    if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SimData"]))
                        Mobile.SendSmsSync(Mobile.ADMIN_MOBILE, "递延-任务执行完毕");
                    break;
                case "ZZDaily"://00:01
                    HsiDaySta.Sta(DateTime.Today.AddDays(-1));
                    StockPlanDay.Sta(DateTime.Today.AddDays(-1));
                    ZZStaUserDay.Sta(DateTime.Today.AddDays(-1));
                    if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["CmpId"]))
                    {
                        //主系统才执行如下操作
                        ZStockSettings.DelRongduan();
                        StockPlan.SetDebt();
                        if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SimData"]))
                            Mobile.SendSmsSync(Mobile.ADMIN_MOBILE, "统计-任务执行完毕");
                    }
                    break;
            }
        }
    }
}
