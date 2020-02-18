using DTemplate.Common.Authentication.Captcha.Model;
using DTemplate.Common.Serialization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DTemplate.Common.Authentication.Captcha
{
    public class GoogleCaptchaService
    {
        private GoogleReCaptchaModel _model;

        public GoogleCaptchaService(IOptions<GoogleReCaptchaModel> model)
        {
            _model = model.Value;
        }

        public virtual async Task<GoogleResponse> ReCaptchaVerification(string _Token)
        {
            GoogleReCaptchaData data = new GoogleReCaptchaData
            {
                response = _Token,
                secret = _model.SecretKey
            };

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_model.SecretKey}&response={_Token}");

                var capRes = Json.Deserialize<GoogleResponse>(response);

                return capRes;
            }
        }

    }
}
