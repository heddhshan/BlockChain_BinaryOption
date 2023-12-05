
//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/3/8 17:58:41
//
using System;
using System.Data;
using System.Collections.Generic;

namespace BlockChain.Share.Model
{
[Serializable]
public class DivShareTokenPair_DividendsDistributed
{
    #region 生成该数据实体的SQL语句
    public const string SQL = @"Select Top(1) * From DivShareTokenPair_DividendsDistributed";
    #endregion

    #region Public Properties

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

    private System.String _AssTokenAddress = System.String.Empty;
    public System.String AssTokenAddress
    {
        get {return _AssTokenAddress;}
        set {_AssTokenAddress = value;}
    }

    private System.Int64 __divIndex;
    public System.Int64 _divIndex
    {
        get {return __divIndex;}
        set {__divIndex = value;}
    }

    private System.String __from = System.String.Empty;
    public System.String _from
    {
        get {return __from;}
        set {__from = value;}
    }

    private System.Decimal __inAssets;
    public System.Decimal _inAssets
    {
        get {return __inAssets;}
        set {__inAssets = value;}
    }

    private System.Decimal __dividendAssets;
    public System.Decimal _dividendAssets
    {
        get {return __dividendAssets;}
        set {__dividendAssets = value;}
    }

    private System.Decimal __currentLiqAmount;
    public System.Decimal _currentLiqAmount
    {
        get {return __currentLiqAmount;}
        set {__currentLiqAmount = value;}
    }

    private System.String __inAssets_Text = System.String.Empty;
    public System.String _inAssets_Text
    {
        get {return __inAssets_Text;}
        set {__inAssets_Text = value;}
    }

    private System.String __dividendAssets_Text = System.String.Empty;
    public System.String _dividendAssets_Text
    {
        get {return __dividendAssets_Text;}
        set {__dividendAssets_Text = value;}
    }

    private System.String __currentLiqAmount_Text = System.String.Empty;
    public System.String _currentLiqAmount_Text
    {
        get {return __currentLiqAmount_Text;}
        set {__currentLiqAmount_Text = value;}
    }

    private System.String __heightBefore_Text = System.String.Empty;
    public System.String _heightBefore_Text
    {
        get {return __heightBefore_Text;}
        set {__heightBefore_Text = value;}
    }

    private System.String __heightAfter_Text = System.String.Empty;
    public System.String _heightAfter_Text
    {
        get {return __heightAfter_Text;}
        set {__heightAfter_Text = value;}
    }

    #endregion

    #region Public construct

    public DivShareTokenPair_DividendsDistributed()
    {
    }

    
    public DivShareTokenPair_DividendsDistributed(System.String AContractAddress,System.String ATransactionHash)
    {
        _ContractAddress = AContractAddress;
        _TransactionHash = ATransactionHash;
    }

    #endregion

    #region Public DataRow2Object

    public static DivShareTokenPair_DividendsDistributed DataRow2Object(System.Data.DataRow dr)
    {
        if (dr == null)
        {
            return null;
        }
        DivShareTokenPair_DividendsDistributed Obj = new DivShareTokenPair_DividendsDistributed();
        Obj.ContractAddress = dr["ContractAddress"] == DBNull.Value ? string.Empty : (System.String)(dr["ContractAddress"]);
        Obj.BlockNumber = dr["BlockNumber"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["BlockNumber"]);
        Obj.TransactionHash = dr["TransactionHash"] == DBNull.Value ? string.Empty : (System.String)(dr["TransactionHash"]);
        Obj.CreateTime = dr["CreateTime"] == DBNull.Value ? System.DateTime.Now : (System.DateTime)(dr["CreateTime"]);
        Obj.AssTokenAddress = dr["AssTokenAddress"] == DBNull.Value ? string.Empty : (System.String)(dr["AssTokenAddress"]);
        Obj._divIndex = dr["_divIndex"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["_divIndex"]);
        Obj._from = dr["_from"] == DBNull.Value ? string.Empty : (System.String)(dr["_from"]);
        Obj._inAssets = dr["_inAssets"] == DBNull.Value ? 0 : (System.Decimal)(dr["_inAssets"]);
        Obj._dividendAssets = dr["_dividendAssets"] == DBNull.Value ? 0 : (System.Decimal)(dr["_dividendAssets"]);
        Obj._currentLiqAmount = dr["_currentLiqAmount"] == DBNull.Value ? 0 : (System.Decimal)(dr["_currentLiqAmount"]);
        Obj._inAssets_Text = dr["_inAssets_Text"] == DBNull.Value ? string.Empty : (System.String)(dr["_inAssets_Text"]);
        Obj._dividendAssets_Text = dr["_dividendAssets_Text"] == DBNull.Value ? string.Empty : (System.String)(dr["_dividendAssets_Text"]);
        Obj._currentLiqAmount_Text = dr["_currentLiqAmount_Text"] == DBNull.Value ? string.Empty : (System.String)(dr["_currentLiqAmount_Text"]);
        Obj._heightBefore_Text = dr["_heightBefore_Text"] == DBNull.Value ? string.Empty : (System.String)(dr["_heightBefore_Text"]);
        Obj._heightAfter_Text = dr["_heightAfter_Text"] == DBNull.Value ? string.Empty : (System.String)(dr["_heightAfter_Text"]);
        return Obj;
    }

    #endregion


        public void ValidateDataLen()
        {
if (this.ContractAddress != null && this.ContractAddress.Length > FL_ContractAddress && FL_ContractAddress > 0) throw new Exception("ContractAddress要求长度小于等于" + FL_ContractAddress.ToString() + "。");
if (this.TransactionHash != null && this.TransactionHash.Length > FL_TransactionHash && FL_TransactionHash > 0) throw new Exception("TransactionHash要求长度小于等于" + FL_TransactionHash.ToString() + "。");
if (this.AssTokenAddress != null && this.AssTokenAddress.Length > FL_AssTokenAddress && FL_AssTokenAddress > 0) throw new Exception("AssTokenAddress要求长度小于等于" + FL_AssTokenAddress.ToString() + "。");
if (this._from != null && this._from.Length > FL__from && FL__from > 0) throw new Exception("_from要求长度小于等于" + FL__from.ToString() + "。");
if (this._inAssets_Text != null && this._inAssets_Text.Length > FL__inAssets_Text && FL__inAssets_Text > 0) throw new Exception("_inAssets_Text要求长度小于等于" + FL__inAssets_Text.ToString() + "。");
if (this._dividendAssets_Text != null && this._dividendAssets_Text.Length > FL__dividendAssets_Text && FL__dividendAssets_Text > 0) throw new Exception("_dividendAssets_Text要求长度小于等于" + FL__dividendAssets_Text.ToString() + "。");
if (this._currentLiqAmount_Text != null && this._currentLiqAmount_Text.Length > FL__currentLiqAmount_Text && FL__currentLiqAmount_Text > 0) throw new Exception("_currentLiqAmount_Text要求长度小于等于" + FL__currentLiqAmount_Text.ToString() + "。");
if (this._heightBefore_Text != null && this._heightBefore_Text.Length > FL__heightBefore_Text && FL__heightBefore_Text > 0) throw new Exception("_heightBefore_Text要求长度小于等于" + FL__heightBefore_Text.ToString() + "。");
if (this._heightAfter_Text != null && this._heightAfter_Text.Length > FL__heightAfter_Text && FL__heightAfter_Text > 0) throw new Exception("_heightAfter_Text要求长度小于等于" + FL__heightAfter_Text.ToString() + "。");
}


        public void ValidateNotEmpty()
        {
if (string.IsNullOrEmpty(this.ContractAddress) || string.IsNullOrEmpty(this.ContractAddress.Trim())) throw new Exception("ContractAddress要求不空。");
if (string.IsNullOrEmpty(this.TransactionHash) || string.IsNullOrEmpty(this.TransactionHash.Trim())) throw new Exception("TransactionHash要求不空。");
if (string.IsNullOrEmpty(this.AssTokenAddress) || string.IsNullOrEmpty(this.AssTokenAddress.Trim())) throw new Exception("AssTokenAddress要求不空。");
if (string.IsNullOrEmpty(this._from) || string.IsNullOrEmpty(this._from.Trim())) throw new Exception("_from要求不空。");
}



        public void ValidateEmptyAndLen()
        {
            this.ValidateDataLen();
            this.ValidateNotEmpty();
        }


public DivShareTokenPair_DividendsDistributed Copy()
{
    DivShareTokenPair_DividendsDistributed obj = new DivShareTokenPair_DividendsDistributed();
    obj.ContractAddress = this.ContractAddress;
    obj.BlockNumber = this.BlockNumber;
    obj.TransactionHash = this.TransactionHash;
    obj.CreateTime = this.CreateTime;
    obj.AssTokenAddress = this.AssTokenAddress;
    obj._divIndex = this._divIndex;
    obj._from = this._from;
    obj._inAssets = this._inAssets;
    obj._dividendAssets = this._dividendAssets;
    obj._currentLiqAmount = this._currentLiqAmount;
    obj._inAssets_Text = this._inAssets_Text;
    obj._dividendAssets_Text = this._dividendAssets_Text;
    obj._currentLiqAmount_Text = this._currentLiqAmount_Text;
    obj._heightBefore_Text = this._heightBefore_Text;
    obj._heightAfter_Text = this._heightAfter_Text;
    return obj;
}



        public static List<DivShareTokenPair_DividendsDistributed> DataTable2List(System.Data.DataTable dt)
        {
            List<DivShareTokenPair_DividendsDistributed> result = new List<DivShareTokenPair_DividendsDistributed>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                result.Add(DivShareTokenPair_DividendsDistributed.DataRow2Object(dr));
            }
            return result;
        }



        private static DivShareTokenPair_DividendsDistributed _NullObject = null;

        public static  DivShareTokenPair_DividendsDistributed NullObject
        {
            get
            {
                if (_NullObject == null)
                {
                    _NullObject = new DivShareTokenPair_DividendsDistributed();
                }
                return _NullObject;
            }
        }


        #region 字段长度

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
        /// 字段 AssTokenAddress 的长度——43
        /// </summary>
        public const int FL_AssTokenAddress = 43;


        /// <summary>
        /// 字段 _divIndex 的长度——8
        /// </summary>
        public const int FL__divIndex = 8;


        /// <summary>
        /// 字段 _from 的长度——43
        /// </summary>
        public const int FL__from = 43;


        /// <summary>
        /// 字段 _inAssets 的长度——17
        /// </summary>
        public const int FL__inAssets = 17;


        /// <summary>
        /// 字段 _dividendAssets 的长度——17
        /// </summary>
        public const int FL__dividendAssets = 17;


        /// <summary>
        /// 字段 _currentLiqAmount 的长度——17
        /// </summary>
        public const int FL__currentLiqAmount = 17;


        /// <summary>
        /// 字段 _inAssets_Text 的长度——80
        /// </summary>
        public const int FL__inAssets_Text = 80;


        /// <summary>
        /// 字段 _dividendAssets_Text 的长度——80
        /// </summary>
        public const int FL__dividendAssets_Text = 80;


        /// <summary>
        /// 字段 _currentLiqAmount_Text 的长度——80
        /// </summary>
        public const int FL__currentLiqAmount_Text = 80;


        /// <summary>
        /// 字段 _heightBefore_Text 的长度——80
        /// </summary>
        public const int FL__heightBefore_Text = 80;


        /// <summary>
        /// 字段 _heightAfter_Text 的长度——80
        /// </summary>
        public const int FL__heightAfter_Text = 80;


        #endregion
}



}