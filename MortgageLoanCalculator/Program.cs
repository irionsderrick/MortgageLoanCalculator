using MortgageCalculatorLogic;
using uiv = CCAD17Utilities.UserInputWithValidation;
namespace MortgageLoanCalculator;

public class Program
{
    public static void Main(string[] args)
    {


        Console.WriteLine("=== Mortgage Calculator ===");

        var mcl = new MortgageDetails();

        var purchasePrice = uiv.GetUserInputDouble("Enter Home Purchase Price: $");
        mcl.HomePrice = purchasePrice;

        var marketValue = uiv.GetUserInputDouble("Enter Market Value of Home: $");
        mcl.MarketValue = marketValue;

        var downPayment = uiv.GetUserInputDouble("Enter Down Payment Amount: $");
        mcl.DownPayment = downPayment;

        int loanTerm;
        do
        {
            loanTerm = uiv.GetUserInputInt("Enter Loan Term (15 or 30 years): ");
        } while (loanTerm != 15 && loanTerm != 30);

        mcl.LoanTermYears = loanTerm;


        var interestRate = uiv.GetUserInputDouble("Enter Fixed Interest Rate (e.g., 5.0 for 5%): ");
        mcl.InterestRate = interestRate;

        var hoaFeesYearly = uiv.GetUserInputDouble("Enter Yearly HOA Fees (if any, otherwise enter 0): $");
        mcl.HoaFeesYearly = hoaFeesYearly;

        var buyerMonthlyIncome = uiv.GetUserInputDouble("Enter Buyer’s Monthly Income: $");
        mcl.BuyerMonthlyIncome = buyerMonthlyIncome;

        MortgageCalculations calculator = new();

        calculator.CalculateLoan(mcl);

        Console.WriteLine("\n=== Loan Summary ===");
        Console.WriteLine($"Loan Amount: ${mcl.LoanValue:F2}");
        Console.WriteLine($"Monthly Payment (Principal + Interest): ${mcl.MonthlyPayment:F2}");
        Console.WriteLine($"Escrow (Taxes & Insurance): ${mcl.EscrowMonthly:F2}");
        Console.WriteLine($"HOA Fees: ${mcl.HoaFeesYearly / 12:F2}");
        Console.WriteLine($"Loan Insurance: ${mcl.LoanInsuranceMonthly:F2}");
        Console.WriteLine($"Total Monthly Payment: ${mcl.TotalMonthlyPayment:F2}");

        if (mcl.IsApproved)
        {
            Console.WriteLine("Loan Approved!");
        }
        else
        {
            Console.WriteLine("Loan Denied! Consider:");
            Console.WriteLine("- Increasing your down payment.");
            Console.WriteLine("- Looking for a more affordable home.");
        }
    }

   
}
