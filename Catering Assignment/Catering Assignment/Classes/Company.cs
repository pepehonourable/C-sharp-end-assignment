using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catering_Assignment.Classes
{
    internal class Company
    {
        //private Course _course;
        private string _coursesName;
        private List<Course> _courses = new List<Course>();

        public string CompanyName
        {
            set { _coursesName = "Ramzis' Kitchen"; }
        }

        //public Course Course
        //{
        //    get { return _course; }
        //    set { _course = value; }
        //}

        public List<Course> Courses
        {
            get { return _courses; }
        }

        public void AddCourse(Course course)
        {
            _courses.Add(course);
        }
   
        //public Company(Course course)
        //{
        //    Course = course;

        //}

        public int TotalTurnover()
        {
            int turnover = 0;
            foreach (Course course in _courses)
            {
                turnover += course.CourseTotalPrice();
            }   
            return turnover;
        }

        public decimal AverageDailyTurnover()
        {
            decimal averageDailyTurnover = 0;
            int courseCount = 0;
            {
                foreach (Course courses in _courses)
                {
                    averageDailyTurnover += courses.CourseDailyPrice();
                    courseCount++;
                }
                if (averageDailyTurnover > 0)
                {
                    averageDailyTurnover /= courseCount;
                    return averageDailyTurnover;
                }
                else
                {
                    return averageDailyTurnover;
                }
            }
        }
        public string HighestSpender()
        {
            string highestSpender = null;
            int highestSpenderValue = 0;
            foreach (Course courses in _courses) 
            {
                if (courses.CourseTotalPrice() > highestSpenderValue)
                {
                    highestSpender = courses.CustomerName;
                    highestSpenderValue = courses.CourseTotalPrice();
                }
            }
            if (highestSpender == null)
            {
                highestSpender = "N/A";
                return highestSpender;
            }
            else
            {
                return highestSpender;
            }
        }

        public string StarterNoDessert()
        {
            string starterNoDessert = null;
            foreach (Course courses in _courses)
            {
                if (courses.HasStarter == true && courses.HasDessert == false)
                {
                    starterNoDessert += courses.CustomerName + " ";
                }
            }
            if (starterNoDessert == null)
            {
                starterNoDessert = "N/A";
                return starterNoDessert;
            }
            else
            {
                return starterNoDessert;

            }
        }

        public string Top50Spenders()
        {
            int counter = 0;
            int top50;
            string top50Spenders = null;
            foreach (Course courses in _courses)
            {
                counter++;
            }
            if (counter == 0)
            {
                top50Spenders = "N/A";
                return top50Spenders;
            }
            else
            {
                top50 = TotalTurnover() / counter;
                foreach (Course course in _courses)
                {
                    if (course.CourseTotalPrice() > top50)
                    {
                        top50Spenders += course.CustomerName + " ";
                    }
                }
            }
            return top50Spenders;
        }
    }
}
