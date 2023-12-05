//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/11/14 16:37:53
//
using System;
using System.Data;
using System.Data.SqlClient;


namespace BlockChain.BinaryOptions.DAL
{
internal class IBinaryOptions_TimePrice
{
#region 对应的数据库表 IBinaryOptions_TimePrice
public const string TableName = @"IBinaryOptions_TimePrice";
#endregion

#region  表 IBinaryOptions_TimePrice 的Insert操作
public static void Insert(string conStr, Model.IBinaryOptions_TimePrice model)
{
    string sql = @"insert into IBinaryOptions_TimePrice (ChainId,ContractAddress,BlockNumber,CreateTime,TimePrice_Time,TimePrice_Price,PriceFormart,BeginPrice,BeginTime) values (@ChainId,@ContractAddress,@BlockNumber,@CreateTime,@TimePrice_Time,@TimePrice_Price,@PriceFormart,@BeginPrice,@BeginTime)";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = model.ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = model.ContractAddress;
    cm.Parameters.Add("@BlockNumber", SqlDbType.BigInt, 8).Value = model.BlockNumber;
    cm.Parameters.Add("@CreateTime", SqlDbType.DateTime, 8).Value = model.CreateTime;
    cm.Parameters.Add("@TimePrice_Time", SqlDbType.BigInt, 8).Value = model.TimePrice_Time;
    cm.Parameters.Add("@TimePrice_Price", SqlDbType.NVarChar, 128).Value = model.TimePrice_Price;
    cm.Parameters.Add("@PriceFormart", SqlDbType.Int, 4).Value = model.PriceFormart;
    cm.Parameters.Add("@BeginPrice", SqlDbType.Float, 8).Value = model.BeginPrice;
    cm.Parameters.Add("@BeginTime", SqlDbType.DateTime, 8).Value = model.BeginTime;

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

#region  表 IBinaryOptions_TimePrice 的Delete操作
public static int Delete(string conStr, System.Int32 ChainId,System.String ContractAddress,System.Int64 BlockNumber)
{
    string sql = @"Delete From IBinaryOptions_TimePrice Where ChainId = @ChainId and ContractAddress = @ContractAddress and BlockNumber = @BlockNumber";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@BlockNumber", SqlDbType.BigInt, 8).Value = BlockNumber;
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

#region  表 IBinaryOptions_TimePrice 的 Update 操作
public static int Update(string conStr, Model.IBinaryOptions_TimePrice model)
{
    string sql = @"Update IBinaryOptions_TimePrice Set CreateTime = @CreateTime, TimePrice_Time = @TimePrice_Time, TimePrice_Price = @TimePrice_Price, PriceFormart = @PriceFormart, BeginPrice = @BeginPrice, BeginTime = @BeginTime Where ChainId = @ChainId And ContractAddress = @ContractAddress And BlockNumber = @BlockNumber ";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = model.ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = model.ContractAddress;
    cm.Parameters.Add("@BlockNumber", SqlDbType.BigInt, 8).Value = model.BlockNumber;
    cm.Parameters.Add("@CreateTime", SqlDbType.DateTime, 8).Value = model.CreateTime;
    cm.Parameters.Add("@TimePrice_Time", SqlDbType.BigInt, 8).Value = model.TimePrice_Time;
    cm.Parameters.Add("@TimePrice_Price", SqlDbType.NVarChar, 128).Value = model.TimePrice_Price;
    cm.Parameters.Add("@PriceFormart", SqlDbType.Int, 4).Value = model.PriceFormart;
    cm.Parameters.Add("@BeginPrice", SqlDbType.Float, 8).Value = model.BeginPrice;
    cm.Parameters.Add("@BeginTime", SqlDbType.DateTime, 8).Value = model.BeginTime;

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

#region  表 IBinaryOptions_TimePrice 的GetModel操作
public static Model.IBinaryOptions_TimePrice GetModel(string conStr, System.Int32 ChainId, System.String ContractAddress, System.Int64 BlockNumber)
{
    string sql = @"select * from IBinaryOptions_TimePrice Where ChainId = @ChainId and ContractAddress = @ContractAddress and BlockNumber = @BlockNumber ";

    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;
    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@BlockNumber", SqlDbType.BigInt, 8).Value = BlockNumber;

    SqlDataAdapter da = new SqlDataAdapter();
    da.SelectCommand = cm;
    System.Data.DataSet ds = new System.Data.DataSet();
    da.Fill(ds);
    if (ds.Tables[0].Rows.Count == 1)
    {
        return Model.IBinaryOptions_TimePrice.DataRow2Object(ds.Tables[0].Rows[0]);
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

#region  表 IBinaryOptions_TimePrice 的 Exist 操作 判断
public static bool Exist(string conStr, System.Int32 ChainId, System.String ContractAddress, System.Int64 BlockNumber)
{
    string sql = @"Select Count(*) From IBinaryOptions_TimePrice Where ChainId = @ChainId and ContractAddress = @ContractAddress and BlockNumber = @BlockNumber ";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;
    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@BlockNumber", SqlDbType.BigInt, 8).Value = BlockNumber;

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