namespace Domain
{
    public class OperationResult<T>
    {
        public Status Status { get; set; } = Status.Success;
        public T Data { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}
