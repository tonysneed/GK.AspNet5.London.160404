using System;
using Microsoft.Extensions.PlatformAbstractions;

public class Program
{
    public Program(IApplicationEnvironment env)
    {
        Console.WriteLine($"App Name: {env.ApplicationName}");
        Console.WriteLine($"App Version: {env.ApplicationVersion}");
        Console.WriteLine($"App Path: {env.ApplicationBasePath}");
        Console.WriteLine($"Runtime: {env.RuntimeFramework}");
    }
    
    public void Main(string[] args)
    {
        string culture = null;
        if (args.Length > 0)
            culture = args[0];        
        var greeting = new Greeter().Greet(culture);
        Console.WriteLine($"{greeting} DNX!");
    }
}