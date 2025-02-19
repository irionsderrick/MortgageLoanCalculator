using Shouldly;
using MortgageCalculatorLogic;
namespace TestMortgageCalculatorLogic;

public class TestMortgageCalculations
{



    private readonly MortgageCalculations _calculator;

    public TestMortgageCalculations()
    {
        _calculator = new MortgageCalculations();   
    }

    [Fact]
    public void CalculateLoan_Should_CorrectlyComputeLoanAmount()
    {
        // Arrange
        var loan = new MortgageDetails
        {
            HomePrice = 300000,
            MarketValue = 310000,
            DownPayment = 60000,
            LoanTermYears = 30,
            InterestRate = 5.0,
            HoaFeesYearly = 1200,
            BuyerMonthlyIncome = 8000
        };

        // Act
        var result = _calculator.CalculateLoan(loan);

        // Assert
        double expectedLoanAmount = (loan.HomePrice - loan.DownPayment) + (0.01 * (loan.HomePrice - loan.DownPayment)) + 2500;
        result.LoanValue.ShouldBe(expectedLoanAmount);
    }

    [Fact]
    public void CalculateLoan_Should_CorrectlyComputeMonthlyMortgagePayment()
    {
        // Arrange
        var loan = new MortgageDetails
        {
            HomePrice = 250000,
            MarketValue = 255000,
            DownPayment = 50000,
            LoanTermYears = 15,
            InterestRate = 4.0,
            HoaFeesYearly = 600,
            BuyerMonthlyIncome = 7500
        };

        // Act
        var result = _calculator.CalculateLoan(loan);

        // Assert
        result.MonthlyPayment.ShouldBeGreaterThan(0);
    }

    [Fact]
    public void CalculateLoan_Should_CorrectlyComputeEscrowPayments()
    {
        // Arrange
        var loan = new MortgageDetails
        {
            HomePrice = 400000,
            MarketValue = 420000,
            DownPayment = 80000,
            LoanTermYears = 30,
            InterestRate = 3.5,
            HoaFeesYearly = 2400,
            BuyerMonthlyIncome = 9000
        };

        // Act
        var result = _calculator.CalculateLoan(loan);

        // Assert
        double expectedPropertyTax = (0.0125 * loan.MarketValue) / 12;
        double expectedInsurance = (0.0075 * loan.MarketValue) / 12;
        double expectedEscrow = expectedPropertyTax + expectedInsurance;

        result.EscrowMonthly.ShouldBe(expectedEscrow);
    }

    [Fact]
    public void CalculateLoan_Should_IncludeLoanInsurance_When_EquityIsLessThan10Percent()
    {
        // Arrange
        var loan = new MortgageDetails
        {
            HomePrice = 500000,
            MarketValue = 500000,
            DownPayment = 20000, // Less than 10% equity
            LoanTermYears = 30,
            InterestRate = 4.5,
            HoaFeesYearly = 1800,
            BuyerMonthlyIncome = 10000
        };

        // Act
        var result = _calculator.CalculateLoan(loan);

        // Assert
        result.LoanInsuranceMonthly.ShouldBeGreaterThan(0);
    }

    [Fact]
    public void CalculateLoan_Should_NotIncludeLoanInsurance_When_EquityIsGreaterThanOrEqualTo10Percent()
    {
        // Arrange
        var loan = new MortgageDetails
        {
            HomePrice = 400000,
            MarketValue = 410000,
            DownPayment = 50000, // Above 10% equity
            LoanTermYears = 30,
            InterestRate = 3.8,
            HoaFeesYearly = 1200,
            BuyerMonthlyIncome = 8500
        };

        // Act
        var result = _calculator.CalculateLoan(loan);

        // Assert
        result.LoanInsuranceMonthly.ShouldBe(0);
    }

    [Fact]
    public void CalculateLoan_Should_DenyLoan_When_MonthlyPaymentExceeds25PercentOfIncome()
    {
        // Arrange
        var loan = new MortgageDetails
        {
            HomePrice = 600000,
            MarketValue = 620000,
            DownPayment = 100000,
            LoanTermYears = 30,
            InterestRate = 6.5,
            HoaFeesYearly = 3600,
            BuyerMonthlyIncome = 8000 // Loan likely to exceed 25% of income
        };

        // Act
        var result = _calculator.CalculateLoan(loan);

        // Assert
        result.IsApproved.ShouldBeFalse();
    }

    [Fact]
    public void CalculateLoan_Should_ApproveLoan_When_MonthlyPaymentIsWithin25PercentOfIncome()
    {
        // Arrange
        var loan = new MortgageDetails
        {
            HomePrice = 250000,
            MarketValue = 255000,
            DownPayment = 75000,
            LoanTermYears = 30,
            InterestRate = 4.2,
            HoaFeesYearly = 1200,
            BuyerMonthlyIncome = 9000 // Loan should be within limits
        };

        // Act
        var result = _calculator.CalculateLoan(loan);

        // Assert
        result.IsApproved.ShouldBeTrue();
    }

    [Fact]
    public void CalculateLoan_Should_HandleEdgeCase_ZeroDownPayment()
    {
        // Arrange
        var loan = new MortgageDetails
        {
            HomePrice = 350000,
            MarketValue = 360000,
            DownPayment = 0, // No down payment
            LoanTermYears = 30,
            InterestRate = 5.0,
            HoaFeesYearly = 1800,
            BuyerMonthlyIncome = 8500
        };

        // Act
        var result = _calculator.CalculateLoan(loan);

        // Assert
        result.LoanValue.ShouldBeGreaterThan(0);
        result.LoanInsuranceMonthly.ShouldBeGreaterThan(0); // Should trigger loan insurance
    }

    [Fact]
    public void CalculateLoan_Should_HandleEdgeCase_ZeroIncome()
    {
        // Arrange
        var loan = new MortgageDetails
        {
            HomePrice = 200000,
            MarketValue = 205000,
            DownPayment = 50000,
            LoanTermYears = 15,
            InterestRate = 3.5,
            HoaFeesYearly = 600,
            BuyerMonthlyIncome = 0 // No income
        };

        // Act
        var result = _calculator.CalculateLoan(loan);

        // Assert
        result.IsApproved.ShouldBeFalse();
    }








    //    [Theory]


    //    [InlineData(300000, 310000, 60000, 30, 5.0, 1200, 8000, true)]
    //    [InlineData(450000, 429500, 32000, 30, 4.5, 0, 9000, false)]
    //    public void TestLoanCalculation(
    //        double homePrice, double marketValue, double downPayment, int loanTerm,
    //        double interestRate, double hoaFeesYearly, double monthlyIncome, bool expectedApproval)
    //    {
    //        // Arrange
    //        var loan = new MortgageDetails
    //        {
    //            HomePrice = homePrice,
    //            MarketValue = marketValue,
    //            DownPayment = downPayment,
    //            LoanTermYears = loanTerm,
    //            InterestRate = interestRate,
    //            HoaFeesYearly = hoaFeesYearly,
    //            BuyerMonthlyIncome = monthlyIncome
    //        };

    //        var calculator = new MortgageCalculations();

    //        // Act
    //        loan = calculator.CalculateLoan(loan);

    //        // Assert
    //        Assert.Equal(expectedApproval, loan.IsApproved);
    //    }


}
