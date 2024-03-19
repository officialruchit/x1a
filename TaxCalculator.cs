using Skillup.TaxCalculator.Utility;
using System;

namespace Skillup.TaxCalculator.Model
{
    /// <summary>
    /// Tax Calculating methods
    /// </summary>
    public class TaxCalculator
    {

        #region Properties

        /// <summary>
        /// Nontaxable amount
        /// </summary>
        public decimal NonTaxableAmount { get; private set; }

        /// <summary>
        /// Taxable amount
        /// </summary>
        public decimal TaxableAmount { get; private set; }

        /// <summary>
        /// Total tax
        /// </summary>
        public decimal TotalTax { get; private set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates TaxCalculator object with costomized slab values and tax rates
        /// </summary>
        /// <param name="slabUpperLimitsForSeniorCitizen"> Slab uppper limits for senior citizen </param>
        /// <param name="slabTaxRantesForSeniorCitizen"> Slab tax rates for senior citizen </param>
        /// <param name="slabUpperLimitsForFemale"> Slab uppper limits for female </param>
        /// <param name="slabTaxRatesForFemale"> Slab tax rates for female  </param>
        /// <param name="slabUpperLimitsForMale"> Slab uppper limits for male </param>
        /// <param name="slabTaxRatesForMale"> Slab tax rates for male  </param>
        public TaxCalculator(decimal[] slabUpperLimitsForSeniorCitizen, int[] slabTaxRantesForSeniorCitizen, decimal[] slabUpperLimitsForFemale, int[] slabTaxRatesForFemale, decimal[] slabUpperLimitsForMale, int[] slabTaxRatesForMale)
        {
            SeniorCitizenTaxPayer seniorCitizenTaxPayer = new SeniorCitizenTaxPayer();
            seniorCitizenTaxPayer.FirstSlabUpperLimit = slabUpperLimitsForSeniorCitizen[0];
            seniorCitizenTaxPayer.SecondSlabUpperLimit = slabUpperLimitsForSeniorCitizen[1];
            seniorCitizenTaxPayer.ThirdSlabUpperLimit = slabUpperLimitsForSeniorCitizen[2];
            seniorCitizenTaxPayer.FirstSlabTaxRate = slabTaxRantesForSeniorCitizen[0];
            seniorCitizenTaxPayer.SecondSlabTaxRate = slabTaxRantesForSeniorCitizen[1];
            seniorCitizenTaxPayer.ThirdSlabTaxRate = slabTaxRantesForSeniorCitizen[2];
            seniorCitizenTaxPayer.FourthSlabTaxRate = slabTaxRantesForSeniorCitizen[3];

            FemaleTaxPayer femaleTaxPayer = new FemaleTaxPayer();
            femaleTaxPayer.FirstSlabUpperLimit = slabUpperLimitsForFemale[0];
            femaleTaxPayer.SecondSlabUpperLimit = slabUpperLimitsForFemale[1];
            femaleTaxPayer.ThirdSlabUpperLimit = slabUpperLimitsForFemale[2];
            femaleTaxPayer.FirstSlabTaxRate = slabTaxRatesForFemale[0];
            femaleTaxPayer.SecondSlabTaxRate = slabTaxRatesForFemale[1];
            femaleTaxPayer.ThirdSlabTaxRate = slabTaxRatesForFemale[2];
            femaleTaxPayer.FourthSlabTaxRate = slabTaxRatesForFemale[3];

            MaleTaxPayer maleTaxPayer = new MaleTaxPayer();
            maleTaxPayer.FirstSlabUpperLimit = slabTaxRatesForMale[0];
            maleTaxPayer.SecondSlabUpperLimit = slabTaxRatesForMale[1];
            maleTaxPayer.ThirdSlabUpperLimit = slabTaxRatesForMale[2];
            maleTaxPayer.FirstSlabTaxRate = slabTaxRatesForMale[0];
            maleTaxPayer.SecondSlabTaxRate = slabTaxRatesForMale[1];
            maleTaxPayer.ThirdSlabTaxRate = slabTaxRatesForMale[2];
            maleTaxPayer.FourthSlabTaxRate = slabTaxRatesForMale[3];
        }

        /// <summary>
        /// Instantiates TaxCalculator object with default slab values and tax rates
        /// </summary>
        public TaxCalculator() { }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the details of the tax 
        /// </summary>
        /// <param name="personalInfo"> Object representing a person having basic informations </param>
        /// <param name="investmentInfo"> Object having Investment informations </param>
        /// <returns> Error code if error generated </returns>
        public ErrorCode CalculateTaxDetails(PersonalInfo personalInfo, InvestmentInfo investmentInfo)
        {
            ErrorCode errorCode;

            #region Information validation
            // Validating the given information
            if (!Utilities.IsValidName(personalInfo.PersonName, out errorCode))
            {
                return errorCode;
            }

            if (!Utilities.IsValidDOB(personalInfo.DOB, out errorCode))
            {
                return errorCode;
            }


            if (!Utilities.IsValidIncome(investmentInfo.Income, out errorCode))
            {
                return errorCode;
            }

            if (!Utilities.IsValidInvestment(investmentInfo.Investment, investmentInfo.Income, out errorCode))
            {
                return errorCode;
            }

            if (!Utilities.IsValidHomeLoan(investmentInfo.HouseLoan, investmentInfo.Income, investmentInfo.Investment, out errorCode))
            {
                return errorCode;
            }
            #endregion

            // Calculate taxable and non taxable amounts
            TaxableAmount = CalcTaxableAmount(investmentInfo, out decimal nonTaxableAmount);
            NonTaxableAmount = nonTaxableAmount;

            // Get the taxpayer according to personal information
            ITaxPayer taxPayer;

            if (personalInfo.Age >= Constants.MIN_AGE_OF_SENIOR_CITIZEN)
            {
                taxPayer = new SeniorCitizenTaxPayer();
            }
            else if (personalInfo.Gender == Genders.Male)
            {
                taxPayer = new MaleTaxPayer();
            }
            else
            {
                taxPayer = new FemaleTaxPayer();
            }

            // Calculating total tax
            TotalTax = CalcTotalTax(taxPayer, TaxableAmount);

            return ErrorCode.NoError;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Calculates taxable and nontaxable amount
        /// </summary>
        /// <param name="investmentInfo"> Investment informations </param>
        /// <param name="nonTaxableAmount"> NonTaxable amount </param>
        /// <returns> Taxable amount </returns>  
        private static decimal CalcTaxableAmount(InvestmentInfo investmentInfo, out decimal nonTaxableAmount)
        {
            decimal investmentNonTaxableAmount = investmentInfo.Investment > Constants.MAX_NON_TAXABLE_INVESTMENT ? Constants.MAX_NON_TAXABLE_INVESTMENT : investmentInfo.Investment;
            nonTaxableAmount = Math.Min(investmentInfo.HouseLoan * Constants.ALLOWED_NON_TAXABLE_HOME_LOAN_PERCENTAGE, investmentInfo.Income * Constants.ALLOWED_NON_TAXABLE_INCOME_PERCENTAGE)/100 + investmentNonTaxableAmount;
            return investmentInfo.Income - nonTaxableAmount;
        }

        /// <summary>
        /// Calculates total tax deviding them into required slabs
        /// </summary>
        /// <param name="taxpayer"> The object of taxpayer having rate and slab limit information </param>
        /// <param name="taxableAmount"> Taxable amount </param>
        /// <returns> Tptal tax to be payed </returns>
        private decimal CalcTotalTax(ITaxPayer taxpayer, decimal taxableAmount)
        { 
            decimal _Slab4Amount = taxableAmount - taxpayer.ThirdSlabUpperLimit > 0 ? taxableAmount - taxpayer.ThirdSlabUpperLimit : 0;
            taxableAmount -= _Slab4Amount;
            decimal _Slab3Amount = taxableAmount - taxpayer.SecondSlabUpperLimit > 0 ? taxableAmount - taxpayer.SecondSlabUpperLimit : 0;
            taxableAmount -= _Slab3Amount;
            decimal _Slab2Amount = taxableAmount - taxpayer.FirstSlabUpperLimit > 0 ? taxableAmount - taxpayer.FirstSlabUpperLimit : 0;
            taxableAmount -= _Slab2Amount;
            decimal _Slab1Amount = taxableAmount;

            decimal taxAmount = (_Slab1Amount * taxpayer.FirstSlabTaxRate + _Slab2Amount * taxpayer.SecondSlabTaxRate + _Slab3Amount * taxpayer.ThirdSlabTaxRate + _Slab4Amount * taxpayer.FourthSlabTaxRate) / 100;

            return taxAmount;
        }
        #endregion
    }
}
