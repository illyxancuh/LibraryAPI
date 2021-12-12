using System;
using System.Collections.Generic;

namespace LibraryAPI.BusinessLogic.DTOs
{
    public class AuthResultDTO
    {
        public bool IsSuccess { get; }
        public string Token { get; }
        public IEnumerable<string> Errors { get; }

        private AuthResultDTO(bool success, string token, IEnumerable<string> errors)
        {
            IsSuccess = success;
            Token = token;
            Errors = errors;
        }

        public static AuthResultDTO Fail(params string[] errors) =>
            new(false, null, errors);

        public static AuthResultDTO Success(string token) =>
            new(true, token, Array.Empty<string>());
    }
}