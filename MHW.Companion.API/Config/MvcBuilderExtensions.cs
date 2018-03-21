using FluentValidation.AspNetCore;
using MHW.Companion.ViewModels.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace MHW.Companion.API.Config
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddValidation(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CredentialsViewModelValidator>();
            });

            return builder;
        }
    }
}
