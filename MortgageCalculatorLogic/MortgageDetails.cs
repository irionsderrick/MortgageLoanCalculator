using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculatorLogic
{
    public class MortgageDetails
    {
        public double HomePrice { get; set; }
        public double MarketValue { get; set; }
        public double DownPayment { get; set; }
        public int LoanTermYears { get; set; }
        public double InterestRate { get; set; }
        public double HoaFeesYearly { get; set; }
        public double BuyerMonthlyIncome { get; set; }

        public double LoanValue { get; set; }
        public double MonthlyPayment { get; set; }
        public double EscrowMonthly { get; set; }
        public double LoanInsuranceMonthly { get; set; }
        public double TotalMonthlyPayment { get; set; }
        public bool IsApproved { get; set; }
    }
}
