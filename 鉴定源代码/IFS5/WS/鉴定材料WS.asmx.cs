using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using DAL;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using LIB;

namespace WS
{
    /// <summary>
    /// ok
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class 鉴定材料WS : System.Web.Services.WebService
    {
        [WebMethod]
        public string Insert(string ID, string 是否样本, string 委托编号, string 名称, string 数量, string 重量,
            string 包装, string 性质, string 提取人, string 提取方法, string 提取位置, string 提取时间, string 备注, string 材料编号, string 单位)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("ID", ID);
            dict.Add("是否样本", 是否样本);
            dict.Add("委托编号", 委托编号);
            dict.Add("名称", 名称);
            dict.Add("数量", 数量);
            dict.Add("重量", 重量);
            dict.Add("包装", 包装);
            dict.Add("性质", 性质);
            dict.Add("提取人", 提取人);
            dict.Add("提取方法", 提取方法);
            dict.Add("提取位置", 提取位置);
            dict.Add("提取时间", 提取时间);
            dict.Add("备注", 备注);
            dict.Add("材料编号", 材料编号);
            dict.Add("单位", 单位);
            return DBHelperSQL.Insert("鉴定材料", dict);
        }
        [WebMethod]
        public string Update(string ID, string 材料编号, string 名称, string 数量, string 重量,
            string 包装, string 性质, string 提取人, string 提取方法, string 提取位置, string 提取时间, string 备注, string 单位)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("材料编号", 材料编号);
            dict.Add("名称", 名称);
            dict.Add("数量", 数量);
            dict.Add("重量", 重量);
            dict.Add("包装", 包装);
            dict.Add("性质", 性质);
            dict.Add("提取人", 提取人);
            dict.Add("提取方法", 提取方法);
            dict.Add("提取位置", 提取位置);
            dict.Add("提取时间", 提取时间);
            dict.Add("备注", 备注);
            dict.Add("单位", 单位);
            return DBHelperSQL.Update("鉴定材料", "ID='" + ID + "'", dict);
        }
        [WebMethod]
        public string Delete(string ID)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("鉴定材料", "ID，" + ID);
            return DBHelperSQL.Delete(dict);
        }
        [WebMethod]
        public string GetAll(string 委托编号, string 是否样本)
        {
            string filter = string.Empty;
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";
            if (是否样本.Length > 0) filter += "是否样本='" + 是否样本 + "' and ";
            return DBHelperSQL.Select("鉴定材料", Helper.CutFilter(filter), "材料编号,创建时间", "*").GetXml();
        }
        [WebMethod]
        public string Query(string 委托编号, string 是否样本, string 名称, string 重量s, string 重量e, string 包装, string 性质,
            string 受理时间s, string 受理时间e, string 材料编号, string 鉴定专业, string 鉴定类别, string 鉴定项目, string 一检人, string 鉴定单位, 
            string pageSize, string pageIndex)
        {
            string filter = string.Empty;
            if (鉴定单位.Length > 0) filter += "鉴定单位='" + 鉴定单位 + "' and ";
            if (委托编号.Length > 0) filter += "委托编号='" + 委托编号 + "' and ";
            if (是否样本.Length > 0) filter += "是否样本='" + 是否样本 + "' and ";
            if (材料编号.Length > 0) filter += "材料编号 like '%" + 材料编号 + "%' and ";
            if (名称.Length > 0) filter += "名称 like '%" + 名称 + "%' and ";
            if (包装.Length > 0) filter += "包装 like '%" + 包装 + "%' and ";
            if (性质.Length > 0) filter += "性质 like '%" + 性质 + "%' and ";
            if (重量s.Length > 0) filter += "重量>='" + 重量s + "' and ";
            if (重量e.Length > 0) filter += "重量<='" + 重量e + "' and ";
            if (受理时间s.Length > 0) filter += "受理时间>='" + 受理时间s + "' and ";
            if (受理时间e.Length > 0) filter += "受理时间<='" + 受理时间e + "' and ";
            if (鉴定专业.Length > 0) filter += "鉴定专业='" + 鉴定专业 + "' and ";
            if (鉴定类别.Length > 0) filter += "鉴定类别='" + 鉴定类别 + "' and ";
            if (鉴定项目.Length > 0) filter += "鉴定项目='" + 鉴定项目 + "' and ";
            if (一检人.Length > 0) filter += "一检人='" + 一检人 + "' and ";
            return DBHelperSQL.SelectRowCount("鉴定材料视图", Helper.CutFilter(filter), "材料编号,创建时间", "*",
                Convert.ToInt32(pageSize), Convert.ToInt32(pageIndex)).GetXml();
        }
        [WebMethod]
        public string SavePicture(int pic_width, int pic_height, string bitmap_data, string fileName,
            string F1, string F2, string F3, string F4, string F5, string F6)
        {
            try
            {
                Bitmap m_pic = new Bitmap(pic_width, pic_height);
                string[] m_tempPics = bitmap_data.Split(',');

                for (int i = 0; i < pic_width; i++)
                {
                    for (int j = 0; j < pic_height; j++)
                    {
                        uint pic_argb = (uint)long.Parse(m_tempPics[i * pic_height + j]);
                        int pic_a = (int)(pic_argb >> 24 & 0xFF);
                        int pic_r = (int)(pic_argb >> 16 & 0xFF);
                        int pic_g = (int)(pic_argb >> 8 & 0xFF);
                        int pic_b = (int)(pic_argb & 0xFF);

                        m_pic.SetPixel(i, j, Color.FromArgb(pic_a, pic_r, pic_g, pic_b));
                    }
                }

                string filePath = ConfigurationManager.AppSettings["WebPath"] + F1;
                if (F2.Length > 0) filePath += "\\" + F2;
                if (F3.Length > 0) filePath += "\\" + F3;
                if (F4.Length > 0) filePath += "\\" + F4;
                if (F5.Length > 0) filePath += "\\" + F5;
                if (F6.Length > 0) filePath += "\\" + F6;

                //判断路径是否存在,若不存在则创建路径
                DirectoryInfo upDir = new DirectoryInfo(filePath);
                if (!upDir.Exists)
                {
                    upDir.Create();
                }

                if (fileName.Length == 0)
                {
                    Random objRand = new Random();
                    DateTime date = DateTime.Now;
                    string saveName = date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + Convert.ToString(objRand.Next(99) * 97 + 100);
                    fileName = saveName + ".jpg";
                }
                else
                {
                    fileName = fileName + ".jpg";
                }

                m_pic.Save(filePath + "\\" + fileName, ImageFormat.Jpeg);
                return "保存成功";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [WebMethod]
        public string TesOper(string 委托编号, string 物证处置, string 领物人, string 领物时间, string 物证处置备注)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("物证处置", 物证处置);
            dict.Add("领物人", 领物人);
            dict.Add("领物时间", 领物时间);
            dict.Add("物证处置备注", 物证处置备注);
            return DBHelperSQL.Update("鉴定流程", "委托编号='" + 委托编号 + "'", dict);
        }
    }
}
