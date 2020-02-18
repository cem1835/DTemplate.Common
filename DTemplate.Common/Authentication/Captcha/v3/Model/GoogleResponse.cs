using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Authentication.Captcha.Model
{
    public class GoogleResponse
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
