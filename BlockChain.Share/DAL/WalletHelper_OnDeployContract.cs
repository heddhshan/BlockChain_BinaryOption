//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/5/17 16:10:42
//
using System;
using System.Data;
using System.Data.SqlClient;


namespace BlockChain.Share.DAL
{
internal class WalletHelper_OnDeployContract
{
#region 对应的数据库表 WalletHelper_OnDeployContract
public const string TableName = @"WalletHelper_OnDeployContract";
#endregion

#region  表 WalletHelper_OnDeployContract 的Insert操作
public static void Insert(string conStr, Model.WalletHelper_OnDeployContract model)
{
    string sql = @"insert into WalletHelper_OnDeployContract (ChainId,ContractAddress,BlockNumber,TransactionHash,_contract,_user,_amount,_salt,_bytecodeHash,_amount_Text) values (@ChainId,@ContractAddress,@BlockNumber,@TransactionHash,@_contract,@_user,@_amount,@_salt,@_bytecodeHash,@_amount_Text)";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = model.ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = model.ContractAddress;
    cm.Parameters.Add("@BlockNumber", SqlDbType.BigInt, 8).Value = model.BlockNumber;
    cm.Parameters.Add("@TransactionHash", SqlDbType.NVarChar, 66).Value = model.TransactionHash;
    cm.Parameters.Add("@_contract", SqlDbType.NVarChar, 43).Value = model._contract;
    cm.Parameters.Add("@_user", SqlDbType.NVarChar, 43).Value = model._user;
    cm.Parameters.Add("@_amount", SqlDbType.Float, 8).Value = model._amount;
    cm.Parameters.Add("@_salt", SqlDbType.NVarChar, 66).Value = model._salt;
    cm.Parameters.Add("@_bytecodeHash", SqlDbType.NVarChar, 66).Value = model._bytecodeHash;
    cm.Parameters.Add("@_amount_Text", SqlDbType.NVarChar, 80).Value = model._amount_Text;

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

#region  表 WalletHelper_OnDeployContract 的Delete操作
public static int Delete(string conStr, System.Int32 ChainId,System.String ContractAddress,System.String TransactionHash)
{
    string sql = @"Delete From WalletHelper_OnDeployContract Where ChainId = @ChainId and ContractAddress = @ContractAddress and TransactionHash = @TransactionHash";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@TransactionHash", SqlDbType.NVarChar, 66).Value = TransactionHash;
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

#region  表 WalletHelper_OnDeployContract 的 Update 操作
public static int Update(string conStr, Model.WalletHelper_OnDeployContract model)
{
    string sql = @"Update WalletHelper_OnDeployContract Set BlockNumber = @BlockNumber, _contract = @_contract, _user = @_user, _amount = @_amount, _salt = @_salt, _bytecodeHash = @_bytecodeHash, _amount_Text = @_amount_Text Where ChainId = @ChainId And ContractAddress = @ContractAddress And TransactionHash = @TransactionHash ";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;

    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = model.ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = model.ContractAddress;
    cm.Parameters.Add("@BlockNumber", SqlDbType.BigInt, 8).Value = model.BlockNumber;
    cm.Parameters.Add("@TransactionHash", SqlDbType.NVarChar, 66).Value = model.TransactionHash;
    cm.Parameters.Add("@_contract", SqlDbType.NVarChar, 43).Value = model._contract;
    cm.Parameters.Add("@_user", SqlDbType.NVarChar, 43).Value = model._user;
    cm.Parameters.Add("@_amount", SqlDbType.Float, 8).Value = model._amount;
    cm.Parameters.Add("@_salt", SqlDbType.NVarChar, 66).Value = model._salt;
    cm.Parameters.Add("@_bytecodeHash", SqlDbType.NVarChar, 66).Value = model._bytecodeHash;
    cm.Parameters.Add("@_amount_Text", SqlDbType.NVarChar, 80).Value = model._amount_Text;

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

#region  表 WalletHelper_OnDeployContract 的GetModel操作
public static Model.WalletHelper_OnDeployContract GetModel(string conStr, System.Int32 ChainId, System.String ContractAddress, System.String TransactionHash)
{
    string sql = @"select * from WalletHelper_OnDeployContract Where ChainId = @ChainId and ContractAddress = @ContractAddress and TransactionHash = @TransactionHash ";

    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;
    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@TransactionHash", SqlDbType.NVarChar, 66).Value = TransactionHash;

    SqlDataAdapter da = new SqlDataAdapter();
    da.SelectCommand = cm;
    System.Data.DataSet ds = new System.Data.DataSet();
    da.Fill(ds);
    if (ds.Tables[0].Rows.Count == 1)
    {
        return Model.WalletHelper_OnDeployContract.DataRow2Object(ds.Tables[0].Rows[0]);
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

#region  表 WalletHelper_OnDeployContract 的 Exist 操作 判断
public static bool Exist(string conStr, System.Int32 ChainId, System.String ContractAddress, System.String TransactionHash)
{
    string sql = @"Select Count(*) From WalletHelper_OnDeployContract Where ChainId = @ChainId and ContractAddress = @ContractAddress and TransactionHash = @TransactionHash ";
    SqlConnection cn = new SqlConnection(conStr);
    SqlCommand cm = new SqlCommand();
    cm.Connection = cn;
    cm.CommandType = System.Data.CommandType.Text;
    cm.CommandText = sql;
    cm.Parameters.Add("@ChainId", SqlDbType.Int, 4).Value = ChainId;
    cm.Parameters.Add("@ContractAddress", SqlDbType.NVarChar, 43).Value = ContractAddress;
    cm.Parameters.Add("@TransactionHash", SqlDbType.NVarChar, 66).Value = TransactionHash;

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