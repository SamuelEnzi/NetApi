using ApiCoreTests.Model;
using ApiCoreTests.Request;

//get all used information
var server = "http://localhost/reflect.php";
var username = "secure";
var password = "password";

UserRequest request = new UserRequest();
request.secret = password;
request.username = username;

var response = await request.Extract().Submit(server);
var res = await response.Content.ReadAsStringAsync();

Console.WriteLine(res);
//if (user == null)
//    throw new Exception("login failed");
//
//Console.WriteLine($"login success!\n[{user.id}] {user.email}");
//Console.ReadLine();