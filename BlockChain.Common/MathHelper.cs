using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Common
{
    public static class MathHelper
    {


        /// <summary>
        /// 得到小数的位数长度，0表示个位数，正数表示大于10, 负数表示小于1。主要用于格式化数据。
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int getLogLength(double num) { 
            if (num == 0) return 0;
            if (num < 0) num = -num;
            var n = Math.Log10(num);
            return (int)n;
        }


        /// <summary>
        /// 取固定有效数字长度
        /// </summary>
        /// <param name="num">原始数据</param>
        /// <param name="len">有效数字长度</param>
        /// <returns></returns>
        public static decimal getFixLenNum(double num, int len)
        {
            if (num == double.NaN  || double.IsInfinity(num) ) {
                //throw new Exception("NaN");
                return 0;    
            };
            if (num == 0) return 0;
            var n = Math.Abs(num);

            var L0 =(int) Math.Log10(n);
            var Y = Math.Pow(10, (len - L0));   //100,10000            
            var x = (decimal)(long)(n * Y) ;
            var result = x / (decimal)Y;

            if (num != n)
            {
                result = - result;
            }
            return result;
        }



    }
}
