using Linux_EnvironmentVariables_Demo;

var envManager = new EnvironmentManager();
envManager.Set("NCB_URL", "https://nextcodeblock.com");
Console.WriteLine("Variable was set");

var url = envManager.Get("NCB_URL");
Console.WriteLine($"The url is: {url}");
