namespace Jake.Assessment.DTO
{
    public class BaseResponseModel<T>
    {
        public string Message { get; set; }= string.Empty;
        public bool Status { get; set; }
        public T? Data { get; set; }
    }
    public class BaseResponseModel
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
