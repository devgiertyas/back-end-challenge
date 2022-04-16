namespace TodoAPI.DataObjects
{
    public class ErrorDTO
    {
        public ErrorDTO (bool success, string error)
        {
            Success = success;  
            Error = error;  
        }

        public bool Success { get; set; }
        public string Error { get; set; }   
    }
}
