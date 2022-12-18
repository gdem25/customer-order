namespace CustomerOrderBack.Dtos
{
    public class LoginResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = null!;

        public string? Token { get; set; }

    }
}
