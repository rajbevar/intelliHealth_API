using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Shared.Configurations
{
    public class RootConfiguration
    {
        private IConfiguration _config;

        public RootConfiguration(IOptions<AWSConfiguration> awsConfig , IConfiguration config)
        {
            AWSConfiguration = awsConfig.Value;
            _config = config;
        }

        public AWSConfiguration AWSConfiguration { get; set; }
        public bool AWSAIServiceEnabled
        {
            get
            {
                bool result = false;
                var strVal = _config["AWSAIServiceEnabled"];
                bool.TryParse(strVal, out result);
                return result;
            }
        }

        public bool MLStudioServiceEnabled
        {
            get
            {
                bool result = false;
                var strVal = _config["MLStudioServiceEnabled"];
                bool.TryParse(strVal, out result);
                return result;
            }
        }

    }

    public class AWSConfiguration
    {

        public string AccessKeyId { get; set; }
        public string AccessSecretKey { get; set; }
        public string ComprehendServiceRegion { get; set; }

    }

    public class Rootpath
    {
        public string ContentRootPath { get; set; }
        public string WebRootPath { get; set; }

        public string BaseDirectoryPath { get; set; }
    }

}
