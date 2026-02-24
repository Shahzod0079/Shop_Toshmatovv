using Shop_Toshmatovv.Data.Common;
using Shop_Toshmatovv.Data.Interfaces;
using Shop_Toshmatovv.Data.Models;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Shop_Toshmatovv.Data.DataBase
{
    public class DBItems : IItems
    {
        public IEnumerable<Categorys> Categories = new DBCategory().AllCategories;

        public IEnumerable<Items> AllItems
        {
            get
            {

                List<Items> items = new List<Items>();

                MySqlConnection MySqlConnection = Connection.MySqlOpen();

                MySqlDataReader ItemsData = Connection.MySqlQuery("SELECT * FROM Items ORDER BY `Name`;", MySqlConnection);

                while (ItemsData.Read())
                {
                    items.Add(new Items()
                    {
                        Id = ItemsData.IsDBNull(0) ? -1 : ItemsData.GetInt32(0),
                        Name = ItemsData.IsDBNull(1) ? "" : ItemsData.GetString(1),
                        Description = ItemsData.IsDBNull(2) ? "" : ItemsData.GetString(2),
                        Img = ItemsData.IsDBNull(3) ? "" : ItemsData.GetString(3),
                        Price = ItemsData.IsDBNull(4) ? -1 : ItemsData.GetInt32(4),
                        Categorys = ItemsData.IsDBNull(5) ? null : Categories.Where(x => x.Id == ItemsData.GetInt32(5)).First()
                    });
                }

                MySqlConnection.Close();

                return items;
            }
        }
    }
}