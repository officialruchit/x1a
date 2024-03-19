namespace Skillup.TaxCalculator.Model
{
    /// <summary>
    /// Class containing information about investment
    /// </summary>
    public class InvestmentInfo
    {
        #region Properties
        /// <summary>
        /// Income
        /// </summary>
        public decimal Income { get; private set; }

        /// <summary>
        /// Investment
        /// </summary>
        public decimal Investment { get; private set; }

        /// <summary>
        /// House Loan
        /// </summary>
        public decimal HouseLoan { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor creating instace of InvestmentInfo class 
        /// </summary>
        /// <param name="income"> Income </param>
        /// <param name="investment"> Investment </param>
        /// <param name="houseLoan"> House Loan</param>

        public InvestmentInfo(decimal income, decimal investment, decimal houseLoan) 
        {
            Income = income;
            Investment = investment;
            HouseLoan = houseLoan;
        }
        #endregion
    }
}
