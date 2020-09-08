using FluentValidation;
       
       
       namespace Solen.Core.Application.Auth.Queries
       {
           public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
           {
               public LoginUserQueryValidator()
               {
                   RuleFor(x => x.Email).NotEmpty().EmailAddress();
                   RuleFor(x => x.Password).NotEmpty();
               }
           }
       }