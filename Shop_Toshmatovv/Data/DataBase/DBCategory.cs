using MySql.Data.MySqlClient;
using Shop_Toshmatovv.Data.Common;
using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Models;
using System.Collections.Generic;

namespace Shop_Toshmatovv.Data.DataBase
{
    public class DBCategory : ICategorys
    {
        public IEnumerable<Categorys> AllCategories
        {
            get
            {
                List<Categorys> categories = new List<Categorys>();

                MySqlConnection MySqlConnection = Connection.MySqlOpen();

                MySqlDataReader CategorysData = Connection.MySqlQuery("SELECT * FROM Shop.Categorys ORDER BY `Name`;", MySqlConnection);

                while (CategorysData.Read())
                {

                    categories.Add(new Categorys()
                    {
                        Id = CategorysData.IsDBNull(0) ? -1 : CategorysData.GetInt32(0),
                        Name = CategorysData.IsDBNull(1) ? null : CategorysData.GetString(1),
                        Description = CategorysData.IsDBNull(2) ? null : CategorysData.GetString(2)
                    });
                }

                // возвращаем список категорий
                return categories;
            }
        }
    }
}