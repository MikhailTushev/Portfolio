using System.Collections.Generic;

namespace Portfolio.Common.Consts
{
    public class MaskConst
    {
        public const string YEAR = "{Year}";
        public const string MONTH = "{Month}";
        public const string DAY = "{Day}";
        public const string USER_NAME = "{UserName}";
        public const string USER_ID = "{UserId}";

        public static Dictionary<string, string> Values { get; } = new()
        {
            { YEAR, "Год" },
            { MONTH, "Месяц" },
            { DAY, "День" },
            { USER_NAME, "Имя пользователя" },
            { USER_ID, "Идентификатор пользователя" },
        };
    }
}