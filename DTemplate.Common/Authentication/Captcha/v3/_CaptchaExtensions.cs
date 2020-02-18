using Autofac;
using DTemplate.Common.Authentication.Captcha.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Authentication.Captcha
{
    public static class CaptchaExtensions
    {
        public static void AddGoogleCaptchav3(this ContainerBuilder builder,GoogleReCaptchaModel model) 
        {
            builder.Register(x => model).SingleInstance();
            builder.RegisterType<GoogleCaptchaService>();
        }
    }
}
