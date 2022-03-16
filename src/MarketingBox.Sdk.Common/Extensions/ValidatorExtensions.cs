using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MarketingBox.Sdk.Common.Exceptions;
using MarketingBox.Sdk.Common.Models;
using ValidationException = FluentValidation.ValidationException;

namespace MarketingBox.Sdk.Common.Extensions;

public static class ValidatorExtensions
{
    
    public static async Task ValidateAndThrowWithRuleSetsAsync<T>(
        this IValidator<T> validator,
        T request,
        params string[] ruleSets)
    {
        var result = await validator.ValidateAsync(
            request,
            options => options.IncludeRuleSets(ruleSets));
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }

    /// <summary>
    /// Validator validates 'ValidatableEntity' object that contains properties decorated with ValidationAttribute attributes.
    /// Validator looks for all properties of type 'ValidatableEntity' and validates them recursively.
    /// Validator also validates all collections of type 'ValidatableEntity'.
    /// </summary>
    /// <param name="entity"></param>
    /// <exception cref="BadRequestException"></exception>
    public static void ValidateEntity(this ValidatableEntity entity)
    {
        var validationResult = new List<ValidationResult>();
        var validationContext = new ValidationContext(entity);
        if (!Validator.TryValidateObject(entity, validationContext, validationResult, true))
        {
            throw new BadRequestException(new Error
            {
                ErrorMessage = BadRequestException.DefaultErrorMessage,
                ValidationErrors = validationResult.Select(
                    x => new ValidationError
                    {
                        ErrorMessage = x.ErrorMessage,
                        ParameterName = x.MemberNames.FirstOrDefault()
                    }).ToList()
            });
        }

        var properties = entity.GetType()
            .GetProperties()
            .Select(x => x.GetValue(entity))
            .ToList();
        var validatableProperties = properties.OfType<ValidatableEntity>();

        foreach (var validatableEntity in validatableProperties)
        {
            validatableEntity.ValidateEntity();
        }

        var enumerableProperties = properties
            .Where(x => typeof(IEnumerable).IsAssignableFrom(x?.GetType()))
            .Select(x => ((IEnumerable) x)!.OfType<ValidatableEntity>())
            .Where(x => x.Any());

        foreach (var collection in enumerableProperties)
        {
            foreach (var validatableEntity in collection)
            {
                validatableEntity.ValidateEntity();
            }
        }
    }
}