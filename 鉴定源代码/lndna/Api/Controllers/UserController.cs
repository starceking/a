using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("User/Login")]
        public async Task<UserModel> Login(string police_no, string login_pwd)
        {
            UserModel user = await DBOper.User.Login(police_no, login_pwd);
            if (user != null)
            {
                string token = await DBOper.User.SetToken(user.id);
                user.access_id = user.id;
                user.access_token = token;
            }
            return user;
        }
        [HttpPost]
        [Route("User/Insert")]
        public async Task<string> Insert(UserModel user)
        {
            if (!await DBOper.User.GetToken(user.access_id, user.access_token)) return "请重新登录";
            return await DBOper.User.Insert(user.dept_no, user.police_no, user.login_pwd, user.name,
                user.id_card_no, user.auth_ids, user.access_id);
        }
        [HttpPut]
        [Route("User/Update")]
        public async Task<string> Update(UserModel user)
        {
            if (!await DBOper.User.GetToken(user.access_id, user.access_token)) return "请重新登录";
            return await DBOper.User.Update(user.id, user.dept_no, user.police_no, user.name, user.id_card_no,
                user.auth_ids, user.access_id);
        }
        [HttpPut]
        [Route("User/UpdatePwd")]
        public async Task<string> UpdatePwd(UserModel user)
        {
            if (!await DBOper.User.GetToken(user.access_id, user.access_token)) return "请重新登录";
            return await DBOper.User.UpdatePwd(user.access_id, user.login_pwd, user.name);
        }
        [HttpPut]
        [Route("User/ResetPwd")]
        public async Task<string> ResetPwd(UserModel user)
        {
            if (!await DBOper.User.GetToken(user.access_id, user.access_token)) return "请重新登录";
            return await DBOper.User.ResetPwd(user.id, user.login_pwd, user.access_id);
        }
        [HttpDelete]
        [Route("User/Delete")]
        public async Task<string> Delete(UserModel user)
        {
            if (!await DBOper.User.GetToken(user.access_id, user.access_token)) return "请重新登录";
            return await DBOper.User.Delete(user.id, user.access_id);
        }
        [HttpGet]
        [Route("User/GetOne")]
        public async Task<UserModel> GetOne(int id, string login_name, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return null;
            return await DBOper.User.GetOne(id, login_name);
        }
        [HttpGet]
        [Route("User/GetList")]
        public async Task<IEnumerable<UserModel>> GetList(string dept_no, string police_no, string name,
             string cg_flag, string page_size, string page_index, int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return new List<UserModel>();
            return await DBOper.User.GetList(dept_no, police_no, name, cg_flag, page_size, page_index);
        }
        [HttpGet]
        [Route("User/GetCount")]
        public async Task<long> GetCount(string dept_no, string police_no, string name, string cg_flag,
            int access_id, string access_token)
        {
            if (!await DBOper.User.GetToken(access_id, access_token)) return 0;
            return await DBOper.User.GetCount(dept_no, police_no, name, cg_flag);
        }
    }
}
