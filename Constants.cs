namespace Skillup.TaxCalculator.Utility
{
    /// <summary>
    /// Constants
    /// </summary>
    public class Constants
    {
        
        #region Validating patterns and values

        /// <summary>
        /// Character pattern for name formating
        /// </summary>
        public const string CHARACTER_PATTERN_FOR_NAME = @"^[A-za-z ]*$";

        /// <summary>
        /// Maximum length of name
        /// </summary>
        public const int MAX_LENGTH_FOR_NAME = 50;
        
        /// <summary>
        /// Minimum age of senior citizen
        /// </summary>
        public const int MIN_AGE_OF_SENIOR_CITIZEN = 60; 

        /// <summary>
        /// Maximum accepting birth year
        /// </summary>
        public const int MAX_ACCEPTED_YEAR = 2010;

        /// <summary>
        /// Minimum accepting birth year
        /// </summary>
        public const int MIN_ACCEPTED_YEAR = 1900;

        /// <summary>
        /// Minimum income
        /// </summary>
        public const decimal MIN_INCOME = 0;

        /// <summary>
        /// Allowed nonTaxable income percentage 
        /// </summary>
        public const int ALLOWED_NON_TAXABLE_INCOME_PERCENTAGE = 20;

        /// <summary>
        /// Minimum investment
        /// </summary>
        public const decimal MIN_INVESTMENT = 0;

        /// <summary>
        /// Maximum nontaxable investment
        /// </summary>
        public const decimal MAX_NON_TAXABLE_INVESTMENT = 100000;

        /// <summary>
        /// Minimum home loan
        /// </summary>
        public const decimal MIN_HOME_LOAN = 0;

        /// <summary>
        /// Allowed nontaxable homeLoan percentage
        /// </summary>
        public const int ALLOWED_NON_TAXABLE_HOME_LOAN_PERCENTAGE = 80;
        #endregion
    }
}
