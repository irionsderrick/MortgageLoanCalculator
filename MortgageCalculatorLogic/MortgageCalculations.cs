
namespace MortgageCalculatorLogic;

public class MortgageCalculations

{
    public MortgageDetails CalculateLoan(MortgageDetails loan)
    {
        // Calculate Initial Loan Amount
        double initialLoan = loan.HomePrice - loan.DownPayment;
        double originationFee = 0.01 * initialLoan;
        loan.LoanValue = initialLoan + originationFee + 2500; // Add $2500 closing cost

        // Calculate Monthly Mortgage Payment (Principal + Interest)
        int totalPayments = loan.LoanTermYears * 12;
        double monthlyInterestRate = (loan.InterestRate / 100) / 12;
        loan.MonthlyPayment = (loan.LoanValue * monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalPayments)) /
                              (Math.Pow(1 + monthlyInterestRate, totalPayments) - 1);

        // Calculate Property Tax & Insurance (Escrow)
        double propertyTaxYearly = 0.0125 * loan.MarketValue;
        double insuranceYearly = 0.0075 * loan.MarketValue;
        loan.EscrowMonthly = (propertyTaxYearly + insuranceYearly) / 12;

        // Calculate HOA Fees (if applicable)
        double hoaMonthly = loan.HoaFeesYearly / 12;

        // Check Loan Insurance Requirement (If equity < 10%)
        double equity = (loan.DownPayment / loan.MarketValue) * 100;
        if (equity < 10)
        {
            loan.LoanInsuranceMonthly = (0.01 * loan.LoanValue) / 12;
        }

        // Calculate Total Monthly Payment
        loan.TotalMonthlyPayment = loan.MonthlyPayment + loan.EscrowMonthly + hoaMonthly + loan.LoanInsuranceMonthly;

        // Loan Approval Check
        loan.IsApproved = loan.TotalMonthlyPayment < (0.25 * loan.BuyerMonthlyIncome);

        return loan;
    }
}


