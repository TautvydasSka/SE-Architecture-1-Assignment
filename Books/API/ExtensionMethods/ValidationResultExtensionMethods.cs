using Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace API.ExtensionMethods
{
    public static class ValidationResultExtensionMethods
    {
        public static ModelStateDictionary ToModelStateDictionary(this ValidationResult validationResult)
        {
            var modelState = new ModelStateDictionary();

            if (validationResult.EntityValidations.Any())
            {
                foreach (var ev in validationResult.EntityValidations)
                {
                    modelState.AddModelError(string.Empty, ev);
                }
            }
            
            if (validationResult.PropertyValidations.Any())
            {
                foreach(var pv in validationResult.PropertyValidations)
                {
                    foreach (var msg in pv.Value)
                    {
                        modelState.AddModelError(pv.Key, msg);
                    }
                }
            }

            return modelState;
        }
    }
}
