using MySql.Data.MySqlClient;
using Shop_Toshmatovv.Data.Common;
using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop_Toshmatovv.Data.DataBase
{
    public class DBCategory : ICategorys
    {
        public IEnumerable<Categorys> AllCategories
        {
            get
            {
                List<Categorys> categories = new List<Categorys>();
                using (MySqlConnection connection = Connection.MySqlOpen())
                {
                    MySqlDataReader reader = Connection.MySqlQuery("SELECT * FROM Categorys ORDER BY Name;", connection);
                    while (reader.Read())
                    {
                        categories.Add(new Categorys()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2)
                        });
                    }
                }
                return categories;
            }
        }

        public Categorys GetCategory(int id)
        {
            return AllCategories.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Categorys category)
        {
            using (MySqlConnection connection = Connection.MySqlOpen())
            {
                string query = $"INSERT INTO Categorys (Name, Description) VALUES ('{category.Name}', '{category.Description}')";
                Connection.MySqlQuery(query, connection);
            }
        }

        public void Update(Categorys category)
        {
            using (MySqlConnection connection = Connection.MySqlOpen())
            {
                string query = $"UPDATE Categorys SET Name = '{category.Name}', Description = '{category.Description}' WHERE Id = {category.Id}";
                Connection.MySqlQuery(query, connection);
            }
        }

        public void Delete(int id)
        {
            using (MySqlConnection connection = Connection.MySqlOpen())
            {
                string query = $"DELETE FROM Categorys WHERE Id = {id}";
                Connection.MySqlQuery(query, connection);
            }
        }
    }
}