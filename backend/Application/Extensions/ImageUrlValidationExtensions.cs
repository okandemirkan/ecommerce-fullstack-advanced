using FluentValidation;

namespace Application.Extensions
{
    public static class ImageUrlValidationExtensions
    {
        public static IRuleBuilderOptions<T, string?> ImageUrlValidation<T>
        (this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .Must(url => url == null || Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Invalid Url");
        }
    }
}
