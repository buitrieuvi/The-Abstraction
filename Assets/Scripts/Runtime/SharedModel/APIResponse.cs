
namespace Abstraction.SharedModel 
{
    public class APIResponse<T>
    {
        public string Code { get; set; }       // Mã lỗi hoặc thành công (200, 401, 404, …)
        public string Message { get; set; }    // Thông điệp đi kèm
        public T Data { get; set; }            // Dữ liệu trả về

        public APIResponse() { }

        public APIResponse(string code, string message, T data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}