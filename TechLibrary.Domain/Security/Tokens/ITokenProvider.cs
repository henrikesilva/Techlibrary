﻿namespace TechLibrary.Domain.Security.Tokens;
public interface ITokenProvider
{
    string TokenOnRequest();
}
