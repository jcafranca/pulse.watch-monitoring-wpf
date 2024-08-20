using FPOMonitoring.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using FPOMonitoring.Data.Class;
using FPOMonitoring.Tools.Helpers;
using Mysqlx.Crud;
using System.Security.Cryptography;
using Dapper;
using HandyControl.Tools.Extension;
using System.Windows;

namespace FPOMonitoring.Services
{
    public class MySQLService
    {
        private readonly LogService log = new LogService();
        public  IDbConnection GetDbContext(string ConnectionString)
        {
            if (ConnectionString != null)
            {
                if (CheckConnection(ConnectionString) is MySqlConnection mySqlConnection) return mySqlConnection;
            }
            else
            {
                var connection = CheckConnection(ViewModelLocator.Instance.LoginVM.Settings);
                return new MySqlConnection(connection);
            }
            return null;
        }
        public dynamic CheckConnection(dynamic databaseConnection)
        {
            if (databaseConnection != null)
            {
                if (databaseConnection is string conn) return new MySqlConnection(conn);
                if (databaseConnection is DatabaseSettings databaseConn && databaseConn.Server.Count > 0)
                {
                    foreach (var item in databaseConn.Server)
                    {
                        var srv = $"server={item};port={databaseConnection.Port};user={databaseConnection.Username};password={databaseConnection.Password};database={databaseConnection.Database}";
                        using (IDbConnection connection = new MySqlConnection(srv))
                        {
                            try
                            {
                                connection.Open();
                                if (connection.State == ConnectionState.Open)
                                {
                                    return srv;
                                }
                            }
                            catch (MySqlException ex)
                            {
                                log.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(CheckConnection));
                            }
                        }
                    }
                }
            }
            return null;
        }
        public void CheckProductionDate(string ConnectionString)
        {
            try
            {
                using (IDbConnection conn = GetDbContext(ConnectionString))
                {
                    var prod_date = conn.GetList<Production>(new { date = DateTime.Now.ToString("yyyy-MM-dd") }).FirstOrDefault();
                    if (prod_date == null )
                    {
                        var insert_prod = new Production { date = DateTime.Now.ToString("yyyy-MM-dd") };
                        conn.Insert(insert_prod);
                    }
                }
            }
            catch (Exception e)
            {
                log.ErrorLogToFile(e.Message, e.StackTrace, nameof(CheckProductionDate));
            }
        }

        public DateTime GetTimeFromDatabase(string ConnectionString)
        {
            try
            {
                string cmd = "SELECT DATE_FORMAT(now(), '%Y-%m-%d %T') as t;";
                using (IDbConnection connection = GetDbContext(ConnectionString))
                {
                    var dt = connection.QueryFirstOrDefault<DateTime>(cmd);
                    if (dt != null) return dt;
                }
            }
            catch (Exception e)
            {
                log.ErrorLogToFile(e.Message, e.StackTrace, nameof(GetTimeFromDatabase));
            }
            return DateTime.Now;
        }

        public  IEnumerable<Production> InitProductionDates(string ConnectionString)
        {
            using(IDbConnection  conn = GetDbContext(ConnectionString))
            {
                return conn.GetList<Production>(new {archived = 0});
            }
        }

    }
}
