﻿namespace blog.Models
{
    public class Response<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? result { get;set; }
    }
}
