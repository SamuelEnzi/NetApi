# NetApi

With this library you can create api calls in a simple and structured way. With the help of attributes, classes can be converted into requests and easily forwarded to the server. You can specify requests with post body, get parameters and header values. the library is lightweight, reliable and can be used on many different types of apis.

## Fetures
- get parameter
- post parameter
- supports all http methods (standarts)
- custom header values
- strict class structure
- subdivision between request structure and response structure
- simple and fast
- built-in json parser

## Examples
Now an example for a possible structure of the api as well as the interface where we go into the following points.

- Structure
- Usage
- Api example

### Structure
I like to divide my interface into 2 namespaces. Request and Model. Where Request contains all classes that define the pure structure of the request and Model contains all classes that define the structure of the response or more complex data classes.

*[See ApiCoreTests as an example.](https://github.com/SamuelEnzi/NetApi/tree/main/Api/ApiCoreTests "see ApiCoreTests as an example")*

##### Request
let's first look at a request structure.
``` c#
[RequestMethod(ApiCore.Attributes.HttpMethod.Post)]
namespace ApiCoreTests.Request
{
    internal class UserRequest : RequestDefinition
    {
        [PropertyType(PropertyType.Get, name:"CustomName")]
        public string? username { get; set; }

        [PropertyType(PropertyType.Get)]
        public string? secret { get; set; }
    }
}
```
Each request class must be `derived` from the `RequestDefinition` class in order to implement the required basic functions. in addition, you can see that i have defined 2 `properties`. `username` and `secret`. these are both defined using the `PropertyTypeAttribute` as `Get` parameters. this way the library knows how to pass each property. So `Get` properties are passed in the url, `post` properties are passed as form body and `header` properties are embedded in the request header.

One can define the request method by adding defining the `RequestMethodAttribute`.

##### Model
however, the model of the response can become more complex. this contains all the information of the response and can be used to control query behavior.

[here is an example.](https://github.com/SamuelEnzi/NetApi/blob/main/Api/ApiCoreTests/Model/User.cs "here is an example.")

``` c#
namespace ApiCoreTests.Model
{
    public class User
    {
        public int id { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }

        public static async Task<User?> Login(string server, string username, string secret)
        {
            //create a new instance of response structure
            var request = new UserRequest 
            { 
                username = username, 
                secret = secret 
            };

            //extract and send the request asynchronously.
            var login = await request.Extract().Submit<User>(server);

            if (login.response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            //return the result component of the request
            return login.result;
        }
    }
}
```

This class defines on the one hand a `static method` to handle the login and on the other hand all `properties` of the data model of the response. first a new instance of the request is created and filled with the required information. The inherited method `Extract` is called which extracts the individual components of the request and converts them into the corresponding types of parameters (*get, post, header*). the generic method submit sends the request asynchronously and expects a `json formatted` response which is parsed into the class. 

### Usage
Now we can use the interface. to do this, we collect all the required information and pass it to the static method `login`. This performs a query and deserializes the response into a new instance of the `user` class. 

``` c#
using ApiCoreTests.Model;

//get all used information
var server = "http://localhost/endpoint.php";
var username = "secure";
var password = "password";

//call login method
var user = await User.Login(server, username!, password!);

if (user == null)
    throw new Exception("login failed");

Console.WriteLine($"login success!\n[{user.id}] {user.email}");
```

### Api example
Now a quick example of a simple api for exactly this setup. This is a very simple php script that I hosted locally using xampp just to check the functionality. 

``` php
<?php
    $username = @$_GET["username"];
    $secret = @$_GET["secret"];

    if($username == "secure" && $secret == "password") {
        $user = new stdClass();
        $user->id = 1;
        $user->username = $username;
        $user->email = "$username@validdomain.ok";
        print_r(json_encode($user, JSON_PRETTY_PRINT));
        die(http_response_code(200));
    }

    die(http_response_code(500));
?>
```
Here simply username and password are expected as defined in the request class. a user class with the properties defined in the corresponding model class is returned. afterwards the request is returned as json.
