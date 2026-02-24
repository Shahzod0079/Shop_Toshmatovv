using MySql.Data.MySqlClient;

namespace Shop_Toshmatovv.Data.Common
{
    public class Connection
    {
        /// <summary> Прописываем настройки для подключения сервера </summary>
        readonly static string ConnectionData = "server=127.0.0.1;port=3306;database=Shop;uid=root;pwd=";

        /// <summary> Открываем соединение с базой данных MySQL </summary>
        public static MySqlConnection MySqlOpen()
        {
            MySqlConnection NewMySqlConnection = new MySqlConnection(ConnectionData);
            NewMySqlConnection.Open();

            return NewMySqlConnection;
        }

        /// <summary> Выполнение запроса </summary>
        public static MySqlDataReader MySqlQuery(string Query, MySqlConnection Connection)
        {
            MySqlCommand NewMySqlCommand = new MySqlCommand(Query, Connection);
            return NewMySqlCommand.ExecuteReader();
        }

        /// <summary> Закрываем соединение с базой данных MySQL </summary>
        public static void MySqlClose(MySqlConnection connection)
        {
            connection.Close();
        }
    }
}