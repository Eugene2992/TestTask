using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace Test.DB
{
    public class DBCompany
    {
        private const string connectionString = @"Server=DESKTOP-M6RU297\SQLEXPRESS01;Database=Test_WorkersCompanies;Integrated Security=true";
        public void CreateCompany(int id,string Name, string OrgLegalFormName)
        {
            //
            SqlConnection connection = new SqlConnection(connectionString);
            id = GetMaxCompanyId() + 1;
            string sqlExpression = $"INSERT INTO dbo.Companies VALUES ({id},'{Name}','{OrgLegalFormName}')";
            connectToDBAndExecuteNonQuery(connection, sqlExpression);
        }

        public List<Company> GetAllCompanies(List<Company> allCompaniesList)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sqlExpression = $"(SELECT * FROM dbo.Companies)";
            connectToDBAndExecuteReader(connection, sqlExpression,ref allCompaniesList);
            GetSizeCompanies(connection, ref allCompaniesList);
            return (allCompaniesList);
        }


        //сделать
        public void ChangeCompany(int id, string Name, string OrgLegalFormName)
        {
            //
            SqlConnection connection = new SqlConnection(connectionString);
            object executeResult = new object();
            string sqlExpression = $"UPDATE dbo.Companies SET Name='{Name}',OrgLegalFormName='{OrgLegalFormName}' WHERE Id = {id}";
            connectToDBAndExecuteNonQuery(connection, sqlExpression);
        }

        public void DeleteCompany(int id)
        {
            //
            SqlConnection connection = new SqlConnection(connectionString);
            object executeResult = new object();
            string sqlExpression = $"DELETE dbo.Companies WHERE Id = {id}";
            connectToDBAndExecuteNonQuery(connection, sqlExpression);
        }

        public static int GetMaxCompanyId()
        {
            Object idMaxObj;
            SqlConnection connection = new SqlConnection(connectionString);
            string sqlExpression = $"SELECT MAX(id) FROM dbo.Companies";
            connectToDBAndExecuteScalar(connection, sqlExpression,out idMaxObj);
            if (idMaxObj is DBNull)
            {
                return 0;
            }
            else
            {
                return ((int)idMaxObj);
            }
        }

        
        public static void GetSizeCompanies(SqlConnection connection, ref List<Company> allCompaniesList)
        { 
            //исправить 
            Object intCountObj;
            foreach (Company comp in allCompaniesList)
            { 
                //пофиксить строку 
               string sqlExpression = $"SELECT COUNT(*) FROM dbo.Workers WHERE Company = '{comp.Name}'";
               connectToDBAndExecuteScalar(connection, sqlExpression, out intCountObj);
               comp.SizeOfCompany = (int)intCountObj;
            }
            
        }
        

        public static void connectToDBAndExecuteScalar(SqlConnection connection, string sqlExpression,out object executeResult)
        {
            try
            {
                connection.OpenAsync().Wait();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                executeResult = command.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                // если подключение открыто
                if (connection.State == ConnectionState.Open)
                {
                    // закрываем подключение
                    connection.CloseAsync();
                }
            }
        }
        public static void connectToDBAndExecuteReader(SqlConnection connection, string sqlExpression,ref List<Company> allCompaniesList)
        {
            try
            {
                connection.OpenAsync().Wait();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    while (reader.Read()) // построчно считываем данные
                    {
                        object Id = reader.GetValue(0);
                        object Name = reader.GetValue(1);
                        object OrgLegalFormName = reader.GetValue(2);
                        allCompaniesList.Add(new Company() { Id = (int)Id, Name = (string)Name, OrgLegalFormName = (string)OrgLegalFormName, SizeOfCompany = 0 });
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                // если подключение открыто
                if (connection.State == ConnectionState.Open)
                {
                    // закрываем подключение
                    connection.CloseAsync();
                }
            }
        }
        public static void connectToDBAndExecuteNonQuery(SqlConnection connection, string sqlExpression)
        {
            try
            {
                connection.OpenAsync().Wait();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                // если подключение открыто
                if (connection.State == ConnectionState.Open)
                {
                    // закрываем подключение
                    connection.CloseAsync();
                }
            }
        }

    }
}
