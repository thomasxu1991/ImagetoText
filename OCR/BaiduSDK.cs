using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OCR
{
    class BaiduSDK
    {
        private string key = SettingValue.key;
        private string secret = SettingValue.secret;
        private string id = SettingValue.id;
        public class xJsonModel
        {
            public string words { get; set; }
        }

        public class xModle
        {
            public int log_id { get; set; }
            public int words_result_num { get; set; }
            public List<xJsonModel> words_result { get; set; }
        }
        public string GeneralBasicDemo(string type,string value)
        {
            byte[] image = new byte[1];
            var client = new Baidu.Aip.Ocr.Ocr(this.key, this.secret);
            client.Timeout = 60000;
            if (type=="screen")
            {
                image = File.ReadAllBytes("screen.jpg");
            }
            if (type == "file")
            {
                image = File.ReadAllBytes(value);
            }
            var result = client.GeneralBasic(image);
            
            if (type == "url")
            {
                result = client.GeneralBasicUrl(value);
            }
            Console.WriteLine(result);
            JObject jObject = JObject.Parse(result.ToString());
            if (jObject.Property("error_code") != null)
            {
                return jObject["error_msg"].ToString();
            }
            var list = jObject["words_result"];
            var returnResult = "";
            foreach (JObject ll in list)
            {
                returnResult += ll["words"]+"\r\n";
            }
            return returnResult;
        }
 
    }

}
