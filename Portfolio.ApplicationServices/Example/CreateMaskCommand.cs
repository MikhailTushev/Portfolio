using MediatR;
using Portfolio.Common.Attributes;

namespace Portfolio.ApplicationServices.Example
{
    public class GenerateMaskCommand : IRequest<string>
    {
        [Mask]
        public string Mask { get; set; }
    }
}