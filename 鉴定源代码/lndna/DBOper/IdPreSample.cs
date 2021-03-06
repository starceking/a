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
    public static class IdPreSample
    {
        public const string TABLE = "id_pre_sample";

        public static async Task<string> InsertOne(ulong id_pre_id, ulong case_sample_id, string id_method, string id_result, int user_id)
        {
            if (id_pre_id <= 0 || case_sample_id <= 0 || user_id <= 0)
                return "参数不全";
            IdPreModel ipm = await IdPre.GetOne(id_pre_id);
            if (ipm == null) return "读取不到MODEL";
            if (ipm.user_id != user_id) return "权限异常";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("id_pre_id", id_pre_id.ToString());
            dict.Add("case_sample_id", case_sample_id.ToString());
            if (!string.IsNullOrWhiteSpace(id_method)) dict.Add("id_method", id_method);
            if (!string.IsNullOrWhiteSpace(id_result)) dict.Add("id_result", id_result);
            dict.Add("user_id", user_id.ToString());
            await DBHelper.Insert(TABLE, dict);

            return string.Empty;
        }
        public static async Task<string> Update(ulong id, string id_method, string id_result, int user_id)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(id_method) || string.IsNullOrWhiteSpace(id_result))
                return "参数不全";
            IdPreSampleModel cem = await GetOne(id);
            if (cem == null) return "读取不到MODEL";
            if (cem.user_id != user_id) return "权限不足";

            IDictionary<string, string> dict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(id_method)) dict.Add("id_method", id_method);
            if (!string.IsNullOrWhiteSpace(id_result)) dict.Add("id_result", id_result);
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
            IdPreSampleModel cem = await GetOne(id);
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
        public static async Task<int> Delete(ulong id, ulong id_pre_id, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (id > 0) fdict.Add("id", id.ToString());
            if (id_pre_id > 0) fdict.Add("id_pre_id", id_pre_id.ToString());
            return await DBHelper.Delete(TABLE, fdict, "and", dbConnection, trans);
        }
        public static async Task<IEnumerable<IdPreSampleModel>> GetList(string id_pre_id)
        {
            IDictionary<string, string> fdict = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(id_pre_id)) fdict.Add("id_pre_id", id_pre_id);
            return await DBHelper.GetList<IdPreSampleModel, long>(TABLE, "*", "id", fdict, "and");
        }
        public static async Task<IdPreSampleModel> GetOne(ulong id)
        {
            return await DBHelper.GetOne<IdPreSampleModel>(id.ToString());
        }
        public static async Task DeleteRedis(ulong id)
        {
            if (id <= 0) return;
            await Redis.Delete<IdPreSampleModel>(id.ToString());
        }
        public static async Task<int> Insert(ulong id_pre_id, ulong case_sample_id, int user_id, MySqlConnection dbConnection, MySqlTransaction trans)
        {
            if (id_pre_id <= 0 || case_sample_id <= 0 || user_id <= 0) return -1;

            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("id_pre_id", id_pre_id.ToString());
            dict.Add("case_sample_id", case_sample_id.ToString());
            dict.Add("user_id", user_id.ToString());
            return (await DBHelper.Insert(TABLE, dict, dbConnection, trans));
        }
    }
}
