
//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/4/6 13:27:02
//
using System;
using System.Data;
using System.Collections.Generic;

namespace BlockChain.Share.Model
{
[Serializable]
public class ContEventBlockNum
{
    #region 生成该数据实体的SQL语句
    public const string SQL = @"Select Top(1) * From ContEventBlockNum";
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

    private System.String _EventName = System.String.Empty;
    public System.String EventName
    {
        get {return _EventName;}
        set {_EventName = value;}
    }

    private System.Int64 _LastBlockNumber;
    public System.Int64 LastBlockNumber
    {
        get {return _LastBlockNumber;}
        set {_LastBlockNumber = value;}
    }

    #endregion

    #region Public construct

    public ContEventBlockNum()
    {
    }

    
    public ContEventBlockNum(System.Int32 AChainId,System.String AContractAddress,System.String AEventName)
    {
        _ChainId = AChainId;
        _ContractAddress = AContractAddress;
        _EventName = AEventName;
    }

    #endregion

    #region Public DataRow2Object

    public static ContEventBlockNum DataRow2Object(System.Data.DataRow dr)
    {
        if (dr == null)
        {
            return null;
        }
        ContEventBlockNum Obj = new ContEventBlockNum();
        Obj.ChainId = dr["ChainId"] == DBNull.Value ? 0 : (System.Int32)(dr["ChainId"]);
        Obj.ContractAddress = dr["ContractAddress"] == DBNull.Value ? string.Empty : (System.String)(dr["ContractAddress"]);
        Obj.EventName = dr["EventName"] == DBNull.Value ? string.Empty : (System.String)(dr["EventName"]);
        Obj.LastBlockNumber = dr["LastBlockNumber"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["LastBlockNumber"]);
        return Obj;
    }

    #endregion


        public void ValidateDataLen()
        {
if (this.ContractAddress != null && this.ContractAddress.Length > FL_ContractAddress && FL_ContractAddress > 0) throw new Exception("ContractAddress要求长度小于等于" + FL_ContractAddress.ToString() + "。");
if (this.EventName != null && this.EventName.Length > FL_EventName && FL_EventName > 0) throw new Exception("EventName要求长度小于等于" + FL_EventName.ToString() + "。");
}


        public void ValidateNotEmpty()
        {
if (string.IsNullOrEmpty(this.ContractAddress) || string.IsNullOrEmpty(this.ContractAddress.Trim())) throw new Exception("ContractAddress要求不空。");
if (string.IsNullOrEmpty(this.EventName) || string.IsNullOrEmpty(this.EventName.Trim())) throw new Exception("EventName要求不空。");
}



        public void ValidateEmptyAndLen()
        {
            this.ValidateDataLen();
            this.ValidateNotEmpty();
        }


public ContEventBlockNum Copy()
{
    ContEventBlockNum obj = new ContEventBlockNum();
    obj.ChainId = this.ChainId;
    obj.ContractAddress = this.ContractAddress;
    obj.EventName = this.EventName;
    obj.LastBlockNumber = this.LastBlockNumber;
    return obj;
}



        public static List<ContEventBlockNum> DataTable2List(System.Data.DataTable dt)
        {
            List<ContEventBlockNum> result = new List<ContEventBlockNum>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                result.Add(ContEventBlockNum.DataRow2Object(dr));
            }
            return result;
        }



        private static ContEventBlockNum _NullObject = null;

        public static  ContEventBlockNum NullObject
        {
            get
            {
                if (_NullObject == null)
                {
                    _NullObject = new ContEventBlockNum();
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
        /// 字段 EventName 的长度——256
        /// </summary>
        public const int FL_EventName = 256;


        /// <summary>
        /// 字段 LastBlockNumber 的长度——8
        /// </summary>
        public const int FL_LastBlockNumber = 8;


        #endregion
}



}