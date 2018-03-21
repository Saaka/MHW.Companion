using MHW.Companion.ViewModels.Validations;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace MHW.Companion.ViewModels.Auth
{
    //[Validation(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
