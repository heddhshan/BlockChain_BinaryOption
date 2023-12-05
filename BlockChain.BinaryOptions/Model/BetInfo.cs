using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.BinaryOptions.Model
{

    #region 代码自动生成后有修改！

    [Serializable]
    public class BetInfo
    {
        #region 生成该数据实体的SQL语句
        public const string SQL = @" SELECT TOP (300) P.PriceFormart, P.TransactionHash AS PlayTx, P._roudId, P.Amount_Num, P.Winnings_Num, O.RealWinnings_Num, O.TransactionHash AS OpenTx, TP.BeginTime, TP.BeginPrice, DATEADD(s, 1, TP.BeginTime) AS EndTime, P._selectedUpToken, O.EndPrice, T.Symbol, 0.0 AS BeginPrice0, 0.0 AS BeginPrice1, 0.0 AS EndPrice0, 0.0 AS EndPrice1, P.Winnings_Num / P.Amount_Num AS odds FROM IBinaryOptions_OnPlay AS P LEFT OUTER JOIN Token AS T ON P.ChainId = T.ChainId AND P._selectedUpToken = T.Address LEFT OUTER JOIN IBinaryOptions_OnOpen AS O ON P.ChainId = O.ChainId AND P.ContractAddress = O.ContractAddress AND P._roudId = O._roudId LEFT OUTER JOIN IBinaryOptions_TimePrice AS TP ON P.ChainId = TP.ChainId AND P.ContractAddress = TP.ContractAddress AND P.BlockNumber = TP.BlockNumber ";
        #endregion

        #region Public Properties

        private System.Int32 _PriceFormart;
        public System.Int32 PriceFormart
        {
            get { return _PriceFormart; }
            set { _PriceFormart = value; }
        }

        private System.String _PlayTx = System.String.Empty;
        public System.String PlayTx
        {
            get { return _PlayTx; }
            set { _PlayTx = value; }
        }

        private System.Int64 __roudId;
        public System.Int64 _roudId
        {
            get { return __roudId; }
            set { __roudId = value; }
        }

        private System.Double _Amount_Num;
        public System.Double Amount_Num
        {
            get { return _Amount_Num; }
            set { _Amount_Num = value; }
        }

        private System.Double _Winnings_Num;
        public System.Double Winnings_Num
        {
            get { return _Winnings_Num; }
            set { _Winnings_Num = value; }
        }

        private System.Double _RealWinnings_Num;
        public System.Double RealWinnings_Num
        {
            get { return _RealWinnings_Num; }
            set { _RealWinnings_Num = value; }
        }

        private System.String _OpenTx = System.String.Empty;
        public System.String OpenTx
        {
            get { return _OpenTx; }
            set { _OpenTx = value; }
        }

        private System.DateTime _BeginTime = System.DateTime.Now;
        public System.DateTime BeginTime
        {
            get { return _BeginTime; }
            set { _BeginTime = value; }
        }

        private System.Double _BeginPrice;
        public System.Double BeginPrice
        {
            get { return _BeginPrice; }
            set { _BeginPrice = value; }
        }

        private System.DateTime _EndTime = System.DateTime.Now;
        public System.DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        private System.String __selectedUpToken = System.String.Empty;
        public System.String _selectedUpToken
        {
            get { return __selectedUpToken; }
            set { __selectedUpToken = value; }
        }

        private System.Double _EndPrice;
        public System.Double EndPrice
        {
            get { return _EndPrice; }
            set { _EndPrice = value; }
        }

        private System.String _Symbol = System.String.Empty;
        public System.String Symbol
        {
            get { return _Symbol; }
            set { _Symbol = value; }
        }

        private System.Decimal _BeginPrice0;
        public System.Decimal BeginPrice0
        {
            get { return _BeginPrice0; }
            set { _BeginPrice0 = value; }
        }

        private System.Decimal _BeginPrice1;
        public System.Decimal BeginPrice1
        {
            get { return _BeginPrice1; }
            set { _BeginPrice1 = value; }
        }

        private System.Decimal _EndPrice0;
        public System.Decimal EndPrice0
        {
            get { return _EndPrice0; }
            set { _EndPrice0 = value; }
        }

        private System.Decimal _EndPrice1;
        public System.Decimal EndPrice1
        {
            get { return _EndPrice1; }
            set { _EndPrice1 = value; }
        }

        private System.Double _odds;
        public System.Double odds
        {
            get { return _odds; }
            set { _odds = value; }
        }



        public string Token0
        {
            get;
            set;
        }

        public System.Windows.Media.Brush TokenColor
        {
            get
            {
                try
                {
                    if (_selectedUpToken == Token0 || _selectedUpToken.ToUpper() == Token0.ToUpper())
                    {
                        return System.Windows.Media.Brushes.Green;
                    }
                    else
                    {
                        return System.Windows.Media.Brushes.Red;
                    }
                }
                catch
                {
                    return System.Windows.Media.Brushes.Black;
                }
            }
        }

        #endregion

        #region Public construct


        public BetInfo()
        {
        }

        public BetInfo(System.DateTime AEndTime, System.Decimal ABeginPrice0, System.Decimal ABeginPrice1, System.Decimal AEndPrice0, System.Decimal AEndPrice1, System.Double Aodds)
        {
            _EndTime = AEndTime;
            _BeginPrice0 = ABeginPrice0;
            _BeginPrice1 = ABeginPrice1;
            _EndPrice0 = AEndPrice0;
            _EndPrice1 = AEndPrice1;
            _odds = Aodds;
        }

        #endregion

        #region Public DataRow2Object

        public static BetInfo DataRow2Object(System.Data.DataRow dr)
        {
            if (dr == null)
            {
                return null;
            }
            BetInfo Obj = new BetInfo();
            Obj.PriceFormart = dr["PriceFormart"] == DBNull.Value ? 0 : (System.Int32)(dr["PriceFormart"]);
            Obj.PlayTx = dr["PlayTx"] == DBNull.Value ? string.Empty : (System.String)(dr["PlayTx"]);
            Obj._roudId = dr["_roudId"] == DBNull.Value ? Convert.ToInt64(0) : (System.Int64)(dr["_roudId"]);
            Obj.Amount_Num = dr["Amount_Num"] == DBNull.Value ? 0 : (System.Double)(dr["Amount_Num"]);
            Obj.Winnings_Num = dr["Winnings_Num"] == DBNull.Value ? 0 : (System.Double)(dr["Winnings_Num"]);
            Obj.RealWinnings_Num = dr["RealWinnings_Num"] == DBNull.Value ? 0 : (System.Double)(dr["RealWinnings_Num"]);
            Obj.OpenTx = dr["OpenTx"] == DBNull.Value ? string.Empty : (System.String)(dr["OpenTx"]);
            Obj.BeginTime = dr["BeginTime"] == DBNull.Value ? System.DateTime.Now : (System.DateTime)(dr["BeginTime"]);
            Obj.BeginPrice = dr["BeginPrice"] == DBNull.Value ? 0 : (System.Double)(dr["BeginPrice"]);
            Obj.EndTime = dr["EndTime"] == DBNull.Value ? System.DateTime.Now : (System.DateTime)(dr["EndTime"]);
            Obj._selectedUpToken = dr["_selectedUpToken"] == DBNull.Value ? string.Empty : (System.String)(dr["_selectedUpToken"]);
            Obj.EndPrice = dr["EndPrice"] == DBNull.Value ? 0 : (System.Double)(dr["EndPrice"]);
            Obj.Symbol = dr["Symbol"] == DBNull.Value ? string.Empty : (System.String)(dr["Symbol"]);
            Obj.BeginPrice0 = dr["BeginPrice0"] == DBNull.Value ? 0 : (System.Decimal)(dr["BeginPrice0"]);
            Obj.BeginPrice1 = dr["BeginPrice1"] == DBNull.Value ? 0 : (System.Decimal)(dr["BeginPrice1"]);
            Obj.EndPrice0 = dr["EndPrice0"] == DBNull.Value ? 0 : (System.Decimal)(dr["EndPrice0"]);
            Obj.EndPrice1 = dr["EndPrice1"] == DBNull.Value ? 0 : (System.Decimal)(dr["EndPrice1"]);
            Obj.odds = dr["odds"] == DBNull.Value ? 0 : (System.Double)(dr["odds"]);
            return Obj;
        }

        #endregion


        public BetInfo Copy()
        {
            BetInfo obj = new BetInfo();
            obj.PriceFormart = this.PriceFormart;
            obj.PlayTx = this.PlayTx;
            obj._roudId = this._roudId;
            obj.Amount_Num = this.Amount_Num;
            obj.Winnings_Num = this.Winnings_Num;
            obj.RealWinnings_Num = this.RealWinnings_Num;
            obj.OpenTx = this.OpenTx;
            obj.BeginTime = this.BeginTime;
            obj.BeginPrice = this.BeginPrice;
            obj.EndTime = this.EndTime;
            obj._selectedUpToken = this._selectedUpToken;
            obj.EndPrice = this.EndPrice;
            obj.Symbol = this.Symbol;
            obj.BeginPrice0 = this.BeginPrice0;
            obj.BeginPrice1 = this.BeginPrice1;
            obj.EndPrice0 = this.EndPrice0;
            obj.EndPrice1 = this.EndPrice1;
            obj.odds = this.odds;
            return obj;
        }

        public static List<BetInfo> DataTable2List(System.Data.DataTable dt)
        {
            List<BetInfo> result = new List<BetInfo>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                result.Add(BetInfo.DataRow2Object(dr));
            }
            return result;
        }

    }

    #endregion


}
