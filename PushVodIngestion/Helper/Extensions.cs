using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace PushVodIngestion
{
    static  class Extensions
    {
        public static string Right(this string value, int length)
        {
            return value.Substring(value.Length - length);
        }

        public static bool isNumeric(this string val)
        {
            double result;
            return Double.TryParse(val, out result);
                
        }

        /// </returns>
        public static TimeSpan ShortTimeStringToTimeSpan(this string data)
        {
            var hours = 0;
            var minutes = 0;
            var seconds = 0;

            if (data == null || DBNull.Value.Equals(data))
            {
                return new TimeSpan(hours, minutes, seconds);

            }

            var elements = data.Split(':');
            hours = elements[0].AsInteger();
            if (hours > 23)
            {
                hours = 23;
            }

            minutes = elements[1].AsInteger();
            if (minutes > 59)
            {
                minutes = 59;
            }

            return new TimeSpan(hours, minutes, 0);
        }

        public static TimeSpan StringToTimeSpan(this object data)
        {
            var returnValue = new TimeSpan(0, 0, 0, 0);
            if (data == null || DBNull.Value.Equals(data)) return returnValue;



            var stringData = data.ToString();
            var splittedValue = stringData.Replace(" ", "0").Split(':');
            
            var hours = 0;

            

            try
            {
                hours = splittedValue[0].AsInteger();
            }
            catch
            {
                
                
            }
        
             


            var minutes = 0;

            try
            {
                minutes = splittedValue[1].AsInteger();
            }
            catch
            {

            }
                  


            var seconds = 0;

            try
            {
                seconds = splittedValue[2].AsInteger();

            }
            catch
            {
                
            }


            if (splittedValue.Count() < 3)
            {

                hours = 0;

                try
                {
                    minutes = splittedValue[0].AsInteger();
                }
                catch
                {

                }
                

                try
                {
                    seconds = splittedValue[1].AsInteger();

                }
                catch
                {

                }
                
            }


            if (seconds > 59)
            {
                seconds = 59;
            }

            if (minutes > 59)
            {
                minutes = 59;
            }

            if (hours > 23)
            {
                hours = 23;
            }

            returnValue = new TimeSpan(0, hours, minutes, seconds);

            return returnValue;
        }


        public static int AsInteger(this object data)
        {
            var retunrValue = 0;
            if (data != null && !DBNull.Value.Equals(data) && !string.IsNullOrEmpty(data.ToString()))
            {
                int.TryParse(data.ToString(), out retunrValue);
            }

            return retunrValue;
        }
    }
}
