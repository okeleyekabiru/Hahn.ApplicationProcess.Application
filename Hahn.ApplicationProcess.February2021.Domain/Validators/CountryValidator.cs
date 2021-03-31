using FluentValidation.Validators;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Validators
{
    public class CountryValidator : AsyncValidatorBase
    {
        private readonly HttpClient _httpClient;

        public CountryValidator(HttpClient httpClient) 
            
        {
            _httpClient = httpClient;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context,
            CancellationToken cancellation)
        {
            return (await _httpClient.GetAsync(
                    $"https://restcountries.eu/rest/v2/name/{context.PropertyValue}?fullText=true"))
                .IsSuccessStatusCode;
        }
    }
}
