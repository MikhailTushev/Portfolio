using MediatR;

namespace Portfolio.ApplicationServices.Example
{
    public class GenerateMaskCommand : IRequest<string>
    {
        public string Mask { get; set; }
    }
}