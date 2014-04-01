using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.Web
{
    /// <summary>
    /// 
    /// </summary>
    public partial class StringHelper
    {
        #region " LetterItem "
        private class LetterItem
        {
            private String fLetter;
            private Int64 fMinValue;
            private Int64 fMaxValue;
            public String Letter { get { return fLetter; } }
            public Int64 MinValue { get { return fMinValue; } }
            public Int64 MaxValue { get { return fMaxValue; } }
            public LetterItem(String fLetter, Int64 fMinValue, Int64 fMaxValue)
            {
                this.fLetter = fLetter;
                this.fMinValue = fMinValue;
                this.fMaxValue = fMaxValue;
            }
        }
        #endregion

        private static List<LetterItem> dictionaryLetter;

        #region " Constructor "
        static StringHelper()
        {
            dictionaryLetter = new List<LetterItem>();
            // 没有 U、V   
            dictionaryLetter.Add(new LetterItem("A", 45217, 45252));
            dictionaryLetter.Add(new LetterItem("B", 45253, 45760));
            dictionaryLetter.Add(new LetterItem("C", 45761, 46317));
            dictionaryLetter.Add(new LetterItem("D", 46318, 46825));
            dictionaryLetter.Add(new LetterItem("E", 46826, 47009));
            dictionaryLetter.Add(new LetterItem("F", 47010, 47296));
            dictionaryLetter.Add(new LetterItem("G", 47297, 47613));
            dictionaryLetter.Add(new LetterItem("H", 47614, 48118));
            dictionaryLetter.Add(new LetterItem("J", 48119, 49061));
            dictionaryLetter.Add(new LetterItem("K", 49062, 49323));
            dictionaryLetter.Add(new LetterItem("L", 49324, 49895));
            dictionaryLetter.Add(new LetterItem("M", 49896, 50370));
            dictionaryLetter.Add(new LetterItem("N", 50371, 50613));
            dictionaryLetter.Add(new LetterItem("O", 50614, 50621));
            dictionaryLetter.Add(new LetterItem("P", 50622, 50905));
            dictionaryLetter.Add(new LetterItem("Q", 50906, 51386));
            dictionaryLetter.Add(new LetterItem("R", 51387, 51445));
            dictionaryLetter.Add(new LetterItem("S", 51446, 52217));
            dictionaryLetter.Add(new LetterItem("T", 52218, 52697));
            dictionaryLetter.Add(new LetterItem("W", 52698, 52979));
            dictionaryLetter.Add(new LetterItem("X", 52980, 53640));
            dictionaryLetter.Add(new LetterItem("Y", 53689, 54480));
            dictionaryLetter.Add(new LetterItem("Z", 54481, 55289));
        }
        #endregion

        /// <summary>  
        /// 获取一段中文中每个中文拼音的第一个字母  
        /// </summary>  
        /// <param name="fInputChinese">需要获取字母的中文</param>  
        /// <returns>中文拼音的第一个字母</returns>  
        public static string GetFirstLetterOfChinese(string fInputChinese)
        {
            return GetFirstLetterOfChinese(fInputChinese, false);
        }

        /// <summary>  
        /// 获取一段中文中每个中文拼音的第一个字母  
        /// </summary>  
        /// <param name="fInputChinese">需要获取字母的中文</param>  
        /// <param name="fReutrnEmptyWhenFailure">当输入不是中文时是否返回空值。True:返回空值；False：返回传入参数的大写</param>  
        /// <returns>中文拼音的第一个字母</returns>  
        public static string GetFirstLetterOfChinese(string fInputChinese, Boolean fReutrnEmptyWhenFailure)
        {
            string letters = "";

            foreach (char c in fInputChinese.ToCharArray())
                letters += GetFirstLetterOfPinyin(c.ToString(), fReutrnEmptyWhenFailure);
            return letters;
        }

        /// <summary>  
        /// 获取一个中文拼音的第一个字母。  
        /// </summary>  
        /// <param name="fInputSingleChinese">需要获取字母的一个中文</param>  
        /// <param name="fReutrnEmptyWhenFailure">当输入不是中文时是否返回空值。True:返回空值；False：返回传入参数的大写</param>  
        /// <returns>中文拼音的第一个字母</returns>  
        private static string GetFirstLetterOfPinyin(String fInputSingleChinese, Boolean fReutrnEmptyWhenFailure)
        {
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(fInputSingleChinese);
            //如果是字母，则直接返回   
            if (byteArray.Length == 1)
            {
                return fReutrnEmptyWhenFailure
                    ? fInputSingleChinese.ToUpper()
                    : String.Empty;
            }
            // 获取范围  
            short minValue = (short)(byteArray[0]);
            short maxValue = (short)(byteArray[1]);
            Int64 value = minValue * 256 + maxValue;
            foreach (LetterItem letterItem in dictionaryLetter)
            {
                if (value >= letterItem.MinValue &&
                    value <= letterItem.MaxValue)
                    return letterItem.Letter;
            }
            return "?"; // 未知  
        }
    }
}
