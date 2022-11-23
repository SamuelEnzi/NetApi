using ApiCore.Attributes;
using ApiCore.Models;

namespace ApiCoreTests.Request
{
    [RequestMethod(ApiCore.Attributes.HttpMethod.Post)]
    internal class UserRequest : RequestDefinition
    {
        [PropertyType(PropertyType.Get, name: "User-name")]
        public string? username { get; set; }


        [PropertyType(PropertyType.Get)]
        public string? secret { get; set; }


        [PropertyType(PropertyType.Post)]
        public string? text { get; set; } = "some post info just to test";
    }
}
