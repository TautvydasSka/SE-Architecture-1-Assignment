using System.Collections.Generic;

namespace Domain
{
    public class Validation
    {
        public List<string> EntityValidations { get; set; } = new List<string>();

        //public IEnumerable<KeyValuePair<string, ModelStateEntry>> FieldsValidations = new List<ValidationResult>();
    }
}
