using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace ApiCliente.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErros(this ModelStateDictionary modelState) {
            var result = new List<string>();
            foreach (var item in modelState.Values)
            {
                result.AddRange(from error in item.Errors select error.ErrorMessage);
            }
            return result;
        }
    }
}
