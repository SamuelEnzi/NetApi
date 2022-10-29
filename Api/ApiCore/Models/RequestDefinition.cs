using ApiCore.Attributes;
using ApiCore.Com;
using System.Reflection;

namespace ApiCore.Models
{
    /// <summary>
    /// defines basic structure of a request; call <see cref="Extract"/> to reflect all properties with defined <see cref="PropertyTypeAttribute"/> into new <see cref="Request"/> object;
    /// </summary>
    public class RequestDefinition
    {
        /// <summary>
        /// extracts a <see cref="Request"/> object using the defined properties in this object;
        /// </summary>
        /// <returns>new <see cref="Request"/> containing all request information; run <see cref="Request.Submit(string)"/> to execute request</returns>
        public virtual Request Extract()
        {
            var request = new Request();
            foreach (var prop in this.GetType().GetProperties())
                prop.GetCustomAttributes(true)
                    .Where((x) => x.GetType() == typeof(PropertyTypeAttribute))
                    .ToList()
                    .ForEach((x) => Insert(x, request, prop));
            return request;
        }

        /// <summary>
        /// inserts extracted properties into <see cref="Request"/> model
        /// </summary>
        /// <param name="x"></param>
        /// <param name="request"></param>
        /// <param name="prop"></param>
        private void Insert(object x, Request request, PropertyInfo prop)
        {
            switch (((PropertyTypeAttribute)x).Type)
            {
                case PropertyType.Header:
                    request.AppendHeader((prop.Name, prop.GetValue(this, null)?.ToString()));
                    break;
                case PropertyType.Post:
                    request.AppendPost((prop.Name, prop.GetValue(this, null)?.ToString()));
                    break;
                case PropertyType.Get:
                    request.AppendGet((prop.Name, prop.GetValue(this, null)?.ToString()));
                    break;
                default:
                    break;
            }
        }
    }
}
