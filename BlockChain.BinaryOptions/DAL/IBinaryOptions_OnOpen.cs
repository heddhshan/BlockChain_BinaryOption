//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/11/14 16:37:52
//
using System;
using System.Data;
using System.Data.SqlClient;


namespace BlockChain.BinaryOptions.DAL
{
internal class IBinaryOptions_OnOpen
{
#region 对应的数据库表 IBinaryOptions_OnOpen
public const string TableName = @"IBinaryOptions_OnOpen";
#endregion

#region  表 IBinaryOptions_OnOpen 的Insert操作
public static void Insert(string conStr, Model.IBinaryOptions_OnOpen model)
{
    string sql = @"insert into IBinaryOptions_OnOpen (ChainId,ContractAddress,BlockNumber,TransactionHash,CreateTime,_player,_roudId,_realWinnings,_resultPrice,PriceFormart,RealWinnings_Num,EndPrice) values (@ChainId,@ContractAddress,@BlockNumber,@TransactionHash,@CreateTime,@_player,@_roudId,@_realWinnings,@_resultPrice,@PriceFormart,@RealWinnings_Num,@EndPrice)";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = model.ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = model.ContractAddress;
    cm.Parameters.Add("@BlockNumber", SqlDbType.BigInt, 8).Value = model.BlockNumber;
    cm.Parameters.Add("@TransactionHash", SqlDbType.NVarChar, 66).Value = model.TransactionHash;
    cm.Parameters.Add("@CreateTime", SqlDbType.DateTime, 8).Value = model.CreateTime;
    cm.Parameters.Add("@_player", SqlDbType.NVarChar, 64).Value = model._player;
    cm.Parameters.Add("@_roudId", SqlDbType.BigInt, 8).Value = model._roudId;
    cm.Parameters.Add("@_realWinnings", SqlDbType.NVarChar, 128).Value = model._realWinnings;
    cm.Parameters.Add("@_resultPrice", SqlDbType.NVarChar, 128).Value = model._resultPrice;
    cm.Parameters.Add("@PriceFormart", SqlDbType.Int, 4).Value = model.PriceFormart;
    cm.Parameters.Add("@RealWinnings_Num", SqlDbType.Float, 8).Value = model.RealWinnings_Num;
    cm.Parameters.Add("@EndPrice", SqlDbType.Float, 8).Value = model.EndPrice;

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

#region  表 IBinaryOptions_OnOpen 的Delete操作
public static int Delete(string conStr, System.Int32 ChainId,System.String ContractAddress,System.Int64 _roudId)
{
    string sql = @"Delete From IBinaryOptions_OnOpen Where ChainId = @ChainId and ContractAddress = @ContractAddress and _roudId = @_roudId";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@_roudId", SqlDbType.BigInt, 8).Value = _roudId;
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

#region  表 IBinaryOptions_OnOpen 的 Update 操作
public static int Update(string conStr, Model.IBinaryOptions_OnOpen model)
{
    string sql = @"Update IBinaryOptions_OnOpen Set BlockNumber = @BlockNumber, TransactionHash = @TransactionHash, CreateTime = @CreateTime, _player = @_player, _realWinnings = @_realWinnings, _resultPrice = @_resultPrice, PriceFormart = @PriceFormart, RealWinnings_Num = @RealWinnings_Num, EndPrice = @EndPrice Where ChainId = @ChainId And ContractAddress = @ContractAddress And _roudId = @_roudId ";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = model.ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = model.ContractAddress;
    cm.Parameters.Add("@BlockNumber", SqlDbType.BigInt, 8).Value = model.BlockNumber;
    cm.Parameters.Add("@TransactionHash", SqlDbType.NVarChar, 66).Value = model.TransactionHash;
    cm.Parameters.Add("@CreateTime", SqlDbType.DateTime, 8).Value = model.CreateTime;
    cm.Parameters.Add("@_player", SqlDbType.NVarChar, 64).Value = model._player;
    cm.Parameters.Add("@_roudId", SqlDbType.BigInt, 8).Value = model._roudId;
    cm.Parameters.Add("@_realWinnings", SqlDbType.NVarChar, 128).Value = model._realWinnings;
    cm.Parameters.Add("@_resultPrice", SqlDbType.NVarChar, 128).Value = model._resultPrice;
    cm.Parameters.Add("@PriceFormart", SqlDbType.Int, 4).Value = model.PriceFormart;
    cm.Parameters.Add("@RealWinnings_Num", SqlDbType.Float, 8).Value = model.RealWinnings_Num;
    cm.Parameters.Add("@EndPrice", SqlDbType.Float, 8).Value = model.EndPrice;

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

#region  表 IBinaryOptions_OnOpen 的GetModel操作
public static Model.IBinaryOptions_OnOpen GetModel(string conStr, System.Int32 ChainId, System.String ContractAddress, System.Int64 _roudId)
{
    string sql = @"select * from IBinaryOptions_OnOpen Where ChainId = @ChainId and ContractAddress = @ContractAddress and _roudId = @_roudId ";

    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;
    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@_roudId", SqlDbType.BigInt, 8).Value = _roudId;

    SqlDataAdapter da = new SqlDataAdapter();
    da.SelectCommand = cm;
    System.Data.DataSet ds = new System.Data.DataSet();
    da.Fill(ds);
    if (ds.Tables[0].Rows.Count == 1)
    {
        return Model.IBinaryOptions_OnOpen.DataRow2Object(ds.Tables[0].Rows[0]);
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

#region  表 IBinaryOptions_OnOpen 的 Exist 操作 判断
public static bool Exist(string conStr, System.Int32 ChainId, System.String ContractAddress, System.Int64 _roudId)
{
    string sql = @"Select Count(*) From IBinaryOptions_OnOpen Where ChainId = @ChainId and ContractAddress = @ContractAddress and _roudId = @_roudId ";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;
    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@_roudId", SqlDbType.BigInt, 8).Value = _roudId;

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