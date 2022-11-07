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
Console.ReadLine();