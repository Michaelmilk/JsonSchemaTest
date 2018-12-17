using System;
using System.IO;
using Manatee.Json;
using Manatee.Json.Schema;
using Manatee.Json.Serialization;

namespace ValidateSchema
{
    public class Validate
    {
        public static int ManateeValidation()
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

            return validCount;
        }
    }
}
