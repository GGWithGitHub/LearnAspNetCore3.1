using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Core.ExtensionMethods
{
    public static class DataExtensions
    {
        public static List<T> ToList<T>(this DataSet ds, Func<DataRow, T> f, int tableIndex = 0)
        {
            if (ds.Tables == null || ds.Tables.Count < tableIndex + 1)
                return default(List<T>);

            return ds.Tables[tableIndex].AsEnumerable().Select(x => f.Invoke(x)).ToList();
        }

        public static Dictionary<T1, T2> ToDictionary<T1, T2>(this DataSet ds, Func<DataRow, T1> f, Func<DataRow, T2> f2, int tableIndex = 0)
        {
            if (ds.Tables == null || ds.Tables.Count < tableIndex + 1)
                return default(Dictionary<T1, T2>);

            Dictionary<T1, T2> response = null;
            IEqualityComparer<T1> comparer = null;

            if (typeof(T1) == typeof(string))
            {
                comparer = (IEqualityComparer<T1>)StringComparer.OrdinalIgnoreCase;
                response = new Dictionary<T1, T2>(comparer);
            }
            else
            {
                response = new Dictionary<T1, T2>();
            }


            var vals = ds.Tables[tableIndex].AsEnumerable();

            foreach (var val in vals)
            {
                if (!response.Keys.Contains(f.Invoke(val)))
                    response.Add(f.Invoke(val), f2.Invoke(val));
            }

            return response;
        }

        public static T As<T>(this DataSet ds, Func<DataRow, T> f, int tableIndex = 0)
        {
            if (ds.Tables == null || ds.Tables.Count < tableIndex + 1)
                return default(T);

            return ds.Tables[tableIndex].AsEnumerable().Select(x => f.Invoke(x)).FirstOrDefault();
        }

        public static Dictionary<string, object> ToColumnDictionary(this DataSet ds, int row, int tableIndex = 0)
        {
            return ds.Tables[tableIndex].Columns
                        .Cast<DataColumn>()
                        .ToList()
                        .ToDictionary(
                            x => x.ColumnName,
                            x => ds.Tables[tableIndex].Rows[row][x.ColumnName]
                        );
        }

        public static async Task LoadAsync(this DataTable datatable, DbDataReader reader)
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
    }
}
