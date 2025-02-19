namespace CCAD17Utilities;

public class UserInputWithValidation
{
    /// <summary>
    /// Get input from the console for the user to give a good integer
    /// </summary>
    /// <param name="prompt">What you say to the user</param>
    /// <param name="minGoodValue">Minimum acceptable value in range</param>
    /// <param name="maxGoodValue">Maximum acceptable value in range</param>
    /// <returns>The int as entered by the user</returns>
    public static int GetUserInputInt(string prompt, int minGoodValue = int.MinValue, int maxGoodValue = int.MaxValue)
    {
        int value = int.MinValue; //-2,147,...,...
        bool success = false;

        while (!success)   //while not success {!FALSE == it's funny because it's true!}
        {
            Console.WriteLine(prompt);
            success = int.TryParse(Console.ReadLine(), out value);
            if (!success)
            {
                Console.WriteLine("Please provide a valid number.");
            }
            else
            {
                if (value < minGoodValue || value > maxGoodValue)
                {
                    Console.WriteLine($"Please provide a number between {minGoodValue} and {maxGoodValue}.");
                    success = false;
                }
            }
        }
        return value;
    }

    /// <summary>
    /// Get input from the console for the user to give a good double
    /// </summary>
    /// <param name="prompt">What you say to the user</param>
    /// <param name="minGoodValue">Minimum acceptable value in range</param>
    /// <param name="maxGoodValue">Maximum acceptable value in range</param>
    /// <returns>The double as entered by the user</returns>
    public static double GetUserInputDouble(string prompt, double minGoodValue = double.MinValue, double maxGoodValue = double.MaxValue)
    {
        Console.WriteLine(prompt);
        double value = double.MinValue;

        var success = double.TryParse(Console.ReadLine(), out value);

        if (success && value >= minGoodValue && value <= maxGoodValue)
        {
            return value;
        }

        Console.WriteLine($"Please make sure to input a valid double between {minGoodValue} and {maxGoodValue}");
        return GetUserInputDouble(prompt, minGoodValue, maxGoodValue);
    }

    public static string GetUserInputString(string prompt)
    {
        Console.WriteLine(prompt);
        
        return Console.ReadLine() ?? string.Empty;
    }

    public static bool GetUserInputBoolean(string prompt)
    {
        Console.WriteLine(prompt);
        string response = Console.ReadLine();

        return response.StartsWith("Y", StringComparison.OrdinalIgnoreCase);
    }
}
