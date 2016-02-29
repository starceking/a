using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    public class HsiImgController : ApiController
    {
        [HttpPost]
        [Route("HsiImg/Insert/{id}")]
        public async Task<string> Insert(string id)
        {
            var disk = GetDisk(id);

            var provider = new MultipartFormDataStreamProvider(disk);
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (MultipartFileData file in provider.FileData)
            {
                //文件路径
                try
                {
                    var path = file.LocalFileName;
                    File.Copy(path, disk + file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty).Replace("'", string.Empty), true);
                    File.Delete(path);
                }
                catch
                {
                }
            }
            return string.Empty;
        }
        [HttpGet]
        [Route("HsiImg/GetList")]
        public async Task<IList<HsiImgModel>> GetList(string id, int access_id, string access_token)
        {
            if (!await DBOper.SysUser.GetToken(access_id, access_token)) return new List<HsiImgModel>();
            var disk = GetDisk(id);
            var url = ConfigurationManager.AppSettings["WebAddr"] + "HsiImg/" + id + "/";

            string[] files = Directory.GetFiles(disk);
            IList<HsiImgModel> list = new List<HsiImgModel>();
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.Exists)
                {
                    HsiImgModel model = new HsiImgModel() { url = url + fi.Name, disk = fi.FullName.Replace(@"\", @"\\") };
                    list.Add(model);
                }
            }
            HsiImgModel lm = new HsiImgModel() { url = "ins" };
            list.Add(lm);
            return list;
        }
        [HttpDelete]
        [Route("HsiImg/Delete")]
        public async Task<string> Delete(HsiImgModel model)
        {
            if (!await DBOper.SysUser.GetToken(model.access_id, model.access_token)) return "请重新登录";
            try
            {
                File.Delete(model.disk);
            }
            catch
            {

            }
            return string.Empty;
        }
        string GetDisk(string id)
        {
            var disk = ConfigurationManager.AppSettings["WebDisk"] + "HsiImg\\" + id + "\\";
            if (!Directory.Exists(disk))
            {
                Directory.CreateDirectory(disk);
            }
            return disk;
        }
    }
}
