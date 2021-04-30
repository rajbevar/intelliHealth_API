using Amazon;
using Amazon.ComprehendMedical;
using Amazon.ComprehendMedical.Model;
using PatientEngTranscription.Shared.Configurations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientEngTranscription.AWSService
{
    public class ComprehendMedicalService
    {
        private IAmazonComprehendMedical comprehend { get; }
        private readonly IAmazonComprehendMedical comprehendMedicalClient;

        public IAmazonComprehendMedical AmazonComprehendMedicalClientInstance => comprehendMedicalClient;

        public ComprehendMedicalService(RootConfiguration rootConfig)
        {
            var awsConfig = rootConfig.AWSConfiguration;
            comprehendMedicalClient = new AmazonComprehendMedicalClient(awsConfig.AccessKeyId, awsConfig.AccessSecretKey, RegionEndpoint.USEast2);
        }

        public async Task<DetectEntitiesV2Response> DetectEntities(string text)
        {
            var request = new DetectEntitiesV2Request
            {
                Text = text
            };
            DetectEntitiesV2Response result = await comprehendMedicalClient.DetectEntitiesV2Async(request);
            return result;
        }


        public void Print(List<Entity> entities)
        {
            Console.WriteLine("{0} entities found", entities.Count);
            entities.ForEach(entity =>
            {
                Console.WriteLine("Attributes");
                entity.Attributes.ForEach(attr => Console.WriteLine("   {0}:{1}:{2}", attr.Type, attr.Score, attr.Text));
                Console.WriteLine("Categories");
                Console.WriteLine("   {0}:{1}:{2}", entity.Category, entity.Score, entity.Text);
                Console.WriteLine("Traits");
                entity.Traits.ForEach(trait => Console.WriteLine("  {0}:{1}", trait.Name, trait.Score));
            });
        }

    }
}
