using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Portfolio.Common;
using Portfolio.Common.Consts;

namespace Portfolio.ApplicationServices.Example
{
    public class SomeHandler : IRequestHandler<GenerateMaskCommand, string>
    {
        private static readonly Regex _regex = new(@"{([^{}]*)}", RegexOptions.Compiled);
        private readonly IUserService _userService;

        public SomeHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Handle(GenerateMaskCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Mask))
            {
                throw new ArgumentException("Введите маску");
            }

            CheckAllConditions(request.Mask);
            var mask = BuildMask(request.Mask);

            return mask;
        }

        private string BuildMask(string mask)
        {
            var date = DateTimeOffset.Now;

            var values = new Dictionary<string, Func<string>>()
            {
                {MaskConst.USER_NAME, () => _userService.CurrentUserDto.Name},
                {MaskConst.USER_ID, () => _userService.CurrentUserDto.Id.ToString()},
                {MaskConst.YEAR, () => date.Year.ToString()},
                {MaskConst.MONTH, () => date.Month.ToString()},
                {MaskConst.DAY, () => date.Day.ToString()},
            };

            var regNumber = new StringBuilder(mask);
            foreach (var (key, value) in values.Where(val => mask.Contains(val.Key)))
            {
                regNumber.Replace(key, value.Invoke());
            }

            return regNumber.ToString();
        }

        private static void CheckAllConditions(string mask)
        {
            var matches = _regex.Matches(mask);

            foreach (Match match in matches)
            {
                if (!MaskConst.Values.ContainsKey(match.Value))
                {
                    throw new ArgumentException($"Условие: {match} - не содержится в перечне всех условий для создания маски.");
                }
            }
        }
    }
}