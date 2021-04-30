using Amazon;
using Amazon.ComprehendMedical;
using Amazon.ComprehendMedical.Model;
using Newtonsoft.Json;
using PatientEngTranscription.Shared.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PatientEngTranscription.AWSService
{
    public class ComprehendMedicalServiceMock
    {
        private readonly IAmazonComprehendMedical comprehendMedicalClient;
        private readonly RootConfiguration _rootConfiguration;
        private Rootpath _rootpath;

        public ComprehendMedicalServiceMock(RootConfiguration rootConfig)
        {
            _rootConfiguration = rootConfig;
        }


        public IAmazonComprehendMedical AmazonComprehendMedicalClientInstance => comprehendMedicalClient;


        public async Task<DetectEntitiesV2Response> DetectEntities(string text,Rootpath rootpath)
        {
            _rootpath = rootpath;
            var response = await  LoadEntityFromFile();
            response.HttpStatusCode = System.Net.HttpStatusCode.OK;

            return response;
        }

        private async Task<DetectEntitiesV2Response> LoadEntityFromFile()
        {
            DetectEntitiesV2Response result = null;
           // var fileName = GetFilePath("Data/MedicationCategory_sample1.json");
            var fileName = GetFilePath("Data/All_sample1.json");
           
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = await r.ReadToEndAsync();
                result = JsonConvert.DeserializeObject<DetectEntitiesV2Response>(json);
            }
            return result;
        }

        private string GetFilePath(string fileName)
        {
            var path = Path.Combine(_rootpath.ContentRootPath, fileName);

            if (File.Exists(path))
                return path;

            path = Path.Combine(_rootpath?.WebRootPath, fileName);

            if (File.Exists(path))
                return path;

            throw new Exception("entity file is not found");

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
