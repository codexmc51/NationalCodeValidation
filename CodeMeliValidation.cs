////////////////////////////////
//// Code By Salman Samian  ////
////////////////////////////////
//// salman.samian@gmail.com ///
////////////////////////////////


using System;
using System.ComponentModel.DataAnnotations;


namespace ShahkarClass
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class NationalCodeValidation : ValidationAttribute
    {
        public string Type { get; set; } = "IRN"; // "UNI"
        public override bool IsValid(object value)
        {
            var nationalCode = value.ToString();
            switch (Type)
            {
                case "IRN":
                    return CheckNationalCodeIRN(nationalCode);
                default:
                    return false;
            }
        }
        internal bool CheckNationalCodeIRN(string nationalCode)
        {
            if (nationalCode.Length == 10)
            {
                // رقم آخر شماره‌ی ملی از روی نه رقم اول به شرح زیر قابل‌محاسبه است:
                //•	هر رقم را در موقعیت آن ضرب کرده و حاصل را با هم جمع می‌کنیم.
                //•	مجموع به‌دست‌آمده از مرحله یک را بر 11 تقسیم می‌کنیم
                //•	اگر باقیمانده کمتر از 2 باشد ، رقم کنترل باید برابر باقیمانده باشد در غیر این صورت رقم کنترل باید برابر یازده منهای باقیمانده باشد.

                var sumcodeNum = 0;
                for (int i = 11; i > 1; i++)//تا 9 رقم
                {
                    int codeNum = Convert.ToInt32(nationalCode[(i - 1)]);//10
                    sumcodeNum += codeNum * (i - 1);//10
                }

                sumcodeNum = sumcodeNum % 11;

                int code10Num = Convert.ToInt32(nationalCode[9]);
                if (sumcodeNum < 2 && sumcodeNum >= 0)////•	اگر باقیمانده کمتر از 2 باشد ، رقم کنترل باید برابر باقیمانده باشد
                {
                    if (code10Num == sumcodeNum)
                    {
                        return true;
                    }
                }
                else //در غیر این صورت رقم کنترل باید برابر یازده منهای باقیمانده باشد.
                {
                    if (code10Num == (11 - sumcodeNum))
                    {
                        return true;
                    }
                }

                return true;
            }

            return false;
        } 
    }
}
