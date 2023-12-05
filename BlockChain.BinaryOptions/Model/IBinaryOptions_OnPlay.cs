
//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/11/14 16:37:28
//
using System;
using System.Data;
using System.Collections.Generic;

namespace BlockChain.BinaryOptions.Model
{
[Serializable]
public class IBinaryOptions_OnPlay
{
    #region 生成该数据实体的SQL语句
    public const string SQL = @"Select Top(1) * From IBinaryOptions_OnPlay";
    #endregion

    #region Public Properties

    private System.Int32 _ChainId;
    public System.Int32 ChainId
    {
        get {return _ChainId;}
        set {_ChainId = value;}
    }

    private System.String _ContractAddress = System.String.Empty;
    public System.String ContractAddress
    {
        get {return _ContractAddress;}
        set {_ContractAddress = value;}
    }

    private System.Int64 _BlockNumber;
    public System.Int64 BlockNumber
    {
        get {return _BlockNumber;}
        set {_BlockNumber = value;}
    }

    private System.String _TransactionHash = System.String.Empty;
    public System.String TransactionHash
    {
        get {return _TransactionHash;}
        set {_TransactionHash = value;}
    }

    private System.DateTime _CreateTime = System.DateTime.Now;
    public System.DateTime CreateTime
    {
        get {return _CreateTime;}
        set {_CreateTime = value;}
    }

    private System.String __player = System.String.Empty;
    public System.String _player
    {
        get {return __player;}
        set {__player = value;}
    }

    private System.Int64 __roudId;
    public System.Int64 _roudId
    {
        get {return __roudId;}
        set {__roudId = value;}
    }

    private System.String __selectedUpToken = System.String.Empty;
    public System.String _selectedUpToken
    {
        get {return __selectedUpToken;}
        set {__selectedUpToken = value;}
    }

    private System.String __amount = System.String.Empty;
    public System.String _amount
    {
        get {return __amount;}
        set {__amount = value;}
    }

    private System.String __winnings = System.String.Empty;
    public System.String _winnings
    {
        get {return __winnings;}
        set {__winnings = value;}
    }

    private System.Int32 _PriceFormart;
    public System.Int32 PriceFormart
    {
        get {return _PriceFormart;}
        set {_PriceFormart = value;}
    }

    private System.Double _Amount_Num;
    public System.Double Amount_Num
    {
        get {return _Amount_Num;}
        set {_Amount_Num = value;}
    }

    private System.Double _Winnings_Num;
    public System.Double Winnings_Num
    {
        get {return _Winnings_Num;}
        set {_Winnings_Num = value;}
    }

    #endregion

    #region Public construct

    public IBinaryOptions_OnPlay()
    {
    }

    
    public IBinaryOptions_OnPlay(System.Int32 AChainId,System.String AContractAddress,System.Int64 A_roudId)
    {
        _ChainId = AChainId;
        _ContractAddress = AContractAddress;
        __roudId = A_roudId;
    }

    #endregion

    #region Public DataRow2Object

    public static IBinaryOptions_OnPlay DataRow2Object(System.Data.DataRow dr)
    {
        if (dr == null)
        {
            return null;
        }
        IBinaryOptions_OnPlay Obj = new IBinaryOptions_OnPlay();
        Obj.ChainId = dr["ChainId"] == DBNull.Value ? 0 : (System.Int32)(dr["ChainId"]);
        Obj.ContractAddress = dr["ContractAddress"] == DBNull.Value ? string.Empty : (System.String)(dr["ContractAddress"]);
        Obj.BlockNumber = dr["BlockNumber"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["BlockNumber"]);
        Obj.TransactionHash = dr["TransactionHash"] == DBNull.Value ? string.Empty : (System.String)(dr["TransactionHash"]);
        Obj.CreateTime = dr["CreateTime"] == DBNull.Value ? System.DateTime.Now : (System.DateTime)(dr["CreateTime"]);
        Obj._player = dr["_player"] == DBNull.Value ? string.Empty : (System.String)(dr["_player"]);
        Obj._roudId = dr["_roudId"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["_roudId"]);
        Obj._selectedUpToken = dr["_selectedUpToken"] == DBNull.Value ? string.Empty : (System.String)(dr["_selectedUpToken"]);
        Obj._amount = dr["_amount"] == DBNull.Value ? string.Empty : (System.String)(dr["_amount"]);
        Obj._winnings = dr["_winnings"] == DBNull.Value ? string.Empty : (System.String)(dr["_winnings"]);
        Obj.PriceFormart = dr["PriceFormart"] == DBNull.Value ? 0 : (System.Int32)(dr["PriceFormart"]);
        Obj.Amount_Num = dr["Amount_Num"] == DBNull.Value ? 0 : (System.Double)(dr["Amount_Num"]);
        Obj.Winnings_Num = dr["Winnings_Num"] == DBNull.Value ? 0 : (System.Double)(dr["Winnings_Num"]);
        return Obj;
    }

    #endregion


        public void ValidateDataLen()
        {
if (this.ContractAddress != null && this.ContractAddress.Length > FL_ContractAddress && FL_ContractAddress > 0) throw new Exception("ContractAddress要求长度小于等于" + FL_ContractAddress.ToString() + "。");
if (this.TransactionHash != null && this.TransactionHash.Length > FL_TransactionHash && FL_TransactionHash > 0) throw new Exception("TransactionHash要求长度小于等于" + FL_TransactionHash.ToString() + "。");
if (this._player != null && this._player.Length > FL__player && FL__player > 0) throw new Exception("_player要求长度小于等于" + FL__player.ToString() + "。");
if (this._selectedUpToken != null && this._selectedUpToken.Length > FL__selectedUpToken && FL__selectedUpToken > 0) throw new Exception("_selectedUpToken要求长度小于等于" + FL__selectedUpToken.ToString() + "。");
if (this._amount != null && this._amount.Length > FL__amount && FL__amount > 0) throw new Exception("_amount要求长度小于等于" + FL__amount.ToString() + "。");
if (this._winnings != null && this._winnings.Length > FL__winnings && FL__winnings > 0) throw new Exception("_winnings要求长度小于等于" + FL__winnings.ToString() + "。");
}


        public void ValidateNotEmpty()
        {
if (string.IsNullOrEmpty(this.ContractAddress) || string.IsNullOrEmpty(this.ContractAddress.Trim())) throw new Exception("ContractAddress要求不空。");
if (string.IsNullOrEmpty(this.TransactionHash) || string.IsNullOrEmpty(this.TransactionHash.Trim())) throw new Exception("TransactionHash要求不空。");
if (string.IsNullOrEmpty(this._player) || string.IsNullOrEmpty(this._player.Trim())) throw new Exception("_player要求不空。");
if (string.IsNullOrEmpty(this._selectedUpToken) || string.IsNullOrEmpty(this._selectedUpToken.Trim())) throw new Exception("_selectedUpToken要求不空。");
if (string.IsNullOrEmpty(this._amount) || string.IsNullOrEmpty(this._amount.Trim())) throw new Exception("_amount要求不空。");
if (string.IsNullOrEmpty(this._winnings) || string.IsNullOrEmpty(this._winnings.Trim())) throw new Exception("_winnings要求不空。");
}



        public void ValidateEmptyAndLen()
        {
            this.ValidateDataLen();
            this.ValidateNotEmpty();
        }


public IBinaryOptions_OnPlay Copy()
{
    IBinaryOptions_OnPlay obj = new IBinaryOptions_OnPlay();
    obj.ChainId = this.ChainId;
    obj.ContractAddress = this.ContractAddress;
    obj.BlockNumber = this.BlockNumber;
    obj.TransactionHash = this.TransactionHash;
    obj.CreateTime = this.CreateTime;
    obj._player = this._player;
    obj._roudId = this._roudId;
    obj._selectedUpToken = this._selectedUpToken;
    obj._amount = this._amount;
    obj._winnings = this._winnings;
    obj.PriceFormart = this.PriceFormart;
    obj.Amount_Num = this.Amount_Num;
    obj.Winnings_Num = this.Winnings_Num;
    return obj;
}



        public static List<IBinaryOptions_OnPlay> DataTable2List(System.Data.DataTable dt)
        {
            List<IBinaryOptions_OnPlay> result = new List<IBinaryOptions_OnPlay>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                result.Add(IBinaryOptions_OnPlay.DataRow2Object(dr));
            }
            return result;
        }



        private static IBinaryOptions_OnPlay _NullObject = null;

        public static  IBinaryOptions_OnPlay NullObject
        {
            get
            {
                if (_NullObject == null)
                {
                    _NullObject = new IBinaryOptions_OnPlay();
                }
                return _NullObject;
            }
        }


        #region 字段长度

        /// <summary>
        /// 字段 ChainId 的长度——4
        /// </summary>
        public const int FL_ChainId = 4;


        /// <summary>
        /// 字段 ContractAddress 的长度——43
        /// </summary>
        public const int FL_ContractAddress = 43;


        /// <summary>
        /// 字段 BlockNumber 的长度——8
        /// </summary>
        public const int FL_BlockNumber = 8;


        /// <summary>
        /// 字段 TransactionHash 的长度——66
        /// </summary>
        public const int FL_TransactionHash = 66;


        /// <summary>
        /// 字段 CreateTime 的长度——8
        /// </summary>
        public const int FL_CreateTime = 8;


        /// <summary>
        /// 字段 _player 的长度——64
        /// </summary>
        public const int FL__player = 64;


        /// <summary>
        /// 字段 _roudId 的长度——8
        /// </summary>
        public const int FL__roudId = 8;


        /// <summary>
        /// 字段 _selectedUpToken 的长度——43
        /// </summary>
        public const int FL__selectedUpToken = 43;


        /// <summary>
        /// 字段 _amount 的长度——128
        /// </summary>
        public const int FL__amount = 128;


        /// <summary>
        /// 字段 _winnings 的长度——128
        /// </summary>
        public const int FL__winnings = 128;


        /// <summary>
        /// 字段 PriceFormart 的长度——4
        /// </summary>
        public const int FL_PriceFormart = 4;


        /// <summary>
        /// 字段 Amount_Num 的长度——8
        /// </summary>
        public const int FL_Amount_Num = 8;


        /// <summary>
        /// 字段 Winnings_Num 的长度——8
        /// </summary>
        public const int FL_Winnings_Num = 8;


        #endregion
}



}