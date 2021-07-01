using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Habbitica.BLL_DAL.DBfunctions;

namespace Habbitica.BLL_DAL.Entity
{
    /// <summary>
    /// Данный класс хранит функции по обработке выполненных пользователем достижений
    /// </summary>
    public class AchievemntDone
    {
        private readonly DBConnection _dBConnection;
        public AchievemntDone(DBConnection dBConnection)
        {
            _dBConnection = dBConnection;
        }
        /// <summary>
        /// Занести в базу данных выполненное пользователем достижение
        /// </summary>
        /// <param name="userLogin">Уникальный идентификатор пользователя</param>
        /// <param name="achievName">Название выполненного достижения</param>
        /// <returns>true - запрос выполнен успешно. false - ошибка в запросе</returns>
        public bool Push(string userLogin, string achievName)
        {
            bool res = true;
            MySqlConnection connect = _dBConnection.Connection;
            try
            {
                connect.Open();
                string sqlCommandText =
                    $"SET @user_id = (SELECT user_id FROM user WHERE user_login = '{userLogin}');" +
                    $"SET @achiev_id = (SELECT achiev_id FROM achievement WHERE achiev_name = '{achievName}');" +
                    $"INSERT INTO achievement_done (user_id, achiev_id) " +
                    $"VALUES (@user_id,@achiev_id)";
                MySqlCommand command = new(sqlCommandText, connect);
                MySqlDataReader reader = command.ExecuteReader();
            }
            catch
            {
                res = false;
            }
            finally
            {
                connect.Close();
            }
            return res;
        }
        /// <summary>
        /// Получить список выполненных достижений указанного пользователя
        /// </summary>
        /// <param name="userLogin">Уникальный идентификатор пользователя</param>
        /// <param name="completeAchievements">Массив, в который необходимо сохранить выполненые достижения</param>
        /// <returns>true - запрос выполнен успешно. false - ошибка в запросе</returns>
        public bool GetReceived(string userLogin, ref Dictionary<string, string> completeAchievements)
        {
            completeAchievements = new Dictionary<string, string>();
            bool res = true;
            MySqlConnection connect = _dBConnection.Connection;
            MySqlDataReader reader = null;
            try
            {
                connect.Open();
                string getRowsSql = $"SELECT COUNT(achiev_name) FROM achievement " +
                    $"WHERE achiev_id  IN (SELECT achiev_id FROM achievement_done " +
                    $"WHERE user_id IN(SELECT user_id FROM user " +
                    $"WHERE user_login = '{userLogin}'))";
                string sqlCommandText = $"SELECT achiev_name,achiev_desc FROM achievement " +
                    $"WHERE achiev_id  IN (SELECT achiev_id FROM achievement_done " +
                    $"WHERE user_id IN(SELECT user_id FROM user " +
                    $"WHERE user_login = '{userLogin}'))";
                string getResultSqlCommand = getRowsSql + ";" + sqlCommandText;
                MySqlCommand command = new(getResultSqlCommand, connect);
                reader = command.ExecuteReader();
                int columns = 0;
                if (reader.Read())
                {
                    columns = int.Parse(reader[0].ToString());
                }
                reader.NextResult();
                int i = 0;
                while (reader.Read())
                {
                    completeAchievements.Add(reader[0].ToString(), reader[1].ToString());
                    i++;
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
                connect.Close();
                if (reader != null)
                    reader.Close();
            }
            return res;
        }
        /// <summary>
        /// Получить список невыполненных достижений
        /// </summary>
        /// <param name="userLogin">Уникальный идентификатор пользователя</param>
        /// <param name="notCompleteAchievements">Массив, в который необходимо сохранить выполненые достижения</param>
        /// <returns>true - запрос выполнен успешно. false - ошибка в запросе</returns>
        public bool GetNotReceived(string userLogin, ref Dictionary<string,string> notCompleteAchievements)
        {
            notCompleteAchievements = new Dictionary<string, string>();
            bool res = true;
            MySqlConnection connect = _dBConnection.Connection;
            MySqlDataReader reader = null;
            try
            {
                connect.Open();
                string getRowsSql = $"SELECT COUNT(achiev_name) FROM achievement " +
                    $"WHERE achiev_id NOT IN (SELECT achiev_id FROM achievement_done " +
                    $"WHERE user_id IN(SELECT user_id FROM user " +
                    $"WHERE user_login = '{userLogin}'))";
                string sqlCommandText = $"SELECT achiev_name,achiev_desc FROM achievement " +
                    $"WHERE achiev_id NOT IN (SELECT achiev_id FROM achievement_done " +
                    $"WHERE user_id IN(SELECT user_id FROM user " +
                    $"WHERE user_login = '{userLogin}'))";
                string getResultSqlCommand = getRowsSql + ";" + sqlCommandText;
                MySqlCommand command = new(getResultSqlCommand, connect);
                reader = command.ExecuteReader();
                int columns = 0;
                if (reader.Read())
                {
                    columns = int.Parse(reader[0].ToString());
                }
                reader.NextResult();
                int i = 0;
                while (reader.Read())
                {
                    notCompleteAchievements.Add(reader[0].ToString(), reader[1].ToString());
                    i++;
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
                connect.Close();
                if (reader != null)
                    reader.Close();
            }
            return res;
        }
    }
}