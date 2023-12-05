//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/11/14 16:37:52
//
using System;
using System.Data;
using System.Data.SqlClient;


namespace BlockChain.BinaryOptions.DAL
{
internal class IBinaryOptions_OnPlay
{
#region 对应的数据库表 IBinaryOptions_OnPlay
public const string TableName = @"IBinaryOptions_OnPlay";
#endregion

#region  表 IBinaryOptions_OnPlay 的Insert操作
public static void Insert(string conStr, Model.IBinaryOptions_OnPlay model)
{
    string sql = @"insert into IBinaryOptions_OnPlay (ChainId,ContractAddress,BlockNumber,TransactionHash,CreateTime,_player,_roudId,_selectedUpToken,_amount,_winnings,PriceFormart,Amount_Num,Winnings_Num) values (@ChainId,@ContractAddress,@BlockNumber,@TransactionHash,@CreateTime,@_player,@_roudId,@_selectedUpToken,@_amount,@_winnings,@PriceFormart,@Amount_Num,@Winnings_Num)";
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
    cm.Parameters.Add("@_selectedUpToken", SqlDbType.NVarChar, 43).Value = model._selectedUpToken;
    cm.Parameters.Add("@_amount", SqlDbType.NVarChar, 128).Value = model._amount;
    cm.Parameters.Add("@_winnings", SqlDbType.NVarChar, 128).Value = model._winnings;
    cm.Parameters.Add("@PriceFormart", SqlDbType.Int, 4).Value = model.PriceFormart;
    cm.Parameters.Add("@Amount_Num", SqlDbType.Float, 8).Value = model.Amount_Num;
    cm.Parameters.Add("@Winnings_Num", SqlDbType.Float, 8).Value = model.Winnings_Num;

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

#region  表 IBinaryOptions_OnPlay 的Delete操作
public static int Delete(string conStr, System.Int32 ChainId,System.String ContractAddress,System.Int64 _roudId)
{
    string sql = @"Delete From IBinaryOptions_OnPlay Where ChainId = @ChainId and ContractAddress = @ContractAddress and _roudId = @_roudId";
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

#region  表 IBinaryOptions_OnPlay 的 Update 操作
public static int Update(string conStr, Model.IBinaryOptions_OnPlay model)
{
    string sql = @"Update IBinaryOptions_OnPlay Set BlockNumber = @BlockNumber, TransactionHash = @TransactionHash, CreateTime = @CreateTime, _player = @_player, _selectedUpToken = @_selectedUpToken, _amount = @_amount, _winnings = @_winnings, PriceFormart = @PriceFormart, Amount_Num = @Amount_Num, Winnings_Num = @Winnings_Num Where ChainId = @ChainId And ContractAddress = @ContractAddress And _roudId = @_roudId ";
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
    cm.Parameters.Add("@_selectedUpToken", SqlDbType.NVarChar, 43).Value = model._selectedUpToken;
    cm.Parameters.Add("@_amount", SqlDbType.NVarChar, 128).Value = model._amount;
    cm.Parameters.Add("@_winnings", SqlDbType.NVarChar, 128).Value = model._winnings;
    cm.Parameters.Add("@PriceFormart", SqlDbType.Int, 4).Value = model.PriceFormart;
    cm.Parameters.Add("@Amount_Num", SqlDbType.Float, 8).Value = model.Amount_Num;
    cm.Parameters.Add("@Winnings_Num", SqlDbType.Float, 8).Value = model.Winnings_Num;

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

#region  表 IBinaryOptions_OnPlay 的GetModel操作
public static Model.IBinaryOptions_OnPlay GetModel(string conStr, System.Int32 ChainId, System.String ContractAddress, System.Int64 _roudId)
{
    string sql = @"select * from IBinaryOptions_OnPlay Where ChainId = @ChainId and ContractAddress = @ContractAddress and _roudId = @_roudId ";

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
        return Model.IBinaryOptions_OnPlay.DataRow2Object(ds.Tables[0].Rows[0]);
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

#region  表 IBinaryOptions_OnPlay 的 Exist 操作 判断
public static bool Exist(string conStr, System.Int32 ChainId, System.String ContractAddress, System.Int64 _roudId)
{
    string sql = @"Select Count(*) From IBinaryOptions_OnPlay Where ChainId = @ChainId and ContractAddress = @ContractAddress and _roudId = @_roudId ";
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