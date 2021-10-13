using Application.Profiles;

namespace Application.Core
{
  public class Result<T>
  {
    public bool isSuccess { get; set; }
    public T Value { get; set; }
    public ApiErrorResponse Errors { get; set; }
    public static Result<T> Success(T value) => new Result<T> { isSuccess = true, Value = value };
    public static Result<T> Failure(ApiErrorResponse errors) => new Result<T> { isSuccess = false, Errors = errors };
  }
}