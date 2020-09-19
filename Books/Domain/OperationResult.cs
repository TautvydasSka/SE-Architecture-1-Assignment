namespace Domain
{
    public class OperationResult<T>
    {
        public Status Status { get; set; }
        public T Data { get; set; }
        public Validation Validation { get; set; }
    }
}
