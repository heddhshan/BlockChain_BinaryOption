
//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/11/14 16:37:27
//
using System;
using System.Data;
using System.Collections.Generic;

namespace BlockChain.BinaryOptions.Model
{
[Serializable]
public class IBinaryOptions_OnOpen
{
    #region 生成该数据实体的SQL语句
    public const string SQL = @"Select Top(1) * From IBinaryOptions_OnOpen";
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

    private System.String __realWinnings = System.String.Empty;
    public System.String _realWinnings
    {
        get {return __realWinnings;}
        set {__realWinnings = value;}
    }

    private System.String __resultPrice = System.String.Empty;
    public System.String _resultPrice
    {
        get {return __resultPrice;}
        set {__resultPrice = value;}
    }

    private System.Int32 _PriceFormart;
    public System.Int32 PriceFormart
    {
        get {return _PriceFormart;}
        set {_PriceFormart = value;}
    }

    private System.Double _RealWinnings_Num;
    public System.Double RealWinnings_Num
    {
        get {return _RealWinnings_Num;}
        set {_RealWinnings_Num = value;}
    }

    private System.Double _EndPrice;
    public System.Double EndPrice
    {
        get {return _EndPrice;}
        set {_EndPrice = value;}
    }

    #endregion

    #region Public construct

    public IBinaryOptions_OnOpen()
    {
    }

    
    public IBinaryOptions_OnOpen(System.Int32 AChainId,System.String AContractAddress,System.Int64 A_roudId)
    {
        _ChainId = AChainId;
        _ContractAddress = AContractAddress;
        __roudId = A_roudId;
    }

    #endregion

    #region Public DataRow2Object

    public static IBinaryOptions_OnOpen DataRow2Object(System.Data.DataRow dr)
    {
        if (dr == null)
        {
            return null;
        }
        IBinaryOptions_OnOpen Obj = new IBinaryOptions_OnOpen();
        Obj.ChainId = dr["ChainId"] == DBNull.Value ? 0 : (System.Int32)(dr["ChainId"]);
        Obj.ContractAddress = dr["ContractAddress"] == DBNull.Value ? string.Empty : (System.String)(dr["ContractAddress"]);
        Obj.BlockNumber = dr["BlockNumber"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["BlockNumber"]);
        Obj.TransactionHash = dr["TransactionHash"] == DBNull.Value ? string.Empty : (System.String)(dr["TransactionHash"]);
        Obj.CreateTime = dr["CreateTime"] == DBNull.Value ? System.DateTime.Now : (System.DateTime)(dr["CreateTime"]);
        Obj._player = dr["_player"] == DBNull.Value ? string.Empty : (System.String)(dr["_player"]);
        Obj._roudId = dr["_roudId"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["_roudId"]);
        Obj._realWinnings = dr["_realWinnings"] == DBNull.Value ? string.Empty : (System.String)(dr["_realWinnings"]);
        Obj._resultPrice = dr["_resultPrice"] == DBNull.Value ? string.Empty : (System.String)(dr["_resultPrice"]);
        Obj.PriceFormart = dr["PriceFormart"] == DBNull.Value ? 0 : (System.Int32)(dr["PriceFormart"]);
        Obj.RealWinnings_Num = dr["RealWinnings_Num"] == DBNull.Value ? 0 : (System.Double)(dr["RealWinnings_Num"]);
        Obj.EndPrice = dr["EndPrice"] == DBNull.Value ? 0 : (System.Double)(dr["EndPrice"]);
        return Obj;
    }

    #endregion


        public void ValidateDataLen()
        {
if (this.ContractAddress != null && this.ContractAddress.Length > FL_ContractAddress && FL_ContractAddress > 0) throw new Exception("ContractAddress要求长度小于等于" + FL_ContractAddress.ToString() + "。");
if (this.TransactionHash != null && this.TransactionHash.Length > FL_TransactionHash && FL_TransactionHash > 0) throw new Exception("TransactionHash要求长度小于等于" + FL_TransactionHash.ToString() + "。");
if (this._player != null && this._player.Length > FL__player && FL__player > 0) throw new Exception("_player要求长度小于等于" + FL__player.ToString() + "。");
if (this._realWinnings != null && this._realWinnings.Length > FL__realWinnings && FL__realWinnings > 0) throw new Exception("_realWinnings要求长度小于等于" + FL__realWinnings.ToString() + "。");
if (this._resultPrice != null && this._resultPrice.Length > FL__resultPrice && FL__resultPrice > 0) throw new Exception("_resultPrice要求长度小于等于" + FL__resultPrice.ToString() + "。");
}


        public void ValidateNotEmpty()
        {
if (string.IsNullOrEmpty(this.ContractAddress) || string.IsNullOrEmpty(this.ContractAddress.Trim())) throw new Exception("ContractAddress要求不空。");
if (string.IsNullOrEmpty(this.TransactionHash) || string.IsNullOrEmpty(this.TransactionHash.Trim())) throw new Exception("TransactionHash要求不空。");
if (string.IsNullOrEmpty(this._player) || string.IsNullOrEmpty(this._player.Trim())) throw new Exception("_player要求不空。");
if (string.IsNullOrEmpty(this._realWinnings) || string.IsNullOrEmpty(this._realWinnings.Trim())) throw new Exception("_realWinnings要求不空。");
if (string.IsNullOrEmpty(this._resultPrice) || string.IsNullOrEmpty(this._resultPrice.Trim())) throw new Exception("_resultPrice要求不空。");
}



        public void ValidateEmptyAndLen()
        {
            this.ValidateDataLen();
            this.ValidateNotEmpty();
        }


public IBinaryOptions_OnOpen Copy()
{
    IBinaryOptions_OnOpen obj = new IBinaryOptions_OnOpen();
    obj.ChainId = this.ChainId;
    obj.ContractAddress = this.ContractAddress;
    obj.BlockNumber = this.BlockNumber;
    obj.TransactionHash = this.TransactionHash;
    obj.CreateTime = this.CreateTime;
    obj._player = this._player;
    obj._roudId = this._roudId;
    obj._realWinnings = this._realWinnings;
    obj._resultPrice = this._resultPrice;
    obj.PriceFormart = this.PriceFormart;
    obj.RealWinnings_Num = this.RealWinnings_Num;
    obj.EndPrice = this.EndPrice;
    return obj;
}



        public static List<IBinaryOptions_OnOpen> DataTable2List(System.Data.DataTable dt)
        {
            List<IBinaryOptions_OnOpen> result = new List<IBinaryOptions_OnOpen>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                result.Add(IBinaryOptions_OnOpen.DataRow2Object(dr));
            }
            return result;
        }



        private static IBinaryOptions_OnOpen _NullObject = null;

        public static  IBinaryOptions_OnOpen NullObject
        {
            get
            {
                if (_NullObject == null)
                {
                    _NullObject = new IBinaryOptions_OnOpen();
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
        /// 字段 _realWinnings 的长度——128
        /// </summary>
        public const int FL__realWinnings = 128;


        /// <summary>
        /// 字段 _resultPrice 的长度——128
        /// </summary>
        public const int FL__resultPrice = 128;


        /// <summary>
        /// 字段 PriceFormart 的长度——4
        /// </summary>
        public const int FL_PriceFormart = 4;


        /// <summary>
        /// 字段 RealWinnings_Num 的长度——8
        /// </summary>
        public const int FL_RealWinnings_Num = 8;


        /// <summary>
        /// 字段 EndPrice 的长度——8
        /// </summary>
        public const int FL_EndPrice = 8;


        #endregion
}



}