
//
//  本文件由代码工具自动生成。如非必要请不要修改。2023/11/29 14:43:24
//
using System;
using System.Data;
using System.Collections.Generic;

namespace BlockChain.BinaryOptions.Model
{
[Serializable]
public class ChainlinkPriceOracle
{
    #region 生成该数据实体的SQL语句
    public const string SQL = @"Select Top(1) * From ChainlinkPriceOracle";
    #endregion

    #region Public Properties

    private System.String _Aggregator = System.String.Empty;
    public System.String Aggregator
    {
        get {return _Aggregator;}
        set {_Aggregator = value;}
    }

    private System.Decimal _roundId;
    public System.Decimal roundId
    {
        get {return _roundId;}
        set {_roundId = value;}
    }

    private System.Decimal _answer;
    public System.Decimal answer
    {
        get {return _answer;}
        set {_answer = value;}
    }

    private System.Int64 _startedAt;
    public System.Int64 startedAt
    {
        get {return _startedAt;}
        set {_startedAt = value;}
    }

    private System.Int64 _updatedAt;
    public System.Int64 updatedAt
    {
        get {return _updatedAt;}
        set {_updatedAt = value;}
    }

    private System.Decimal _answeredInRound;
    public System.Decimal answeredInRound
    {
        get {return _answeredInRound;}
        set {_answeredInRound = value;}
    }

    private System.DateTime _StartedAt_Time = System.DateTime.Now;
    public System.DateTime StartedAt_Time
    {
        get {return _StartedAt_Time;}
        set {_StartedAt_Time = value;}
    }

    private System.DateTime _UpdatedAt_Time = System.DateTime.Now;
    public System.DateTime UpdatedAt_Time
    {
        get {return _UpdatedAt_Time;}
        set {_UpdatedAt_Time = value;}
    }

    #endregion

    #region Public construct

    public ChainlinkPriceOracle()
    {
    }

    
    public ChainlinkPriceOracle(System.String AAggregator,System.Decimal AroundId)
    {
        _Aggregator = AAggregator;
        _roundId = AroundId;
    }

    #endregion

    #region Public DataRow2Object

    public static ChainlinkPriceOracle DataRow2Object(System.Data.DataRow dr)
    {
        if (dr == null)
        {
            return null;
        }
        ChainlinkPriceOracle Obj = new ChainlinkPriceOracle();
        Obj.Aggregator = dr["Aggregator"] == DBNull.Value ? string.Empty : (System.String)(dr["Aggregator"]);
        Obj.roundId = dr["roundId"] == DBNull.Value ? 0 : (System.Decimal)(dr["roundId"]);
        Obj.answer = dr["answer"] == DBNull.Value ? 0 : (System.Decimal)(dr["answer"]);
        Obj.startedAt = dr["startedAt"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["startedAt"]);
        Obj.updatedAt = dr["updatedAt"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["updatedAt"]);
        Obj.answeredInRound = dr["answeredInRound"] == DBNull.Value ? 0 : (System.Decimal)(dr["answeredInRound"]);
        Obj.StartedAt_Time = dr["StartedAt_Time"] == DBNull.Value ? System.DateTime.Now : (System.DateTime)(dr["StartedAt_Time"]);
        Obj.UpdatedAt_Time = dr["UpdatedAt_Time"] == DBNull.Value ? System.DateTime.Now : (System.DateTime)(dr["UpdatedAt_Time"]);
        return Obj;
    }

    #endregion


        public void ValidateDataLen()
        {
if (this.Aggregator != null && this.Aggregator.Length > FL_Aggregator && FL_Aggregator > 0) throw new Exception("Aggregator要求长度小于等于" + FL_Aggregator.ToString() + "。");
}


        public void ValidateNotEmpty()
        {
if (string.IsNullOrEmpty(this.Aggregator) || string.IsNullOrEmpty(this.Aggregator.Trim())) throw new Exception("Aggregator要求不空。");
}



        public void ValidateEmptyAndLen()
        {
            this.ValidateDataLen();
            this.ValidateNotEmpty();
        }


public ChainlinkPriceOracle Copy()
{
    ChainlinkPriceOracle obj = new ChainlinkPriceOracle();
    obj.Aggregator = this.Aggregator;
    obj.roundId = this.roundId;
    obj.answer = this.answer;
    obj.startedAt = this.startedAt;
    obj.updatedAt = this.updatedAt;
    obj.answeredInRound = this.answeredInRound;
    obj.StartedAt_Time = this.StartedAt_Time;
    obj.UpdatedAt_Time = this.UpdatedAt_Time;
    return obj;
}



        public static List<ChainlinkPriceOracle> DataTable2List(System.Data.DataTable dt)
        {
            List<ChainlinkPriceOracle> result = new List<ChainlinkPriceOracle>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                result.Add(ChainlinkPriceOracle.DataRow2Object(dr));
            }
            return result;
        }



        private static ChainlinkPriceOracle _NullObject = null;

        public static  ChainlinkPriceOracle NullObject
        {
            get
            {
                if (_NullObject == null)
                {
                    _NullObject = new ChainlinkPriceOracle();
                }
                return _NullObject;
            }
        }


        #region 字段长度

        /// <summary>
        /// 字段 Aggregator 的长度——43
        /// </summary>
        public const int FL_Aggregator = 43;


        /// <summary>
        /// 字段 roundId 的长度——17
        /// </summary>
        public const int FL_roundId = 17;


        /// <summary>
        /// 字段 answer 的长度——17
        /// </summary>
        public const int FL_answer = 17;


        /// <summary>
        /// 字段 startedAt 的长度——8
        /// </summary>
        public const int FL_startedAt = 8;


        /// <summary>
        /// 字段 updatedAt 的长度——8
        /// </summary>
        public const int FL_updatedAt = 8;


        /// <summary>
        /// 字段 answeredInRound 的长度——17
        /// </summary>
        public const int FL_answeredInRound = 17;


        /// <summary>
        /// 字段 StartedAt_Time 的长度——8
        /// </summary>
        public const int FL_StartedAt_Time = 8;


        /// <summary>
        /// 字段 UpdatedAt_Time 的长度——8
        /// </summary>
        public const int FL_UpdatedAt_Time = 8;


        #endregion
}



}