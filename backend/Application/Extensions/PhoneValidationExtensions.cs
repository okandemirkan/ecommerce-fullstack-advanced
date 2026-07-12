using FluentValidation;

namespace Application.Extensions
{
    public static class PhoneValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> TurkishPhoneNumber<T>
            (this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                 .Matches(@"^(\+90|0)?5\d{9}$")
                 .WithMessage("Phone number is Invalid.");
            #region Kural: 
            //^ → başlangıç
            //(\+90 | 0) ? → +90 veya 0 ile başlayabilir, ya da hiç olmayabilir(opsiyonel)
            //5 → 5 ile başlamak zorunlu
            //\d{ 9} → 5'ten sonra tam 9 rakam
            //$ → son
            #endregion
        }
    }
}
