using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Phonebook_v2._2
{
    class PersonAccaunt
    {
        #region Filds
        string firstName;
        string lastName;
        string cityOfResidence;
        uint postIndex;
        string cityStreet;
        uint numberOfHous;
        uint apartmentNumber;
        uint mobilPhone;
        readonly string dateOfRegistration;
        readonly string timeOfRegistration;
        #endregion
        #region Constructors
        public PersonAccaunt(string name = "", string soname = "", string city = "", uint index = 0, string strt = "", uint hous = 0, uint kv = 0, uint phone = 0)
        {
            FirstName = name;
            LastName = soname;
            CityOfResidence = city;
            PostIndex = index;
            //PersonalID = ID;
            CityStreet = strt;
            NumberOfHous = hous;
            ApartmentNumber = kv;
            MobilPhone = phone;
            dateOfRegistration = DateTime.Today.ToShortDateString();
            timeOfRegistration = DateTime.Now.ToShortTimeString();
        }
        #endregion
        #region Properties
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value))
                    firstName = value;
                else firstName = "unknown";
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value))
                    lastName = value;
                else lastName = "unknown";
            }
        }
        public string CityOfResidence
        {
            get { return cityOfResidence; }
            set
            {
                if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value))
                    cityOfResidence = value;
                else cityOfResidence = "unknown";
            }
        }
        public uint PostIndex
        {
            get { return postIndex; }
            set
            {
                if (value < 1000000) postIndex = value;
                else postIndex = 0;
            }
        }
        
        public string CityStreet
        {
            get { return cityStreet; }
            set
            {
                if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value))
                    cityStreet = value;
                else
                    cityStreet = "unknown";
            }
        }
        public uint NumberOfHous
        {
            get { return numberOfHous; }
            set
            {
                if (value < 150) numberOfHous = value;
                else numberOfHous = 0;
            }
        }
        public uint ApartmentNumber
        {
            get { return apartmentNumber; }
            set
            {
                if (value < 500) apartmentNumber = value;
                else apartmentNumber = 0;
            }
        }
        public uint MobilPhone
        {
            get { return mobilPhone; }
            set
            {
                if ((value > 100) & (value < 1000000000)) mobilPhone = value;
                else mobilPhone = 0;
            }
        }
        public string DateOfRegistration
        {
            get { return dateOfRegistration; }
        }
        public string TimeOfRegistration
        {
            get { return timeOfRegistration; }
        }
        #endregion
        #region Methods
        #region Method that Display Person Accaunt data on Console
        /// <summary>
        /// Method for displaying the current values ​​of class fields
        /// </summary>
        /// <param name="p"></param>
        public static void DisplayPersonAccaunt(PersonAccaunt p)
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("First name            :  {0}", p.FirstName);
            Console.WriteLine("Last name             :  {0}", p.LastName);
            Console.WriteLine("City                  :  {0}", p.CityOfResidence);

            if (p.PostIndex == 0)
                Console.WriteLine("Post index            :  unknown");
            else
                Console.WriteLine("Post index            :  {0}", p.PostIndex);

            Console.WriteLine("Street                :  {0}", p.CityStreet);

            if (p.NumberOfHous == 0)
                Console.WriteLine("Hous                  :  unknown");
            else
                Console.WriteLine("Hous                  :  {0}", p.NumberOfHous);

            if (p.ApartmentNumber == 0)
                Console.WriteLine("Apartment             :  unknown");
            else
                Console.WriteLine("Apartment             :  {0}", p.ApartmentNumber);

            if (p.MobilPhone == 0)
                Console.WriteLine("Phone                 :  unknown");
            else
                Console.WriteLine("Phone                 :  +380{0}", p.MobilPhone);

            Console.WriteLine("Time of registration  :  {0}", p.DateOfRegistration);
            Console.WriteLine("Date of registration  :  {0}", p.TimeOfRegistration);
        }
        #endregion
        #region WritePersonInFile
        /// <summary>
        /// Block of writing data to a file
        /// </summary>
        /// <param name="sb">temporary data storage</param>
        /// <param name="phoneBookDirectory">The path to the file</param>
        public void WritePersonInFile(StringBuilder sb, DirectoryInfo phoneBookDirectory)
        {
            sb.AppendLine("========================================");
            sb.AppendLine("First name            :  " + FirstName);
            sb.AppendLine("Last name             :  " + LastName);
            sb.AppendLine("City                  :  " + CityOfResidence);
            if (PostIndex == 0)
                sb.AppendLine("Post index            :  unknown");
            else
                sb.AppendLine("Post index            :  " + PostIndex);
            sb.AppendLine("Street                :  " + CityStreet);
            if (NumberOfHous == 0)
                sb.AppendLine("Hous                  :  unknown");
            else
                sb.AppendLine("Hous                  :  " + NumberOfHous);
            if (ApartmentNumber == 0)
                sb.AppendLine("Apartment             :  unknown");
            else
                sb.AppendLine("Apartment             :  " + ApartmentNumber);

            if (MobilPhone == 0)
                sb.AppendLine("Phone                 :  unknown");
            else
                sb.AppendLine("Phone                 :  +380" + MobilPhone);

            sb.AppendLine("Time of registration  :  " + DateOfRegistration);
            sb.AppendLine("Date of registration  :  " + TimeOfRegistration);

            foreach (var s in sb.ToString())
            {
                File.AppendAllText(Path.Combine(Convert.ToString(phoneBookDirectory), "phonebook.txt"), $"{s}");
            }
            sb.Clear();
        }
        #endregion
        #endregion  
    }
}
