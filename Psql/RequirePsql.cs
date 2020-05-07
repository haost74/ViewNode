using Npgsql;
using System;

namespace Psql
{
    public class RequirePsql
    {
        public string PathConnect { get; set; } = "";
        public Action<string> Error = null;

        public bool Connect(string sql)
        {
            bool isRes = false;

            using (NpgsqlConnection conn = new NpgsqlConnection(""))
            {
                conn.Open();
                if (conn.State != System.Data.ConnectionState.Open) return isRes;
                isRes = true;
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    dr.ReadAsync()
                        .ContinueWith(res =>
                        {
                            var test = res.Result;
                        });


                }
            }

            return isRes;
        }

        #region возвращает масив запроса
        /// <summary>
        /// возвращает масив запроса
        /// </summary>
        /// <typeparam name="T">тип allocator</typeparam>
        /// <param name="obj">объект  allocator</param>
        /// <param name="sql">запрос к db</param>
        /// <returns>массив значений T</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "<Ожидание>")]
        public virtual T[] GetArray<T>(T obj, string sql) where T : class
        {

            T[] res = new T[0];
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(PathConnect))
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                        {
                            try
                            {
                                using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                                {
                                    da.Fill(dt);
                                    res = new T[dt.Rows.Count];
                                    try
                                    {
                                        for (int i = 0; i < dt.Rows.Count; ++i)
                                        {
                                            T instance = (T)Activator.CreateInstance(typeof(T));

                                            for (int k = 0; k < dt.Rows[i].ItemArray.Length; ++k)
                                            {
                                                if (dt.Rows[i].ItemArray[k] != System.DBNull.Value && typeof(T).GetProperty(dt.Columns[k].ColumnName) != null)
                                                    typeof(T).GetProperty(dt.Columns[k].ColumnName).SetValue(instance, dt.Rows[i].ItemArray[k]);

                                            }

                                            res[i] = instance;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Error?.BeginInvoke(ex.ToString(), null, null);
                                        Console.WriteLine(ex.ToString());
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Error?.BeginInvoke(ex.ToString(), null, null);
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                Error?.BeginInvoke(ex.ToString(), null, null);
            }

            return res;
        }
        #endregion

    }
}
