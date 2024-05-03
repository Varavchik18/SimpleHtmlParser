using DemoConsoleAppWebClient;
using System;
using System.Net;

class Program
{
    static async Task Main(string[] args)
    {
        WebHelper webHelper = new();
        var url = GetUrlFromUserInput();
        try
        {
            var htmlData = await webHelper.RetrieveWebPageByUrlIfUrlIsValid(url);
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine("Url should not be null or empty" +
            $"{exception.Message}" + $"{exception.Data}");
        }
        catch (WebException exception)
        {
            Console.WriteLine("Hmmm .... Something is wrong with the URL you provided. Here is some message errors, maybe it will be clear to you what is wrong: " +
                $"\n\n {exception.Message}" +
                $"\n {exception.Data}" +
                $"\n {exception.Status}" +
                $"\t {exception.Response}");
        }

        while (true)
        {
            var actionCode = GetActionFromUser();
            UserMenu(actionCode);
        }
    }

    private static void UserMenu(int actionCode)
    {
        switch (actionCode)
        {
            case 1:
                Console.WriteLine($"Total number is: {100}");
                break;
            case 2:
                Console.WriteLine($"The tag is: {120}");
                break;
            case 3:
                Console.WriteLine($"The total execution time to retrieve data is: {123}ms");
                break;
            case 0:
                actionCode = GetActionFromUser();
                break;
            case 100:
                Environment.Exit(1);
                break;
            default:
                Console.WriteLine("Enter the correct value what is present in the list");
                break;
        }
    }

    private static int GetActionFromUser()
    {
        try
        {
            Console.WriteLine("Okay, tell me what kind of information you want me to tell about that page." +
                "\n\t 1. Total number of symbols for html code" +
                "\n\t 2. Find tag by word" +
                "\n\t 3. Total execution time to parse the page" +
                "\n\t " +
                @"Type ""exit"" if you want to close the application");

            var input = Console.ReadLine();

            ArgumentException.ThrowIfNullOrEmpty(input);

            if (Int32.TryParse(input, out int userInput))
                return userInput;
            else if (input.ToLower() == "exit")
                return 100;
            else
                throw new ArgumentException("Wrong user input. Try again");
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine("Enter the number from the list displayed before. You cannot enter the number what is not present in the list of action." +
                $"\n{exception.Message}" +
                $"\t {exception.Data}");
            return 0;
        }
    }

    private static string GetUrlFromUserInput()
    {
        Console.WriteLine("Hey! Lets parse an html code of the web page what you want. Paste here a URL to parse it ----> ");
        var inputUrl = Console.ReadLine();

        return inputUrl;
    }
}
