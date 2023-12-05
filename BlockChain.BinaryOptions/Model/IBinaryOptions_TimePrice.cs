
//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/11/14 16:37:28
//
using System;
using System.Data;
using System.Collections.Generic;

namespace BlockChain.BinaryOptions.Model
{
[Serializable]
public class IBinaryOptions_TimePrice
{
    #region 生成该数据实体的SQL语句
    public const string SQL = @"Select Top(1) * From IBinaryOptions_TimePrice";
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

    private System.DateTime _CreateTime = System.DateTime.Now;
    public System.DateTime CreateTime
    {
        get {return _CreateTime;}
        set {_CreateTime = value;}
    }

    private System.Int64 _TimePrice_Time;
    public System.Int64 TimePrice_Time
    {
        get {return _TimePrice_Time;}
        set {_TimePrice_Time = value;}
    }

    private System.String _TimePrice_Price = System.String.Empty;
    public System.String TimePrice_Price
    {
        get {return _TimePrice_Price;}
        set {_TimePrice_Price = value;}
    }

    private System.Int32 _PriceFormart;
    public System.Int32 PriceFormart
    {
        get {return _PriceFormart;}
        set {_PriceFormart = value;}
    }

    private System.Double _BeginPrice;
    public System.Double BeginPrice
    {
        get {return _BeginPrice;}
        set {_BeginPrice = value;}
    }

    private System.DateTime _BeginTime = System.DateTime.Now;
    public System.DateTime BeginTime
    {
        get {return _BeginTime;}
        set {_BeginTime = value;}
    }

    #endregion

    #region Public construct

    public IBinaryOptions_TimePrice()
    {
    }

    
    public IBinaryOptions_TimePrice(System.Int32 AChainId,System.String AContractAddress,System.Int64 ABlockNumber)
    {
        _ChainId = AChainId;
        _ContractAddress = AContractAddress;
        _BlockNumber = ABlockNumber;
    }

    #endregion

    #region Public DataRow2Object

    public static IBinaryOptions_TimePrice DataRow2Object(System.Data.DataRow dr)
    {
        if (dr == null)
        {
            return null;
        }
        IBinaryOptions_TimePrice Obj = new IBinaryOptions_TimePrice();
        Obj.ChainId = dr["ChainId"] == DBNull.Value ? 0 : (System.Int32)(dr["ChainId"]);
        Obj.ContractAddress = dr["ContractAddress"] == DBNull.Value ? string.Empty : (System.String)(dr["ContractAddress"]);
        Obj.BlockNumber = dr["BlockNumber"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["BlockNumber"]);
        Obj.CreateTime = dr["CreateTime"] == DBNull.Value ? System.DateTime.Now : (System.DateTime)(dr["CreateTime"]);
        Obj.TimePrice_Time = dr["TimePrice_Time"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["TimePrice_Time"]);
        Obj.TimePrice_Price = dr["TimePrice_Price"] == DBNull.Value ? string.Empty : (System.String)(dr["TimePrice_Price"]);
        Obj.PriceFormart = dr["PriceFormart"] == DBNull.Value ? 0 : (System.Int32)(dr["PriceFormart"]);
        Obj.BeginPrice = dr["BeginPrice"] == DBNull.Value ? 0 : (System.Double)(dr["BeginPrice"]);
        Obj.BeginTime = dr["BeginTime"] == DBNull.Value ? System.DateTime.Now : (System.DateTime)(dr["BeginTime"]);
        return Obj;
    }

    #endregion


        public void ValidateDataLen()
        {
if (this.ContractAddress != null && this.ContractAddress.Length > FL_ContractAddress && FL_ContractAddress > 0) throw new Exception("ContractAddress要求长度小于等于" + FL_ContractAddress.ToString() + "。");
if (this.TimePrice_Price != null && this.TimePrice_Price.Length > FL_TimePrice_Price && FL_TimePrice_Price > 0) throw new Exception("TimePrice_Price要求长度小于等于" + FL_TimePrice_Price.ToString() + "。");
}


        public void ValidateNotEmpty()
        {
if (string.IsNullOrEmpty(this.ContractAddress) || string.IsNullOrEmpty(this.ContractAddress.Trim())) throw new Exception("ContractAddress要求不空。");
if (string.IsNullOrEmpty(this.TimePrice_Price) || string.IsNullOrEmpty(this.TimePrice_Price.Trim())) throw new Exception("TimePrice_Price要求不空。");
}



        public void ValidateEmptyAndLen()
        {
            this.ValidateDataLen();
            this.ValidateNotEmpty();
        }


public IBinaryOptions_TimePrice Copy()
{
    IBinaryOptions_TimePrice obj = new IBinaryOptions_TimePrice();
    obj.ChainId = this.ChainId;
    obj.ContractAddress = this.ContractAddress;
    obj.BlockNumber = this.BlockNumber;
    obj.CreateTime = this.CreateTime;
    obj.TimePrice_Time = this.TimePrice_Time;
    obj.TimePrice_Price = this.TimePrice_Price;
    obj.PriceFormart = this.PriceFormart;
    obj.BeginPrice = this.BeginPrice;
    obj.BeginTime = this.BeginTime;
    return obj;
}



        public static List<IBinaryOptions_TimePrice> DataTable2List(System.Data.DataTable dt)
        {
            List<IBinaryOptions_TimePrice> result = new List<IBinaryOptions_TimePrice>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                result.Add(IBinaryOptions_TimePrice.DataRow2Object(dr));
            }
            return result;
        }



        private static IBinaryOptions_TimePrice _NullObject = null;

        public static  IBinaryOptions_TimePrice NullObject
        {
            get
            {
                if (_NullObject == null)
                {
                    _NullObject = new IBinaryOptions_TimePrice();
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
        /// 字段 CreateTime 的长度——8
        /// </summary>
        public const int FL_CreateTime = 8;


        /// <summary>
        /// 字段 TimePrice_Time 的长度——8
        /// </summary>
        public const int FL_TimePrice_Time = 8;


        /// <summary>
        /// 字段 TimePrice_Price 的长度——128
        /// </summary>
        public const int FL_TimePrice_Price = 128;


        /// <summary>
        /// 字段 PriceFormart 的长度——4
        /// </summary>
        public const int FL_PriceFormart = 4;


        /// <summary>
        /// 字段 BeginPrice 的长度——8
        /// </summary>
        public const int FL_BeginPrice = 8;


        /// <summary>
        /// 字段 BeginTime 的长度——8
        /// </summary>
        public const int FL_BeginTime = 8;


        #endregion
}



}