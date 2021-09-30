using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace CC.Common.SchemaFilters
{
    public class EnumStringSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                model.Enum.Clear();
                int index = 0;
                var values = Enum.GetValues(context.Type);

                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(n => {
                        model.Enum.Add(new OpenApiString($"{Convert.ToInt32(values.GetValue(index)).ToString()} = {n}"));
                        index++;
                     });
            }
        }
    }
}
