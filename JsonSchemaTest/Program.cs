using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manatee.Json;
using Manatee.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using ValidateSchema;
using JsonSchema = Manatee.Json.Schema.JsonSchema;

namespace JsonSchemaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ManateeValidation();
            var validCount = Validate.ManateeValidation();
            Console.WriteLine(validCount);
        }

        public static void ManateeValidation()
        {
            var schemaContent = File.ReadAllText(@"D:\code\test\JsonSchemaTest\JsonSchemaTest\schema.json");

            var schemaValue = JsonValue.Parse(schemaContent);
            var schema = new JsonSchema();
            schema.FromJson(schemaValue, new JsonSerializer());

            var total = 0;
            var validCount = 0;
            using (var rd = new StreamReader(@"D:\code\test\JsonSchemaTest\JsonSchemaTest\oneksrcdata.tsv"))
            {
                string line;
                while ((line = rd.ReadLine()) != null)
                {
                    var strs = line.Split('\t');
                    if (strs.Length >= 2)
                    {
                        total++;
                        var json = JsonValue.Parse(strs[1]);

                        var validateResult = schema.Validate(json);

                        if (validateResult.IsValid)
                        {
                            validCount++;
                        }
                    }

                }
            }

            Console.WriteLine("Total: {0}, valid Count: {1}", total, validCount);
        }

        public static void NewTonValidation()
        {
            var schemaContent = File.ReadAllText(@"D:\code\test\JsonSchemaTest\JsonSchemaTest\schema.json");
            // load schema
            JSchema schema = JSchema.Parse(schemaContent);

            var total = 0;
            var validCount = 0;
            using (var rd = new StreamReader(@"D:\code\test\JsonSchemaTest\JsonSchemaTest\oneksrcdata.tsv"))
            {
                string line;
                while ((line = rd.ReadLine()) != null)
                {
                    var strs = line.Split('\t');
                    if (strs.Length >= 2)
                    {
                        total++;
                        JToken json = JToken.Parse(strs[1]);

                        // validate json
                        IList<ValidationError> errors;
                        bool valid = json.IsValid(schema, out errors);

                        if (valid)
                        {
                            validCount++;
                        }
                    }

                }
            }

            Console.WriteLine("Total: {0}, valid Count: {1}", total, validCount);
        }
    }
}
