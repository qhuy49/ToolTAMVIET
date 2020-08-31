using System.Net;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MinvoiceLoadDataMisa.Config;
using Newtonsoft.Json.Linq;

namespace MinvoiceLoadDataMisa.Services
{
    public class LoginService
    {
        private static JObject Login(string username, string password)
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            client.Headers.Add("Content-Type", "application/json; charset=utf-8");
            JObject json = new JObject
            {
                {"username",username },
                {"password",password },
                {"ma_dvcs","VP" }
            };

            var urlLogin = BaseConfig.UrlLogin;
            var token = client.UploadString(urlLogin, json.ToString());
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            return JObject.Parse(token);
        }

        public static void CreateAuthorization(WebClient webClient, string username, string pass)
        {
            try
            {
                var tokenJson = Login(username, pass);
                if (tokenJson!=null)
                {
                    var authorization = "Bear " + tokenJson["token"] + ";VP;vi";
                    webClient.Headers[HttpRequestHeader.Authorization] = authorization;
                }
               
            }
            catch (System.Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
