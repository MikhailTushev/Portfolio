using System.ComponentModel.DataAnnotations;

namespace Portfolio.Common.Attributes
{
    public class MaskAttribute : ValidationAttribute
    {
        private const string ErrMsg = "Password length must be equal or more than 6";

        public MaskAttribute()
        {
            ErrorMessage = ErrMsg;
        }

        public override bool IsValid(object value)
        {
            if(value is not string)
            {
                return false;
            }

            
            
            return false;
        }
    }
}