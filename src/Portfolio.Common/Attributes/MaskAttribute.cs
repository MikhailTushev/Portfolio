using System.ComponentModel.DataAnnotations;

namespace Portfolio.Common.Attributes
{
    public class MaskAttribute : ValidationAttribute
    {
        private const string ErrMsg = "example";

        public MaskAttribute()
        {
            ErrorMessage = ErrMsg;
        }

        public override bool IsValid(object value)
        {
            return value is string;
        }
    }
}