using System;

public class Greeter
{
    public string Greet(string culture)
    {
        switch (culture)
        {
            case "aussie":
                return "Good Day";
            case "british":
                return "Cheers";
            case "western":
                return "Howdy";
            case "eastern":
                return "Yo";
            default:
                return "Hello";
        }
    }
}