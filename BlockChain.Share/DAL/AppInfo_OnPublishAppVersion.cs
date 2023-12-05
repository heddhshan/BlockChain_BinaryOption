//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/4/6 13:27:11
//
using System;
using System.Data;
using System.Data.SqlClient;


namespace BlockChain.Share.DAL
{
internal class AppInfo_OnPublishAppVersion
{
#region 对应的数据库表 AppInfo_OnPublishAppVersion
public const string TableName = @"AppInfo_OnPublishAppVersion";
#endregion

#region  表 AppInfo_OnPublishAppVersion 的Insert操作
public static void Insert(string conStr, Model.AppInfo_OnPublishAppVersion model)
{
    string sql = @"insert into AppInfo_OnPublishAppVersion (ChainId,ContractAddress,BlockNumber,TransactionHash,_eventId,_AppId,_PlatformId,_Version,_Sha256Value,_AppName,_UpdateInfo,_IconUri) values (@ChainId,@ContractAddress,@BlockNumber,@TransactionHash,@_eventId,@_AppId,@_PlatformId,@_Version,@_Sha256Value,@_AppName,@_UpdateInfo,@_IconUri)";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = model.ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = model.ContractAddress;
    cm.Parameters.Add("@BlockNumber", SqlDbType.BigInt, 8).Value = model.BlockNumber;
    cm.Parameters.Add("@TransactionHash", SqlDbType.NVarChar, 66).Value = model.TransactionHash;
    cm.Parameters.Add("@_eventId", SqlDbType.BigInt, 8).Value = model._eventId;
    cm.Parameters.Add("@_AppId", SqlDbType.Int, 4).Value = model._AppId;
    cm.Parameters.Add("@_PlatformId", SqlDbType.Int, 4).Value = model._PlatformId;
    cm.Parameters.Add("@_Version", SqlDbType.Int, 4).Value = model._Version;
    cm.Parameters.Add("@_Sha256Value", SqlDbType.NVarChar, 128).Value = model._Sha256Value;
    cm.Parameters.Add("@_AppName", SqlDbType.NVarChar, 128).Value = model._AppName;
    cm.Parameters.Add("@_UpdateInfo", SqlDbType.NVarChar, 1024).Value = model._UpdateInfo;
    cm.Parameters.Add("@_IconUri", SqlDbType.NVarChar, 1024).Value = model._IconUri;

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

#region  表 AppInfo_OnPublishAppVersion 的Delete操作
public static int Delete(string conStr, System.Int32 ChainId,System.String ContractAddress,System.Int64 _eventId)
{
    string sql = @"Delete From AppInfo_OnPublishAppVersion Where ChainId = @ChainId and ContractAddress = @ContractAddress and _eventId = @_eventId";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@_eventId", SqlDbType.BigInt, 8).Value = _eventId;
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

#region  表 AppInfo_OnPublishAppVersion 的 Update 操作
public static int Update(string conStr, Model.AppInfo_OnPublishAppVersion model)
{
    string sql = @"Update AppInfo_OnPublishAppVersion Set BlockNumber = @BlockNumber, TransactionHash = @TransactionHash, _AppId = @_AppId, _PlatformId = @_PlatformId, _Version = @_Version, _Sha256Value = @_Sha256Value, _AppName = @_AppName, _UpdateInfo = @_UpdateInfo, _IconUri = @_IconUri Where ChainId = @ChainId And ContractAddress = @ContractAddress And _eventId = @_eventId ";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = model.ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = model.ContractAddress;
    cm.Parameters.Add("@BlockNumber", SqlDbType.BigInt, 8).Value = model.BlockNumber;
    cm.Parameters.Add("@TransactionHash", SqlDbType.NVarChar, 66).Value = model.TransactionHash;
    cm.Parameters.Add("@_eventId", SqlDbType.BigInt, 8).Value = model._eventId;
    cm.Parameters.Add("@_AppId", SqlDbType.Int, 4).Value = model._AppId;
    cm.Parameters.Add("@_PlatformId", SqlDbType.Int, 4).Value = model._PlatformId;
    cm.Parameters.Add("@_Version", SqlDbType.Int, 4).Value = model._Version;
    cm.Parameters.Add("@_Sha256Value", SqlDbType.NVarChar, 128).Value = model._Sha256Value;
    cm.Parameters.Add("@_AppName", SqlDbType.NVarChar, 128).Value = model._AppName;
    cm.Parameters.Add("@_UpdateInfo", SqlDbType.NVarChar, 1024).Value = model._UpdateInfo;
    cm.Parameters.Add("@_IconUri", SqlDbType.NVarChar, 1024).Value = model._IconUri;

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

#region  表 AppInfo_OnPublishAppVersion 的GetModel操作
public static Model.AppInfo_OnPublishAppVersion GetModel(string conStr, System.Int32 ChainId, System.String ContractAddress, System.Int64 _eventId)
{
    string sql = @"select * from AppInfo_OnPublishAppVersion Where ChainId = @ChainId and ContractAddress = @ContractAddress and _eventId = @_eventId ";

    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;
    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@_eventId", SqlDbType.BigInt, 8).Value = _eventId;

    SqlDataAdapter da = new SqlDataAdapter();
    da.SelectCommand = cm;
    System.Data.DataSet ds = new System.Data.DataSet();
    da.Fill(ds);
    if (ds.Tables[0].Rows.Count == 1)
    {
        return Model.AppInfo_OnPublishAppVersion.DataRow2Object(ds.Tables[0].Rows[0]);
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

#region  表 AppInfo_OnPublishAppVersion 的 Exist 操作 判断
public static bool Exist(string conStr, System.Int32 ChainId, System.String ContractAddress, System.Int64 _eventId)
{
    string sql = @"Select Count(*) From AppInfo_OnPublishAppVersion Where ChainId = @ChainId and ContractAddress = @ContractAddress and _eventId = @_eventId ";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;
    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@_eventId", SqlDbType.BigInt, 8).Value = _eventId;

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