using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Authentication.Captcha.Model
{
    public class GoogleReCaptchaData
    {
        public string response { get; set; } // Token
        public string secret { get; set; }
    }
}
