using ApiCore.Attributes;
using ApiCore.Models;

namespace ApiCoreTests.Request
{
    internal class UserRequest : RequestDefinition
    {
        [PropertyType(PropertyType.Get)]
        public string? username { get; set; }


        [PropertyType(PropertyType.Get)]
        public string? secret { get; set; }
    }
}
