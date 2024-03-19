namespace Skillup.TaxCalculator.Utility
{
    /// <summary>
    /// Genders
    /// </summary>
    public enum Genders
    {
        Male,
        Female
    }

    /// <summary>
    /// Error codes
    /// </summary>
    public enum ErrorCode
    {
        NoError, 
        InputOutOftextLimit,
        NameNotGiven,
        InvalidCharacters,
        BirthYearOutOfRange,
        InvalidInput,
        IncomeNotGiven, 
        InvestmentMoreThanIncome,
        InvestmentCombinedWithHomeLoanMoreThanIncome
    }
}
