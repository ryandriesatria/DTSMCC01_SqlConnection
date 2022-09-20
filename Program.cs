using ConnectionMCC.Models;
using System;
using System.Data.SqlClient;

namespace ConnectionMCC
{
    class Program
    {
        SqlConnection sqlConnection;

        string connectionString = "Data Source=DESKTOP-BMTQ0KM;Initial Catalog=Toko;User ID=ryan;Password=123456;";

        static void Main(string[] args)
        {
            Program program = new Program();

            Products product = new Products()
            {
                Name = "Indomie Soto",
                Stock = 70,
                Price = 6000
            };

            //program.Insert(product);

            //program.UpdateById(2, product);

            program.DeleteById(2);

            program.GetAll();
        }

        void GetAll()
        {
            string query = "SELECT * FROM Products";

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0] + " - " + sqlDataReader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void GetById(int id)
        {
            string query = "SELECT * FROM Products WHERE Id = @id";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@id";
            sqlParameter.Value = id;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);
            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0] + " - " + sqlDataReader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void Insert(Products product)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                //SqlParameter sqlParameter = new SqlParameter();
                //sqlParameter.ParameterName = "@name";
                //sqlParameter.Value = product.Name;

                sqlCommand.Parameters.Add(new SqlParameter("@name", product.Name));
                sqlCommand.Parameters.Add(new SqlParameter("@stock", product.Stock));
                sqlCommand.Parameters.Add(new SqlParameter("@price", product.Price));

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Products " +
                        "(Name, Stock, Price) " +
                        "VALUES (@name, @stock, @price)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // Update
        void UpdateById(int id, Products product)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                sqlCommand.Parameters.Add(new SqlParameter("@id", id));
                sqlCommand.Parameters.Add(new SqlParameter("@name", product.Name));
                sqlCommand.Parameters.Add(new SqlParameter("@stock", product.Stock));
                sqlCommand.Parameters.Add(new SqlParameter("@price", product.Price));

                try
                {
                    sqlCommand.CommandText = "UPDATE Products " +
                        "SET Name = @name, Stock = @stock, Price = @price " +
                        "WHERE Id = @id";

                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // Delete
        void DeleteById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                sqlCommand.Parameters.Add(new SqlParameter("@id", id));
                

                try
                {
                    sqlCommand.CommandText = "DELETE FROM Products " +
                        "WHERE Id = @id";

                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
