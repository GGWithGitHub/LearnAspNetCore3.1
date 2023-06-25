using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Learn_core_mvc.Repository
{
    #region Data Access Code With Stored Procedure Part 1
    //public abstract class SqlSPRepository : IDisposable
    //{
    //    protected string connectionString;

    //    SqlConnection conn;
    //    SqlCommand cmd;
    //    SqlTransaction tran;
    //    public SqlSPRepository(string connectionString)
    //    {
    //        this.connectionString = connectionString;
    //        conn = new SqlConnection(connectionString);
    //    }

    //    public void BeginTran()
    //    {
    //        if (conn.State != ConnectionState.Open)
    //        {
    //            conn.Open();
    //        }
    //        tran = conn.BeginTransaction();
    //    }

    //    public void Rollback()
    //    {
    //        tran.Rollback();
    //        tran.Dispose();
    //        tran = null;
    //    }

    //    public void Commit()
    //    {
    //        tran.Commit();
    //        tran.Dispose();
    //        tran = null;
    //    }

    //    public void SetParam(string paramName, object paramValue)
    //    {
    //        cmd.Parameters.Add(new SqlParameter(paramName, paramValue));
    //    }

    //    public void SetProc(string storedProcedure)
    //    {
    //        if (cmd != null)
    //        {
    //            cmd.Dispose();
    //        }
    //        cmd = new SqlCommand(storedProcedure,conn);
    //        cmd.CommandTimeout = 600;
    //        cmd.CommandType = CommandType.StoredProcedure;
    //    }

    //    public void SetOutParam(string paramName, int outParam)
    //    {
    //        SqlParameter p = new SqlParameter(paramName, SqlDbType.Int);
    //        p.Direction = ParameterDirection.Output;
    //        cmd.Parameters.Add(p);
    //    }

    //    public void SetOutStrParam(string paramName, string outParam)
    //    {
    //        SqlParameter p = new SqlParameter(paramName, SqlDbType.VarChar);
    //        p.Direction = ParameterDirection.Output;
    //        cmd.Parameters.Add(p);
    //    }

    //    public object GetOutParam(string paramName)
    //    {
    //        return cmd.Parameters[paramName].Value;
    //    }

    //    public SqlDataReader GetDataReader()
    //    {
    //        if (conn.State != ConnectionState.Open)
    //        {
    //            conn.Open();
    //        }
    //        if (tran != null)
    //        {
    //            cmd.Transaction = tran;
    //        }
    //        return cmd.ExecuteReader();
    //    }

    //    public DataTable GetDataTable()
    //    {
    //        if (tran != null)
    //        {
    //            cmd.Transaction = tran;
    //        }
    //        DataTable result = new DataTable();
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da.SelectCommand = cmd;
    //        da.Fill(result);
    //        conn.Close();
    //        return result;
    //    }

    //    public DataSet GetDataSet()
    //    {
    //        if (tran != null)
    //        {
    //            cmd.Transaction = tran;
    //        }
    //        DataSet result = new DataSet();
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da.SelectCommand = cmd;
    //        da.Fill(result);
    //        conn.Close();
    //        return result;
    //    }

    //    public DataSet GetDataSet(string sTblName, ref DataSet ds)
    //    {
    //        if (tran != null)
    //        {
    //            cmd.Transaction = tran;
    //        }
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da.SelectCommand = cmd;
    //        if (sTblName != "")
    //        {
    //            da.Fill(ds,sTblName);
    //        }
    //        else
    //        {
    //            da.Fill(ds);
    //        }
    //        conn.Close();
    //        return ds;
    //    }

    //    public object GetSingleValue()
    //    {
    //        if (conn.State != ConnectionState.Open)
    //        {
    //            conn.Open();
    //        }
    //        if (tran != null)
    //        {
    //            cmd.Transaction = tran;
    //        }
    //        return cmd.ExecuteScalar();
    //    }
    //    public void Execute(string executeQuery)
    //    {
    //        if (cmd != null)
    //        {
    //            cmd.Dispose();
    //        }
    //        cmd = new SqlCommand(executeQuery, conn);
    //        cmd.CommandTimeout = 600;
    //        cmd.CommandType = CommandType.Text;
    //        conn.Open();
    //        cmd.ExecuteNonQuery();
    //        conn.Close();
    //    }
    //    public int ExecuteNonQuery()
    //    {
    //        int rowsAffected;
    //        if (conn.State != ConnectionState.Open)
    //        {
    //            conn.Open();
    //        }
    //        if (tran != null)
    //        {
    //            cmd.Transaction = tran;
    //        }
    //        cmd.CommandTimeout = 1000;
    //        rowsAffected = cmd.ExecuteNonQuery();
    //        conn.Close();
    //        return rowsAffected;
    //    }

    //    public string ExecuteScalarValue(string commandText)
    //    {
    //        string value = "";
    //        try
    //        {
    //            if (cmd != null)
    //            {
    //                cmd.Dispose();
    //            }
    //            cmd = new SqlCommand(commandText, conn);
    //            cmd.CommandType = CommandType.Text;
    //            cmd.CommandText = commandText;
    //            cmd.CommandTimeout = 600;
    //            cmd.Connection = conn;

    //            conn.Open();

    //            value = cmd.ExecuteScalar().ToString();

    //            conn.Close();
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //        return value;
    //    }

    //    public int TimeOut
    //    {
    //        get { return cmd.CommandTimeout; }
    //        set { cmd.CommandTimeout = value; }
    //    }

    //    public List<T> GetToList<T>(DataSet ds, Func<DataRow, T> f, int tableIndex = 0)
    //    {
    //        if (ds.Tables == null || ds.Tables.Count < tableIndex + 1)
    //            return default(List<T>);

    //        return ds.Tables[tableIndex].AsEnumerable().Select(x => f.Invoke(x)).ToList();
    //    }

    //    public void Dispose() 
    //    {
    //        if (cmd != null)
    //        {
    //            cmd.Dispose();
    //        }
    //        if (conn != null)
    //        {
    //            conn.Dispose();
    //        }
    //    }
    //}
    #endregion
}
