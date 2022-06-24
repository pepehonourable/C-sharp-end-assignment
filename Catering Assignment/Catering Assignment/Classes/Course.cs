using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catering_Assignment.Classes
{
    internal class Course
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private bool _hasStarter;
        private bool _hasMain;
        private bool _hasDessert;
        private string _customerName;
        //Customer _customer;

        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; } 
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public bool HasStarter
        { 
            get { return _hasStarter; } 
            set { _hasStarter = value; }
        }

        public bool HasMain
        {
            get { return _hasMain; }
            set { _hasMain = value; }
        }

        public bool HasDessert
        {
            get { return _hasDessert; }
            set { _hasDessert = value; }
        }

        //public Customer Customer
        //{
        //    get { return _customer; }
        //    set { _customer = value; }
        //}

        //Methods
        public int CourseDailyPrice()
        {
            int dailyPrice = 0;
            {
                if(HasStarter == true)
                {
                    dailyPrice += 3;
                }
                if(HasMain == true)
                {
                    dailyPrice += 5;
                }
                if(HasDessert == true)
                {
                    dailyPrice += 2;
                }
            }
            return dailyPrice;
        }

        public int CourseTotalPrice()
        {
            double amountOfDays;
            int totalDays;
            int totalPrice;
            
            amountOfDays = (_endDate - _startDate).TotalDays + 1;
            totalDays = Convert.ToInt32(amountOfDays);

            totalPrice = totalDays * CourseDailyPrice();
            
            return totalPrice;
        }

        public Course(string customerName, DateTime startDate, DateTime endDate, bool hasStarter, bool hasMain, bool hasDessert)
        {
            _customerName = customerName; //huhuhuhuh
            StartDate = startDate;
            EndDate = endDate;
            HasStarter = hasStarter;
            HasMain = hasMain;
            HasDessert = hasDessert;
        }

    }
}
