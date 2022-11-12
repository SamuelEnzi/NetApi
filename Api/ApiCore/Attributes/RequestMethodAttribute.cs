using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequestMethodAttribute : Attribute
    {

        public System.Net.Http.HttpMethod Method = System.Net.Http.HttpMethod.Get;

        public RequestMethodAttribute(HttpMethod method)
        {
            switch (method)
            {
                case HttpMethod.Get:
                    this.Method = System.Net.Http.HttpMethod.Get;
                    break;
                case HttpMethod.Post:
                    this.Method = System.Net.Http.HttpMethod.Post;
                    break;
                case HttpMethod.Put:
                    this.Method = System.Net.Http.HttpMethod.Put;   
                    break;
                case HttpMethod.Head:
                    this.Method = System.Net.Http.HttpMethod.Head;   
                    break;
                case HttpMethod.Options:
                    this.Method = System.Net.Http.HttpMethod.Options;   
                    break;
                case HttpMethod.Trace:
                    this.Method = System.Net.Http.HttpMethod.Trace;   
                    break;
                case HttpMethod.Delete:
                    this.Method = System.Net.Http.HttpMethod.Delete;
                    break;
                case HttpMethod.Default:
                    this.Method = System.Net.Http.HttpMethod.Get;
                    break;
                case HttpMethod.Patch:
                    this.Method = System.Net.Http.HttpMethod.Patch;
                    break;
                default:
                    this.Method = System.Net.Http.HttpMethod.Get;
                    break;
            }
        }
    }

    public enum HttpMethod
    {
        Get,
        Post,
        Put,
        Delete,
        Patch,
        Head,
        Trace,
        Options,
        Default
    }
}
