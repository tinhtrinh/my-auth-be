using Domain.Users;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Users.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IUserRepository userRepository)
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithErrorCode(UserError.NoName.Code)
            .WithMessage(UserError.NoName.Message)
            .Must((name) =>
            {
                return UserRule.IsUserNameLengthValid(name);
            })
            .WithErrorCode(UserError.ShortName(UserRule.MinNameLength).Code)
            .WithMessage(UserError.ShortName(UserRule.MinNameLength).Message)
            .MustAsync(async (name, _) =>
            {
                return await userRepository.IsNameUnique(name);
            })
            .WithErrorCode(UserError.NotUniqueName.Code)
            .WithMessage(UserError.NotUniqueName.Message);

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithErrorCode(UserError.NoPassword.Code)
            .WithMessage(UserError.NoPassword.Message)
            .Must((passWord) =>
            {
                return UserRule.IsPasswordLengthValid(passWord);
            })
            .WithErrorCode(UserError.ShortPassword(UserRule.MinPasswordLength).Code)
            .WithMessage(UserError.ShortPassword(UserRule.MinPasswordLength).Message);
    }

}

//public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
//{
//    private readonly UserRule? UserRule;

//    public RegisterCommandValidator(IUserRuleRepository userRuleRepository, IUserRepository userRepository)
//    {
//        UserRule = userRuleRepository.GetUserRule().Result;

//        RuleFor(c => c.Name)
//            .NotEmpty()
//            .WithErrorCode(UserRuleError.NoName.Code)
//            .WithMessage(UserRuleError.NoName.Message)
//            .Must((name) =>
//            {
//                if (UserRule is null) return false;
//                return UserRule.IsUserNameLengthValid(name);
//            })
//            .WithErrorCode(UserRuleError.ShortName(UserRule?.MinNameLength).Code)
//            .WithMessage(UserRuleError.ShortName(UserRule?.MinNameLength).Message)
//            .MustAsync(async (name, _) =>
//            {
//                return await userRepository.IsNameUnique(name);
//            })
//            .WithErrorCode(UserError.NotUniqueName.Code)
//            .WithMessage(UserError.NotUniqueName.Message);

//        RuleFor(c => c.Password)
//            .NotEmpty()
//            .WithErrorCode(UserRuleError.NoPassword.Code)
//            .WithMessage(UserRuleError.NoPassword.Message)
//            .Must((passWord) =>
//            {
//                if (UserRule is null) return false;
//                return UserRule.IsPasswordLengthValid(passWord);
//            })
//            .WithErrorCode(UserRuleError.ShortPassword(UserRule?.MinPasswordLength).Code)
//            .WithMessage(UserRuleError.ShortPassword(UserRule?.MinPasswordLength).Message);
//    }

//    protected override bool PreValidate(ValidationContext<RegisterCommand> context, ValidationResult result)
//    {
//        if (context.InstanceToValidate is null)
//        {
//            var failure = new ValidationFailure("", UserRuleError.RegisterCommandNotFound.Message)
//            {
//                ErrorCode = UserRuleError.RegisterCommandNotFound.Code
//            };
//            result.Errors.Add(failure);
//            return false;
//        }

//        if (UserRule is null)
//        {
//            var failure = new ValidationFailure("", UserRuleError.UserRuleNotFound.Message)
//            {
//                ErrorCode = UserRuleError.UserRuleNotFound.Code
//            };
//            result.Errors.Add(failure);
//            return false;
//        }

//        return base.PreValidate(context, result);
//    }
//}