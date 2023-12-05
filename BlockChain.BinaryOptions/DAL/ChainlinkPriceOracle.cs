//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/11/29 14:43:22
//
using System;
using System.Data;
using System.Data.SqlClient;


namespace BlockChain.BinaryOptions.DAL
{
internal class ChainlinkPriceOracle
{
#region 对应的数据库表 ChainlinkPriceOracle
public const string TableName = @"ChainlinkPriceOracle";
#endregion

#region  表 ChainlinkPriceOracle 的Insert操作
public static void Insert(string conStr, Model.ChainlinkPriceOracle model)
{
    string sql = @"insert into ChainlinkPriceOracle (Aggregator,roundId,answer,startedAt,updatedAt,answeredInRound,StartedAt_Time,UpdatedAt_Time) values (@Aggregator,@roundId,@answer,@startedAt,@updatedAt,@answeredInRound,@StartedAt_Time,@UpdatedAt_Time)";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@Aggregator", SqlDbType.NVarChar, 43).Value = model.Aggregator;
    cm.Parameters.Add("@roundId", SqlDbType.Decimal, 17).Value = model.roundId;
    cm.Parameters.Add("@answer", SqlDbType.Decimal, 17).Value = model.answer;
    cm.Parameters.Add("@startedAt", SqlDbType.BigInt, 8).Value = model.startedAt;
    cm.Parameters.Add("@updatedAt", SqlDbType.BigInt, 8).Value = model.updatedAt;
    cm.Parameters.Add("@answeredInRound", SqlDbType.Decimal, 17).Value = model.answeredInRound;
    cm.Parameters.Add("@StartedAt_Time", SqlDbType.DateTime, 8).Value = model.StartedAt_Time;
    cm.Parameters.Add("@UpdatedAt_Time", SqlDbType.DateTime, 8).Value = model.UpdatedAt_Time;

    cn.Open();
    try
    {
        cm.ExecuteNonQuery();
    }
    finally
    {
        cn.Close();
    }
}
#endregion

#region  表 ChainlinkPriceOracle 的Delete操作
public static int Delete(string conStr, System.String Aggregator,System.Decimal roundId)
{
    string sql = @"Delete From ChainlinkPriceOracle Where Aggregator = @Aggregator and roundId = @roundId";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@Aggregator", SqlDbType.NVarChar, 43).Value = Aggregator;
    cm.Parameters.Add("@roundId", SqlDbType.Decimal, 17).Value = roundId;
    int RecordAffected = -1;
    cn.Open();
    try
    {
        RecordAffected = cm.ExecuteNonQuery();
    }
    finally
    {
        cn.Close();
    }
    return RecordAffected;
}
#endregion

#region  表 ChainlinkPriceOracle 的 Update 操作
public static int Update(string conStr, Model.ChainlinkPriceOracle model)
{
    string sql = @"Update ChainlinkPriceOracle Set answer = @answer, startedAt = @startedAt, updatedAt = @updatedAt, answeredInRound = @answeredInRound, StartedAt_Time = @StartedAt_Time, UpdatedAt_Time = @UpdatedAt_Time Where Aggregator = @Aggregator And roundId = @roundId ";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@Aggregator", SqlDbType.NVarChar, 43).Value = model.Aggregator;
    cm.Parameters.Add("@roundId", SqlDbType.Decimal, 17).Value = model.roundId;
    cm.Parameters.Add("@answer", SqlDbType.Decimal, 17).Value = model.answer;
    cm.Parameters.Add("@startedAt", SqlDbType.BigInt, 8).Value = model.startedAt;
    cm.Parameters.Add("@updatedAt", SqlDbType.BigInt, 8).Value = model.updatedAt;
    cm.Parameters.Add("@answeredInRound", SqlDbType.Decimal, 17).Value = model.answeredInRound;
    cm.Parameters.Add("@StartedAt_Time", SqlDbType.DateTime, 8).Value = model.StartedAt_Time;
    cm.Parameters.Add("@UpdatedAt_Time", SqlDbType.DateTime, 8).Value = model.UpdatedAt_Time;

    int RecordAffected = -1;
    cn.Open();
    try
    {
    RecordAffected = cm.ExecuteNonQuery();
    }
    finally
    {
        cn.Close();
    }
    return RecordAffected;
}
#endregion

#region  表 ChainlinkPriceOracle 的GetModel操作
public static Model.ChainlinkPriceOracle GetModel(string conStr, System.String Aggregator, System.Decimal roundId)
{
    string sql = @"select * from ChainlinkPriceOracle Where Aggregator = @Aggregator and roundId = @roundId ";

    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;
    cm.Parameters.Add("@Aggregator", SqlDbType.NVarChar, 43).Value = Aggregator;
    cm.Parameters.Add("@roundId", SqlDbType.Decimal, 17).Value = roundId;

    SqlDataAdapter da = new SqlDataAdapter();
    da.SelectCommand = cm;
    System.Data.DataSet ds = new System.Data.DataSet();
    da.Fill(ds);
    if (ds.Tables[0].Rows.Count == 1)
    {
        return Model.ChainlinkPriceOracle.DataRow2Object(ds.Tables[0].Rows[0]);
    }
    else if (ds.Tables[0].Rows.Count > 1)
    {
        throw new Exception("数据错误，对应多条记录！");
    }
    else
    {
        return null;
    }
}


#endregion

#region  表 ChainlinkPriceOracle 的 Exist 操作 判断
public static bool Exist(string conStr, System.String Aggregator, System.Decimal roundId)
{
    string sql = @"Select Count(*) From ChainlinkPriceOracle Where Aggregator = @Aggregator and roundId = @roundId ";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;
    cm.Parameters.Add("@Aggregator", SqlDbType.NVarChar, 43).Value = Aggregator;
    cm.Parameters.Add("@roundId", SqlDbType.Decimal, 17).Value = roundId;

            cn.Open();
            try
            {
                return Convert.ToInt32(cm.ExecuteScalar()) > 0;
            }
            finally
            {
                cn.Close();
            }   
}

#endregion

}


}