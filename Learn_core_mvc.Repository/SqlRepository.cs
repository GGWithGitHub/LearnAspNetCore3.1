using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    //public abstract class SqlRepository<T>
    public abstract class SqlRepository
    {
        protected string connectionString;

        public SqlRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //public async Task<long> ExecuteNonQueryGetId(string query, Dictionary<string, object> parameters = null)
        //{
        //    long lastId = 0;

        //    try
        //    {
        //        using (var Connection = new SqlConnection(connectionString))
        //        using (var cmd = new SqlCommand(query, Connection))
        //        {
        //            await Connection.OpenAsync();

        //            if (parameters != null)
        //            {
        //                foreach (var parameter in parameters)
        //                {
        //                    cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
        //                }
        //            }

        //            var reader = await cmd.ExecuteNonQueryAsync();

        //            lastId = cmd.LastInsertedId;

        //            await Connection.CloseAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data["parameters"] = parameters;
        //        throw ex;
        //    }

        //    return lastId;
        //}

        public async Task<bool> ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                using (var Connection = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, Connection))
                {
                    await Connection.OpenAsync();

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                    }

                    var reader = await cmd.ExecuteNonQueryAsync();

                    await Connection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                ex.Data["parameters"] = parameters;
                throw ex;
            }

            return true;
        }
        public async Task<DataSet> ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            var dataset = new DataSet();
            var datatable = new DataTable();

            try
            {
                using (var Connection = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, Connection))
                {
                    await Connection.OpenAsync();

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                    }

                    var reader = await cmd.ExecuteReaderAsync();

                    //await datatable.LoadAsync(reader);
                    await LoadData(datatable, reader);

                    dataset.Tables.Add(datatable);

                    await Connection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                ex.Data["parameters"] = parameters;
                throw ex;
            }

            return dataset;
        }

        public async Task<DataSet> ExecuteCommand(string commandName, Dictionary<string, object> parameters = null)
        {
            var dataset = new DataSet();
            var datatables = new List<DataTable>();

            try
            {
                using (var Connection = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(commandName, Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    await Connection.OpenAsync();

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                    }

                    var reader = await cmd.ExecuteReaderAsync();

                    do
                    {
                        var table = new DataTable();

                        //await table.LoadAsync(reader);
                        await LoadData(table, reader);

                        datatables.Add(table);
                    }
                    while (await reader.NextResultAsync());

                    dataset.Tables.AddRange(datatables.ToArray());

                    await Connection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                ex.Data["parameters"] = parameters;
                throw ex;
            }

            return dataset;
        }

        public async Task<bool> ExecuteNonCommand(string commandName, Dictionary<string, object> parameters = null)
        {
            try
            {
                using (var Connection = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(commandName, Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    await Connection.OpenAsync();

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }
                    }

                    var reader = await cmd.ExecuteNonQueryAsync();

                    await Connection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                ex.Data["parameters"] = parameters;
                throw ex;
            }

            return true;
        }

        //internal ParameterizedOrQuery BuildOrQuery(string columnName, string parameterPrefix, List<string> values)
        //{
        //    var query = new ParameterizedOrQuery()
        //    {
        //        WhereQuery = "",
        //        Parameters = new Dictionary<string, object>()
        //    };

        //    for (var i = 0; i < values.Count; i++)
        //    {
        //        query.WhereQuery += String.Format("{0} = {1}{2}", columnName, parameterPrefix, i);

        //        query.Parameters.Add(String.Format("{0}{1}", parameterPrefix, i), values[i]);

        //        if (i < values.Count - 1)
        //            query.WhereQuery += " OR ";
        //    }

        //    return query;
        //}

        public async Task LoadData(DataTable datatable, DbDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (!datatable.Columns.Contains(reader.GetName(i)))
                    datatable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
            }

            var values = new List<object>();

            while (await reader.ReadAsync())
            {
                values.Clear();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader[i]);
                }
                try
                {
                    datatable.LoadDataRow(values.ToArray(), true);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public List<T> GetToList<T>(DataSet ds, Func<DataRow, T> f, int tableIndex = 0)
        {
            if (ds.Tables == null || ds.Tables.Count < tableIndex + 1)
                return default(List<T>);

            return ds.Tables[tableIndex].AsEnumerable().Select(x => f.Invoke(x)).ToList();
        }
    }
}
