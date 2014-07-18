using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class Database
{
    private static string sdwConnectionString = @"Data Source = .\SQLEXPRESS; user id=sa; password=server; Initial Catalog = oap.v.02.2;";
    private static SqlConnection sdwDBConnection;  // to create a connection 
    private static SqlCommand queryCommand; //pass the constructor the connection string and the query string in this
    private static SqlDataReader queryCommandReader;
    private static DataTable dataTable; //to hold all the data returned by the query.
    private static DataSet queryDataSet;
    private static SqlDataAdapter queryAdapter;
    private static string tableName;

    public static DataTable getDataTable()
    {
        return dataTable;
    }

    public static void connectToDatabse()
    {
        sdwDBConnection = new SqlConnection(sdwConnectionString);
        sdwDBConnection.Open();
    }

    public static void executeQuery(String query)
    {
        queryCommand = new SqlCommand(query, sdwDBConnection);
        queryCommandReader = queryCommand.ExecuteReader();
        dataTable = new DataTable();
        dataTable.Load(queryCommandReader);

    }

    public static DataRow getRowFromTable(string table)
    {
        sdwDBConnection = new SqlConnection(sdwConnectionString);
        sdwDBConnection.Open();
        queryCommand = new SqlCommand();
        tableName = table;
        DataRow drow=null;
        try
        {
            queryCommand.CommandText = "select * from " + tableName;
            queryCommand.Connection = sdwDBConnection;
            queryAdapter = new SqlDataAdapter();
            queryAdapter.SelectCommand = queryCommand;
            queryDataSet = new DataSet();
            queryAdapter.Fill(queryDataSet, tableName);
            SqlCommandBuilder cb = new SqlCommandBuilder(queryAdapter);
            drow = queryDataSet.Tables[tableName].NewRow();
        }
        catch (Exception ex)
        {
        }
        return drow;
    }

    public static void updateTable(DataRow drow)
    {
        try
        {
            queryDataSet.Tables[tableName].Rows.Add(drow);
            queryAdapter.Update(queryDataSet, tableName);
        }
        catch (Exception ex)
        {
            AUserSession.Current.Warning = "Problem while Registering. Try after some time.";
        }
        disconnectToDatabase();
    }
    public static void disconnectToDatabase()
    {
        sdwDBConnection.Close();
    }
}