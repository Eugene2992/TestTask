using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace Test.DB
{
    public class DBWorker
    {
        //строка подключения к БД
        private const string connectionString = @"Server=DESKTOP-M6RU297\SQLEXPRESS01;Database=Test_WorkersCompanies;Integrated Security=true";
        public void CreateWorker(int id,string FirstName, string SecondName, string Patronymic, string DateRecruitment, string Position, string Company)
        {
            //созданиее записи нового работника
            SqlConnection connection = new SqlConnection(connectionString);
            //нахождение максимального ID работника, с последующим присвоением инкримента
            id = GetMaxWorkerId() + 1;
            string sqlExpression = $"INSERT INTO dbo.Workers VALUES ({id},'{FirstName}','{SecondName}','{Patronymic}','{DateRecruitment}','{Position}','{Company}')";
            connectToDBAndExecuteNonQuery(connection, sqlExpression);
        }

        public List<Worker> GetAllWorkers(List<Worker> allWorkersList)
        {
            //формирование списка всех работников из БД
            SqlConnection connection = new SqlConnection(connectionString);
            string sqlExpression = $"(SELECT * FROM dbo.Workers)";
            connectToDBAndExecuteReader(connection, sqlExpression,ref allWorkersList);
            return (allWorkersList);
        }


        public void ChangeWorker(int id, string FirstName, string SecondName, string Patronymic, string DateRecruitment, string Position, string Company)
        {
            //редактирование информации о работнике в БД
            SqlConnection connection = new SqlConnection(connectionString);
            object executeResult = new object();
            string sqlExpression = $"UPDATE dbo.Workers SET FirstName='{FirstName}',SecondName='{SecondName}',Patronymic='{Patronymic}',DateRecruitment='{DateRecruitment}',Position='{Position}',Company='{Company}' WHERE Id = {id}";
            connectToDBAndExecuteNonQuery(connection, sqlExpression);
        }

        public void DeleteWorker(int id)
        {
            //удаление информации о работнике из БД
            SqlConnection connection = new SqlConnection(connectionString);
            object executeResult = new object();
            string sqlExpression = $"DELETE dbo.Workers WHERE Id = {id}";
            connectToDBAndExecuteNonQuery(connection, sqlExpression);
        }

        public static int GetMaxWorkerId()
        {
            //определение максимально ID работника в БД
            Object idMaxObj;
            SqlConnection connection = new SqlConnection(connectionString);
            string sqlExpression = $"SELECT MAX(id) FROM dbo.Workers";
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

        public static void connectToDBAndExecuteScalar(SqlConnection connection, string sqlExpression,out object executeResult)
        {
            //Выполнение запроса с возвращаемым скалярным значением
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
        public static void connectToDBAndExecuteReader(SqlConnection connection, string sqlExpression,ref List<Worker> allWorkersList)
        {
            //выполнение запроса с чтением
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
                        object FirstName = reader.GetValue(1);
                        object SecondName = reader.GetValue(2);
                        object Patronymic = reader.GetValue(3);   
                        object DateRecruitment = reader.GetValue(4);
                        object Position = reader.GetValue(5);
                        object Company = reader.GetValue(6);
                        allWorkersList.Add(new Worker() { Id = (int)Id, FirstName = (string)FirstName, SecondName = (string)SecondName, Patronymic = (string)Patronymic, DateRecruitment = (DateTime)DateRecruitment, Position = (Positions)Enum.Parse(typeof(Positions),Position.ToString()), Company = (string)Company });;;
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
            //выполнение запроса без возвращаемого значения
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
