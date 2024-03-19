using Skillup.TaxCalculator.Utility;
using System;

namespace Skillup.TaxCalculator.Model
{
    /// <summary>
    /// Instance of person and personal information 
    /// </summary>
    public class PersonalInfo
    {
        #region Properties
        /// <summary>
        /// Person name
        /// </summary>
        public string PersonName { get; private set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        public DateTime DOB { get; private set; }

        /// <summary>
        /// Gender
        /// </summary>
        public Genders Gender { get; private set; }

        /// <summary>
        /// Property to get age of the person
        /// </summary>
        public int Age 
        { 
            get
            { 
                return GetAge(); 
            }  
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor creating instace of the PersonalInfo class
        /// </summary>
        /// <param name="personName"> Person name </param>
        /// <param name="dob"> Date of birth </param>
        /// <param name="gender"> Gender </param>
        public PersonalInfo(string personName, DateTime dob, Genders gender)
        {
            PersonName = personName;
            DOB = dob;
            Gender = gender;
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Gets the age of the person
        /// </summary>
        private int GetAge()
        {
            int age = DateTime.Now.Year - DOB.Year;

            if (DateTime.Now.DayOfYear < DOB.DayOfYear)
            {
                age--;
            }

            return age;
        }
        #endregion
    }
}
