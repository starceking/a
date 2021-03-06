﻿using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DBOper
{
    public static class IdEpSample
    {
        public const string TABLE = "id_ep_sample";

        public static async Task<string> InsertOne(ulong id_ep_id, ulong case_sample_id, string position, int amount, int user_id)
        {
            if (id_ep_id <= 0 || case_sample_id <= 0 || user_id <= 0)
                return "参数不全";
            IdEpModel ipm = await IdEp.GetOne(id_ep_id);
            if (ipm == null) return "读取不到MODEL";
            if (ipm.user_id != user_id) return "权限异常";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("id_ep_id", id_ep_id.ToString());
            dict.Add("case_sample_id", case_sample_id.ToString());
            if (!string.IsNullOrWhiteSpace(position)) dict.Add("position", position);
            dict.Add("amount", amount.ToString());
            dict.Add("user_id", user_id.ToString());
            await DBHelper.Insert(TABLE, dict);

            return string.Empty;
        }
        public static async Task<string> Update(ulong id, string position, int amount, int user_id)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(position))
                return "参数不全";
            IdEpSampleModel cem = await GetOne(id);
            if (cem == null) return "读取不到MODEL";
            if (cem.user_id != user_id) return "权限不足";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("position", position);
            dict.Add("amount", amount.ToString());
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            fdict.Add("user_id", user_id.ToString());
            if ((await DBHelper.Update(TABLE, dict, fdict, "and")) <= 0)
            {
                return "异常，请重试";
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<string> Delete(ulong id, int user_id)
        {
            if (id <= 0) return "参数不全";
            IdEpSampleModel cem = await GetOne(id);
            if (cem == null) return "读取不到MODEL";
            if (cem.user_id != user_id) return "权限不足";

            IDictionary<string, string> fdict = new Dictionary<string, string>();
            fdict.Add("id", id.ToString());
            fdict.Add("user_id", user_id.ToString());
            if ((await DBHelper.Delete(TABLE, fdict, "and")) <= 0)
            {
                return "异常，请重试";
            }

            await DeleteRedis(id);
            return string.Empty;
        }
        public static async Task<int> Delete(ulong id, ulong id_ep_id, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (id > 0) fdict.Add("id", id.ToString());
            if (id_ep_id > 0) fdict.Add("id_ep_id", id_ep_id.ToString());
            return await DBHelper.Delete(TABLE, fdict, "and", dbConnection, trans);
        }
        public static async Task<IEnumerable<IdEpSampleModel>> GetList(string id_ep_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(id_ep_id)) fdict.Add("id_ep_id", id_ep_id);
            return await DBHelper.GetList<IdEpSampleModel, long>(TABLE, "*", "id", fdict, "and");
        }
        public static async Task<IdEpSampleModel> GetOne(ulong id)
        {
            return await DBHelper.GetOne<IdEpSampleModel>(id.ToString());
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<IdEpSampleModel>(id.ToString());
        }
        public static async Task<int> Insert(ulong id_ep_id, ulong case_sample_id, int user_id, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (id_ep_id <= 0 || case_sample_id <= 0 || user_id <= 0) return -1;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("id_ep_id", id_ep_id.ToString());
            dict.Add("case_sample_id", case_sample_id.ToString());
            dict.Add("user_id", user_id.ToString());
            return (await DBHelper.Insert(TABLE, dict, dbConnection, trans));
        }
    }
}
