using System;
using System.Data.SqlClient;
using Entities;
using System.ServiceModel;

namespace ProductSOAPService
{
    public class ProductSOAPService : IProductSOAPService
    {
        private readonly string connectionString;

        public ProductSOAPService()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Sales_DB_Connection"].ConnectionString;
        }

        public Products Create(Products products)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"INSERT INTO Products (ProductName, CategoryID, UnitPrice, UnitsInStock)
                                     VALUES (@ProductName, @CategoryID, @UnitPrice, @UnitsInStock);
                                     SELECT SCOPE_IDENTITY();";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", products.ProductName);
                        command.Parameters.AddWithValue("@CategoryID", products.CategoryID);
                        command.Parameters.AddWithValue("@UnitPrice", products.UnitPrice);
                        command.Parameters.AddWithValue("@UnitsInStock", products.UnitsInStock);

                        int newId = Convert.ToInt32(command.ExecuteScalar());
                        products.ProductID = newId;
                    }
                }
                return products;
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error creating product: {ex.Message}");
            }
        }

        public Products RetrieveById(int productId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT ProductID, ProductName, CategoryID, UnitPrice, UnitsInStock 
                                     FROM Products WHERE ProductID = @ProductID";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Products
                                {
                                    ProductID = (int)reader["ProductID"],
                                    ProductName = reader["ProductName"].ToString(),
                                    CategoryID = (int)reader["CategoryID"],
                                    UnitPrice = (decimal)reader["UnitPrice"],
                                    UnitsInStock = (int)reader["UnitsInStock"]
                                };
                            }
                            throw new FaultException("Product not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error retrieving product: {ex.Message}");
            }
        }

        public bool Update(Products products)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"UPDATE Products SET ProductName = @ProductName, CategoryID = @CategoryID, 
                                     UnitPrice = @UnitPrice, UnitsInStock = @UnitsInStock WHERE ProductID = @ProductID";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", products.ProductName);
                        command.Parameters.AddWithValue("@CategoryID", products.CategoryID);
                        command.Parameters.AddWithValue("@UnitPrice", products.UnitPrice);
                        command.Parameters.AddWithValue("@UnitsInStock", products.UnitsInStock);
                        command.Parameters.AddWithValue("@ProductID", products.ProductID);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error updating product: {ex.Message}");
            }
        }

        public bool Delete(int productId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"DELETE FROM Products WHERE ProductID = @ProductID";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productId);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException($"Error deleting product: {ex.Message}");
            }
        }
    }
}
