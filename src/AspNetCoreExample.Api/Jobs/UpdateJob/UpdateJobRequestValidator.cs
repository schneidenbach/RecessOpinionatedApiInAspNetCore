using System;
using System.Linq;
using AspNetCoreWorkshop.Api.Jobs.GetJobs;
using FluentValidation;
using FluentValidation.Validators;
using Newtonsoft.Json.Linq;

namespace AspNetCoreWorkshop.Api.Jobs.UpdateJob
{
    public class UpdateJobRequestValidator : AbstractValidator<UpdateJobRequest>
    {
        public UpdateJobRequestValidator()
        {
            RuleFor(r => r.Data)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage("Please specify properties to change.")
                .Custom(AllPropsMustHaveMatchingDataTypes);
        }

        private void AllPropsMustHaveMatchingDataTypes(JObject data, CustomContext context)
        {
            var properties = typeof(Job).GetProperties();
            
            foreach (var prop in data)
            {
                var name = prop.Key;
                var value = prop.Value;

                var property = properties.SingleOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (property == null) {
                    continue;
                }

                try
                {
                    Convert.ChangeType(value, property.PropertyType);
                }
                catch (FormatException)
                {
                    context.AddFailure(property.Name, $"Property '{property.Name}' has an invalid type specified.");
                }
            }
        }
    }
}