using Swashbuckle.AspNetCore.Annotations;

namespace TopWay.API.TopWay.Resources;

[SwaggerSchema(Required = new[]{"Id","Name"})]

public class CategoriesResource
{
    [SwaggerSchema("Category Identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Category Name")]
    public string Name { get; set;}
}