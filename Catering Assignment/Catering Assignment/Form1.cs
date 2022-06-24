using Catering_Assignment.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//Nathan Mills
namespace Catering_Assignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();
        }
        Company newCompany = new Company();
        public void StartForm()
        {
            Application.Run(new LoadingScreen());
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        string con = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CateringAssingmentDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into Customer VALUES (@CustomerName)";
                    command.Parameters.AddWithValue("@CustomerName", textBox1.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool starter = false; //set to false
            bool main = false; //set to false
            bool dessert = false; //set to false

            if (checkbox1.Checked) //BIT DATA TYPE (0 AND 1)
            {
                //dailyPrice = dailyPrice + 3;
                starter = true;
            }
            if (checkbox2.Checked)
            {
                //dailyPrice = dailyPrice + 5;
                main = true;
            }
            if (checkbox3.Checked)
            {
                //dailyPrice = dailyPrice + 2;
                dessert = true;
            }
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;
            string customerName = Convert.ToString(comboBox1.Text);

            //double amountOfDays = (endDate - startDate).TotalDays + 1; //Difference between start and end date
            //int totalDays = Convert.ToInt32(amountOfDays);
            //int totalPrice = dailyPrice * totalDays; //Total price

            //CALL METHOD BY Class.Method()

            Course newCourse = new Course(customerName, startDate, endDate, starter, main, dessert);

            newCompany.AddCourse(newCourse); //Creation of a arraylist

            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into Course VALUES (@StartDate, @EndDate, @HasStarter, @HasMain, @HasDessert, @CustomerName, @DailyCost, @TotalCost)";
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@HasStarter", starter);
                    command.Parameters.AddWithValue("@HasMain", main);
                    command.Parameters.AddWithValue("@HasDessert", dessert);
                    command.Parameters.AddWithValue("@CustomerName", customerName);
                    command.Parameters.AddWithValue("@DailyCost", newCourse.CourseDailyPrice()); //set to null
                    command.Parameters.AddWithValue("@TotalCost", newCourse.CourseTotalPrice()); //set to null so will be calculated in C#

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(con)) //Customer name from Customer table
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM Customer";

                    connection.Open();
                    using (SqlDataReader rows = command.ExecuteReader())
                    {
                        while (rows.Read())
                        {
                            if (!comboBox1.Items.Contains(rows["Customer Name"].ToString()))
                            {
                                comboBox1.Items.Add(rows["Customer Name"].ToString());
                            }
                        }
                    }
                    connection.Close();
                }
            }
            /*
            int totalTurnover = 0;
            using (SqlConnection connection = new SqlConnection(con)) //Total turnover CHNAGE
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT TotalCost FROM Course";

                    connection.Open();
                    using (SqlDataReader row = command.ExecuteReader())
                    {

                        while (row.Read())
                        {
                            totalTurnover += int.Parse(row["TotalCost"].ToString());
                        }
                    }
                    connection.Close();
                }
            }
            label13.Text = "€ " + totalTurnover.ToString();*/
            /*
            decimal dailyAverage;
            using (SqlConnection connection = new SqlConnection(con)) //Sum all fields
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT AVG (DailyCost) FROM Course";

                    connection.Open();
                    dailyAverage = (int)command.ExecuteScalar(); //need (int) instead of double [NOT SURE WHY]
                    connection.Close();
                }
            }
            
            int count;
            using (SqlConnection connection = new SqlConnection(con)) //count all fields to be divided by sum
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT COUNT (DailyCost) FROM Course"; //Need brackets or crashes!
                    
                    connection.Open();
                    count = (int) command.ExecuteScalar(); //need (int) 
                    connection.Close();
                }
            }
            */
            int totalTurn = newCompany.TotalTurnover();
            label13.Text = "€ " + totalTurn.ToString();

            decimal dailyAverage = newCompany.AverageDailyTurnover();
            label14.Text = "€ " + dailyAverage.ToString();

            string highestSpender = newCompany.HighestSpender();
            label17.Text = highestSpender;

            string starterNoDessert = newCompany.StarterNoDessert();
            label19.Text = starterNoDessert;

            string top50Spenders = newCompany.Top50Spenders();
            label21.Text = top50Spenders;

            //string highestSpender;
            //using (SqlConnection connection = new SqlConnection(con)) //count all fields to be divided by sum
            //{
            //    using (SqlCommand command = new SqlCommand())
            //    {
            //        command.Connection = connection;
            //        command.CommandType = CommandType.Text;
            //        command.CommandText = "SELECT CustomerName FROM Course ORDER BY TotalCost DESC"; //Need brackets or crashes!

            //        connection.Open();
            //        highestSpender = (string)command.ExecuteScalar(); //need (int) 
            //        connection.Close();
            //    }
            //}



            //List<String> columnData1 = new List<String>();
            //string combinedString1;
            //using (SqlConnection connection = new SqlConnection(con)) //HasStarter no HasDessert
            //{
            //    using (SqlCommand command = new SqlCommand())
            //    {
            //        command.Connection = connection;
            //        command.CommandType = CommandType.Text;
            //        command.CommandText = "SELECT CustomerName FROM Course WHERE HasStarter = 'TRUE' AND HasDessert = 'FALSE'";

            //        connection.Open();
            //        using (SqlDataReader row = command.ExecuteReader())
            //        {
            //            while (row.Read())
            //            {
            //                columnData1.Add(row.GetString(0));
            //            }
            //        }
            //        connection.Close();
            //    }
            //}
            //combinedString1 = string.Join(", ", columnData1); //Converts list to string

        }
            /*
            decimal totalAverage;
            using (SqlConnection connection = new SqlConnection(con)) //Sum all fields of total price
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT AVG (TotalCost) FROM Course";

                    connection.Open();
                    totalAverage = (int)command.ExecuteScalar(); //need (int) instead of double [NOT SURE WHY]
                    connection.Close();
                }
            }

            string combinedString2;
            List<String> columnData2 = new List<String>();
            using (SqlConnection connection = new SqlConnection(con)) //Total turnover
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT CustomerName FROM Course WHERE TotalCost > " + totalAverage; //Open to injection however no user input

                    connection.Open();

                    using (SqlDataReader row = command.ExecuteReader())
                    {
                        while (row.Read())
                        {
                            columnData2.Add(row.GetString(0));
                        }
                    }
                    connection.Close();
                }
            }
            combinedString2 = string.Join(", ", columnData2); //Converts list to string
            label21.Text = Convert.ToString(combinedString2);
        }
            */

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Text = "Catering Assignment";
            using (SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CateringAssingmentDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM Course";

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime startDate = reader.GetDateTime(0);
                            DateTime endDate = reader.GetDateTime(1);
                            bool starter = reader.GetBoolean(2);
                            bool main = reader.GetBoolean(3);
                            bool dessert = reader.GetBoolean(4);
                            string customerName = reader.GetString(5);

                            Course newCourse = new Course(customerName, startDate, endDate, starter, main, dessert);

                            newCompany.AddCourse(newCourse);
                        }
                    }
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        //private void button3_Click(object sender, EventArgs e)//TESTING
        //{
        //    label22.Text = "";

        //    foreach (Course courses in newCompany.Courses)
        //    {
        //        label22.Text += courses.CustomerName + courses.HasStarter + courses.HasMain + courses.CourseTotalPrice() + " | ";
        //    }
        //    //foreach (Course courses in newCompany.Courses)
        //    //{
        //    //    label22.Text += courses.CourseDailyPrice() + " | ";
        //    //}
        //}
    }
}
