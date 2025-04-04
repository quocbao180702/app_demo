﻿namespace EmployeeManagement.Api
{
    public class BaseResponse
    {
        public bool? Success { get; set; } = false;
        public string? Message { get; set; } = null;
        public object? Data { get; set; } = null;
    }
}
