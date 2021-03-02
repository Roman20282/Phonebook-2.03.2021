using System;

namespace Phonebook
{
    public class PersonAccaunt
    {
        #region Filds
        static int ID = 0;     // Namber of records in PhoneBook
        string firstName;
        string lastName;
        string cityOfResidence;
        uint postIndex;
        int personalID;
        string cityStreet;
        uint numberOfHous;
        uint apartmentNumber;
        uint mobilPhone;
        readonly string dateOfRegistration;
        readonly string timeOfRegistration;
        #endregion
        #region Constructors
        public PersonAccaunt(string name ="", string soname="", string city="", uint index=0, string strt="", uint hous=0, uint kv=0, uint phone=0) 
        {
            FirstName = name;
            LastName = soname;
            CityOfResidence = city;
            PostIndex = index;
            PersonalID = ID;
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
        public int PersonalID
        {
            get { return personalID; }
            set
            {
                personalID = ID;
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
        public static int GetID()
        {
            return ID;
        }
        public static void SetID()
        {
            ID++;
        }
        #endregion  
    }
}
