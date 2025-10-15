namespace Evote360.Core.Application
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? Error { get; }
        public string FieldName { get; }
        public MessageType MessageType { get; }

        protected Result(
            bool isSuccess,
            string? error,
            string fieldName = "",
            MessageType messageType = MessageType.Field)
        {
            IsSuccess = isSuccess;
            Error = error;
            FieldName = fieldName;
            MessageType = messageType;
        }

        public static Result Ok() => new(true, null, "", MessageType.Field);

        public static Result Fail(
            string error,
            string fieldName = "",
            MessageType messageType = MessageType.Field)
            => new(false, error, fieldName, messageType);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        protected Result(
            bool isSuccess,
            T? value,
            string? error,
            string fieldName = "",
            MessageType messageType = MessageType.Field)
            : base(isSuccess, error, fieldName, messageType)
        {
            Value = value;
        }

        public static Result<T> Ok(T value) => new(true, value, null, "", MessageType.Field);

        public new static Result<T> Fail(
            string error,
            string fieldName = "",
            MessageType messageType = MessageType.Field)
            => new(false, default, error, fieldName, messageType);
    }
}
