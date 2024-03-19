using System;
using System.Text.RegularExpressions;

namespace Skillup.TaxCalculator.Utility
{
    /// <summary>
    /// Utilities class
    /// </summary>
    public static class Utilities
    {
        #region Public methods
        /// <summary>
        /// Validates name
        /// </summary>
        /// <param name="name"> Person name </param>
        /// <param name="errorCode"> Errorcode </param>
        /// <returns> Boolean validation for valid name or not </returns>
        public static bool IsValidName(string name, out ErrorCode errorCode)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                errorCode = ErrorCode.NameNotGiven;
                return false;
            }

            if (name.Length > Constants.MAX_LENGTH_FOR_NAME)
            {
                errorCode = ErrorCode.InputOutOftextLimit;
                return false;
            }

            if (!Regex.IsMatch(name, Constants.CHARACTER_PATTERN_FOR_NAME))
            {
                errorCode = ErrorCode.InvalidCharacters;
                return false;
            }

            errorCode = ErrorCode.NoError;
            return true;
        }

        /// <summary>
        /// Validates Date of birth
        /// </summary>
        /// <param name="DOB"> Date of birth </param>
        /// <param name="errorCode"> ErrorCode </param>
        /// <returns> Boolean validation for valid DOB or not </returns>
        public static bool IsValidDOB(DateTime DOB, out ErrorCode errorCode)
        {

            if (DOB.Year >= Constants.MAX_ACCEPTED_YEAR || DOB.Year <= Constants.MIN_ACCEPTED_YEAR)
            {
                errorCode = ErrorCode.BirthYearOutOfRange;
                return false;
            }

            errorCode = ErrorCode.NoError;

            return true;
        }


        /// <summary>
        /// Validates income
        /// </summary>
        /// <param name="income"> Income </param>
        /// <param name="errorCode"> ErroCode </param>
        /// <returns> Boolean validation for valid income or not </returns>
        public static bool IsValidIncome(decimal income, out ErrorCode errorCode)
        {
            if (income == 0)
            {
                errorCode = ErrorCode.IncomeNotGiven;
                return false;
            }

            if (income < Constants.MIN_INCOME)
            {
                errorCode = ErrorCode.InvalidInput;
                return false;
            }

            errorCode = ErrorCode.NoError;
            return true;
        }

        /// <summary>
        /// Validates Investment input
        /// </summary>
        /// <param name="investment"> Investment</param>
        /// <param name="income"> Income </param>
        /// <param name="errorCode"> ErrorCode </param>
        /// <returns> Boolean validation for valid investment value or not </returns>
        public static bool IsValidInvestment(decimal investment, decimal income, out ErrorCode errorCode)
        {
            if (investment < Constants.MIN_INVESTMENT)
            {
                errorCode = ErrorCode.InvalidInput;
                return false;
            }

            if (income < investment)
            {
                errorCode = ErrorCode.InvestmentMoreThanIncome;
                return false;
            }

            errorCode = ErrorCode.NoError;
            return true;
        }

        /// <summary>
        /// Validates homeloan input values
        /// </summary>
        /// <param name="homeLoan"> Homeloan</param>
        /// <param name="income"> Income </param>
        /// <param name="investment"> Investment </param>
        /// <param name="errorCode"> ErrorCode </param>
        /// <returns> Boolean validation for valid home loan input value or not</returns>
        public static bool IsValidHomeLoan(decimal homeLoan, decimal income, decimal investment, out ErrorCode errorCode)
        {
            if (homeLoan < Constants.MIN_HOME_LOAN)
            {
                errorCode = ErrorCode.InvalidInput;
                return false;
            }

            if ((homeLoan + investment) > income)
            {
                errorCode = ErrorCode.InvestmentCombinedWithHomeLoanMoreThanIncome;
                return false;
            }

            errorCode = ErrorCode.NoError;
            return true;
        }
        #endregion
    }
}
