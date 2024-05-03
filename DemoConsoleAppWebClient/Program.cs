using DemoConsoleAppWebClient;
using DemoConsoleAppWebClient.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.ObjectModel;
using System.Net;

class Program
{
    static async Task Main(string[] args)
    {
        WebHelper webHelper = new();
        DateHelper dateHelper = new();
        WebPage webPage = new WebPage();
        bool validUrl = false;

        while (!validUrl)
        {
            try
            {
                webPage.Url = GetUrlFromUserInput(dateHelper);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                webPage.HtmlData = await webHelper.RetrieveWebPageByUrlIfUrlIsValid(webPage.Url);
                watch.Stop();
                webPage.ElapsedTimeToRetrieve = watch.ElapsedMilliseconds;
                validUrl = true;
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
        }

        while (true)
        {
            var actionCode = GetActionFromUser();
            UserMenu(actionCode, webHelper, dateHelper, webPage);
        }
    }

    private static void UserMenu(int actionCode, WebHelper webHelper, DateHelper dateHelper, WebPage webPage)
    {

        switch (actionCode)
        {
            case 1:
                var symbolsOfCodeCount = webHelper.GetNumbersOfHtmlSymbols(webPage.HtmlData);
                Console.WriteLine($"Total number is: {symbolsOfCodeCount}");
                break;
            case 2:
                Console.WriteLine("Let me find a tag with specified value! Enter the value to start searching process.");
                var searchValue = Console.ReadLine();
                var results = webHelper.GetTagsFromHtmlWhatContainsValue(searchValue, webPage.HtmlData);
                if (results.Count > 0)
                {
                    Console.WriteLine($"Tags containing '{searchValue}':");
                    for (int i = 0; i < results.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {results[i]}");
                    }
                }
                else
                {
                    Console.WriteLine($"Value '{searchValue}' not found within any tag.");
                }
                break;
            case 3:
                Console.WriteLine($"The total execution time to retrieve data is: {webPage.ElapsedTimeToRetrieve}ms");
                break;
            case 4:
                DateTimeOffset thisTime;
                thisTime = new DateTimeOffset(dateHelper.GetCurrentDateAndTime(), DateTimeOffset.Now.ToLocalTime().Offset);
                Console.WriteLine($"Local Date Time in UTC format: {thisTime}");
                dateHelper.ShowPossibleLocalTimeZones(thisTime);
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
            Console.WriteLine("\nOkay, tell me what kind of information you want me to tell about that page." +
                "\n\t 1. Total number of symbols for html code" +
                "\n\t 2. Find tag by word" +
                "\n\t 3. Total execution time to parse the page" +
                "\n\t 4. Local Timezone" +
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

    private static string GetUrlFromUserInput(DateHelper dateHelper)
    {
        PrintLocalDateAndTime(dateHelper);
        Console.WriteLine("Hey! Lets parse an html code of the web page what you want. Paste here a URL to parse it ----> ");
        var inputUrl = Console.ReadLine();

        return inputUrl;
    }

    private static void PrintLocalDateAndTime(DateHelper dateHelper)
    {
        Console.WriteLine($"Today is {dateHelper.GetCurrentDateOnly(dateHelper.GetCurrentDateAndTime()).ToLongDateString()}" +
            $"\nNow is {dateHelper.GetCurrentTimeOnly(dateHelper.GetCurrentDateAndTime()).ToLongTimeString()}\n\n");
    }

}
