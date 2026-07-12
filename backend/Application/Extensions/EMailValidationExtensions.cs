using FluentValidation;

namespace Application.Extensions
{
    public static class EMailValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> EMailValidation<T>
           (this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                 .Matches(@"^[^@\s]+@[a-zA-Z0-9]([a-zA-Z0-9-]*[a-zA-Z0-9])?(\.[a-zA-Z0-9]([a-zA-Z0-9-]*[a-zA-Z0-9])?)*\.[a-zA-Z]{2,}$")
                 .WithMessage("Mail is Invalid.");
            #region Kurallar
            /*
             → nokta, tire ile başlayamaz
            ([a-zA-Z0-9-]*[a-zA-Z0-9])?        → ortada tire olabilir ama sonda olamaz
            (\.[a-zA-Z0-9]([a-zA-Z0-9-]*)*)?   → nokta sonrası harf/rakam zorunlu
                                                — ardışık nokta imkansız hale gelir
            \.[a-zA-Z]{2,}                      → .com .net .io gibi uzantı zorunlu
            */
            #endregion

        }
    }
}
