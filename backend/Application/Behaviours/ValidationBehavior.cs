using FluentValidation;
using MediatR;

namespace Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> //Pipeline : Behavior zinciri.
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        //Bu request tipi için kaç tane validator yazıldıysa hepsi buraya gelir.
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var failures = _validators
                .Select(v => v.Validate(context)) // her validator'ı çalıştır, her birinden bir ValidationResult döner
                    .SelectMany(e => e.Errors)// her ValidationResult'ın içindeki hata listelerini tek bir listeye geçirir.
                    .Where(f => f != null) //null hataları haricini göster. Prensip gibi bir şeymiş. Null hatası içermemeliymiş
                    .ToList();
                if (failures.Any()) //herhangi bir hata yakalandıysa exception fırlat ve handlera geçme.
                    throw new ValidationException(failures);

            }
            return await next(); // Sonraki behavior'a ya da yoksa handlera yönlendirir.
        }
    }
}
