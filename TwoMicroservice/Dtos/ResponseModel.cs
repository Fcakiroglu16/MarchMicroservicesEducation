using Microsoft.AspNetCore.Mvc;

namespace BMicroservice.Dtos;

public class ResponseModel<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }

    public ProblemDetails? Error { get; set; }
}