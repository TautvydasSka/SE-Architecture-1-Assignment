namespace Domain
{
    public class OperationResult<T>
    {
        public Status Status { get; set; }
        public T Data { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}
