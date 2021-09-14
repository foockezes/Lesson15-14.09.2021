using System;
using System.Data.SqlClient;

namespace ConsoleApp
{
    public class Person
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string BirthDate { get; set; }

        public void AddPereson(SqlConnection connection)
        {
            var sqlQuery = $"INSERT INTO Person(LastName, FirstName, MiddleName, BirthDate) VALUES('{LastName}', '{FirstName}', '{MiddleName}', '{BirthDate}');";
            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result > 0) Console.WriteLine("Добавление прошло успешно!!!");
        }
        public void DeletPereson(SqlConnection connection)
        {
            var sqlQuery = $"DELETE PERSON WHERE ID = {Id};";
            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result > 0) Console.WriteLine("Удаление прошло успешно");
            else Console.WriteLine($"Человек с таким Id: {Id} не найдено!!!");
        }

        public void SelecAlltPereson(SqlConnection connection)
        {
            var sqlQuery = $"SELECT * FROM Person;";
            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                Console.WriteLine($"ID: {sqlReader.GetValue(0)}, Фамилия: {sqlReader.GetValue(1)}, Имя: {sqlReader.GetValue(2)}, Отчество: {sqlReader.GetValue(3)}, Дата рождения: {sqlReader.GetValue(4)}");
            }
            sqlReader.Close();
        }

        public void SelecByIdPereson(SqlConnection connection)
        {
            var sqlQuery = $"SELECT * FROM Person WHERE id ={Id}; ";
            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                Console.WriteLine($"ID: {sqlReader.GetValue(0)}, Last name: {sqlReader.GetValue(1)}, First name: {sqlReader.GetValue(2)}, Date of Birth: {sqlReader.GetValue(4)}");
            }

            sqlReader.Close();
        }


        public void UpdatePereson(SqlConnection connection)
        {
            var sqlQuery = $"UPDATE Person SET LastName='{LastName}', FirstName='{FirstName}', MiddleName='{MiddleName}', BirthDate='{BirthDate}' WHERE ID = {Id}";
            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Обнавление прошло успешно");
            }

            else
            {
                Console.WriteLine("Ошибка обновлении");
            }
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            string ConString = "" + @"Data source=.\SQLEXPRESS; " + "Initial catalog=Person; " + "Trusted_Connection = True;";
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                connection.Open();
                // Проверяет соеденение
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("успешное соедения\n");
                }

                // Выбор действия
                Console.WriteLine("Добавить 'Add' Удалить 'Del' Выбрать один по Id 'SBI' Выбрать всё 'SA' Обновлять 'Up'");
                var m = Console.ReadLine();

                //Добавляет человека по ID
                if (m == "Add")
                {
                    Console.WriteLine(" -----------------------------------------\n| с * является обязательным для заполнения |\n -----------------------------------------");
                    Console.WriteLine("Фамилия*, Имя*, Отчество, Дата* ПРИМЕР '1999-12-12'");
                    Person add = new Person()
                    {
                        LastName = Console.ReadLine(),
                        FirstName = Console.ReadLine(),
                        MiddleName = Console.ReadLine(),
                        BirthDate = Console.ReadLine(),
                    };
                    add.AddPereson(connection);
                }

                //удаляет человека по ID
                else if (m == "Del")
                {
                    Console.Write("ВВедите id человека для удаления: ");
                    Person delete = new Person()
                    {
                        Id = Convert.ToInt32(Console.ReadLine()),
                    };
                    delete.DeletPereson(connection);
                }

                //Выводит все записи таблицы 
                else if (m == "SA")
                {
                    Person selectAll = new Person();
                    selectAll.SelecAlltPereson(connection);
                }
                // Выводит одного человека по id
                else if (m == "SBI")
                {
                    Person selectById = new Person() { Id = Convert.ToInt32(Console.ReadLine()) };
                    selectById.SelecByIdPereson(connection);
                }

                // Обновляет данные человека
                else if (m == "Up")
                {
                    Console.WriteLine(" -----------------------------------------\n| с * является обязательным для заполнения |\n -----------------------------------------");
                    Console.WriteLine("Id*, Фамилия*, Имя*, Отчество, Дата* ПРИМЕР '1999-12-12'");
                    Person update = new Person()
                    {
                        Id = Convert.ToInt32(Console.ReadLine()),
                        LastName = Console.ReadLine(),
                        FirstName = Console.ReadLine(),
                        MiddleName = Console.ReadLine(),
                        BirthDate = Console.ReadLine(),
                    };
                    update.UpdatePereson(connection);

                } else Console.WriteLine("Command not found");

                connection.Close();
            }
        }
    }
}
