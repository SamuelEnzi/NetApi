using ApiCoreTests.Request;
using System.Text.Json.Serialization;

namespace ApiCoreTests.Model
{
    public class User
    {
        public int id { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }


        /// <summary>
        /// sends a login request to the server. the request structure is defined in <see cref="UserRequest"/>
        /// </summary>
        /// <param name="server">represents the url of the server</param>
        /// <param name="username">username</param>
        /// <param name="secret">user password</param>
        /// <returns>if respons is <see cref="System.Net.HttpStatusCode.OK"/> than a new instance of a <see cref="User"/> is returned. else result is null.</returns>
        public static async Task<User?> Login(string server, string username, string secret)
        {
            //create a new instance of response structure
            var request = new UserRequest 
            { 
                username = username, 
                secret = secret 
            };

            //extract and send the request asynchronously. expect the result to be a json in format of this class.
            var login = await request.Extract().Submit<User>(server);

            if (login.response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            //return the result component of the request
            return login.result;
        }
    }
}
