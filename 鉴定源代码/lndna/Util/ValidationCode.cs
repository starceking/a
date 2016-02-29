using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;

namespace Util
{
    public class ValidationCode
    {
        static bool SIM_DATA = (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SimData"]));
        #region verify code
        async Task<string> SetCode(string uuid)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            string code = r.Next(1000, 9999).ToString();
            await Redis.InsertString("VERIFY_CODE_" + uuid, code, new TimeSpan(0, 10, 0));

            Log.Info("SetCode:" + uuid, code);

            return code;
        }
        public static async Task<bool> GetCode(string uuid, string code)
        {
            if (SIM_DATA) return true;

            Log.Info("GetCode:" + uuid, code);

            string key = "VERIFY_CODE_" + uuid;
            StackExchange.Redis.RedisValue val = await Redis.GetString(key);
            if (val.HasValue && val.ToString().Equals(code))
            {
                await Redis.DeleteKey(key);
                return true;
            }
            return false;
        }
        #endregion

        //用户存取验证码字符串
        public string validationCode = string.Empty;

        Graphics g = null;

        int bgWidth = 0;
        int bgHeight = 0;

        public string FontFace = "Comic Sans MS";
        public int FontSize = 40;
        public Color foreColor = Color.FromArgb(220, 220, 220);
        public Color backColor = Color.FromArgb(190, 190, 190);
        public Color mixedLineColor = Color.FromArgb(220, 220, 220);
        public int mixedLineWidth = 1;
        public int mixedLineCount = 5;

        #region 根据指定长度及背景图片样式，返回带有随机验证码的图片对象
        /// <summary>
        /// 根据指定长度及背景图片样式，返回带有随机验证码的图片对象
        /// </summary>
        /// <param >指定长度</param>
        /// <param >背景图片样式</param>
        /// <returns>Image对象</returns>
        public Image NextImage(int length, HatchStyle hatchStyle, bool allowMixedLines, string uuid)
        {
            this.validationCode = SetCode(uuid).Result;

            //校验码字体
            Font myFont = new Font(FontFace, FontSize);

            //根据校验码字体大小算出背景大小
            bgWidth = (int)myFont.Size * length + 4;
            bgHeight = (int)myFont.Size * 2;
            //生成背景图片
            Bitmap myBitmap = new Bitmap(bgWidth, bgHeight);

            g = Graphics.FromImage(myBitmap);


            this.DrawBackground(hatchStyle);
            this.DrawValidationCode(this.validationCode, myFont);
            if (allowMixedLines)
                this.DrawMixedLine();

            return (Image)myBitmap;
        }
        #endregion


        #region 内部方法：绘制验证码背景
        private void DrawBackground(HatchStyle hatchStyle)
        {
            //设置填充背景时用的笔刷
            HatchBrush hBrush = new HatchBrush(hatchStyle, backColor);

            //填充背景图片
            g.FillRectangle(hBrush, 0, 0, this.bgWidth, this.bgHeight);
        }
        #endregion


        #region 内部方法：绘制验证码
        private void DrawValidationCode(string vCode, Font font)
        {
            g.DrawString(vCode, font, new SolidBrush(this.foreColor), 2, 2);
        }
        #endregion


        #region 内部方法：绘制干扰线条
        /// <summary>
        /// 绘制干扰线条
        /// </summary>
        private void DrawMixedLine()
        {
            for (int i = 0; i < mixedLineCount; i++)
            {
                g.DrawBezier(new Pen(new SolidBrush(mixedLineColor), mixedLineWidth), RandomPoint(), RandomPoint(), RandomPoint(), RandomPoint());
            }
        }
        #endregion


        #region 内部方法：产生随机数和随机点

        /// <summary>
        /// 返回一个随机点，该随机点范围在验证码背景大小范围内
        /// </summary>
        /// <returns>Point对象</returns>
        private Point RandomPoint()
        {
            Thread.Sleep(15);
            Random ram = new Random();
            Point point = new Point(ram.Next(this.bgWidth), ram.Next(this.bgHeight));
            return point;
        }
        #endregion
    }
}
