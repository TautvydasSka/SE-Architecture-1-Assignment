using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class ValidationResult
    {
        public List<string> EntityValidations { get; set; } = new List<string>();

        public Dictionary<string, List<string>> PropertyValidations = new Dictionary<string, List<string>>();

        public bool IsValid => !EntityValidations.Any() && !PropertyValidations.Any();
    }
}
