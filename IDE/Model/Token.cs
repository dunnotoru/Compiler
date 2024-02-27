using System.Collections.Generic;
using System.Globalization;
using System;

namespace IDE.Model
{
    internal enum TokenType
    {
        ComplexNumber = 0,
        Identifier = 1,
        Integer = 2,
        Double = 3,
        Whitespace = 4,
        OpenTemplate = 5,
        CloseTemplate = 6,
        OpenArgument = 7,
        CloseArgument = 8,
        SignedIntegerNumber = 9,
        SignedDoubleNumber = 10,
        Comma = 11,
        Semicolon = 12,
        Invalid = 13,
    }

    internal class Token
    {
        public static Dictionary<string, TokenType> DefaultTypes { get; } 
            = new Dictionary<string, TokenType>()
        {
            { "std::complex", TokenType.ComplexNumber },
            { " ", TokenType.Whitespace },
            { "int", TokenType.Integer },
            { "double", TokenType.Double },
            { "<", TokenType.OpenTemplate },
            { ">", TokenType.CloseTemplate },
            { "(", TokenType.OpenArgument },
            { ")", TokenType.CloseArgument },
            { ",", TokenType.Comma },
            { ";", TokenType.Semicolon },
        };

        private static bool IsIdentifier(string rawToken)
        {
            return rawToken.Length != 0 && char.IsLetter(rawToken[0]);
        }

        private static bool IsSignedInteger(string rawToken)
        {
            return int.TryParse(rawToken, out int _);
        }

        private static bool IsSignedDouble(string rawToken)
        {
            return double.TryParse(rawToken, NumberFormatInfo.InvariantInfo, out double _);
        }

        public Token(string rawToken, int startPos)
        {
            RawToken = rawToken;
            StartPos = startPos;

            if (rawToken.Length == 0)
                throw new ArgumentException("raw token is empty");

            if (DefaultTypes.ContainsKey(rawToken))
                Type = DefaultTypes[rawToken];
            else if (IsIdentifier(rawToken) && char.IsLetter(rawToken[0]))
                Type = TokenType.Identifier;
            else if (IsSignedInteger(rawToken))
                Type = TokenType.SignedIntegerNumber;
            else if (IsSignedDouble(rawToken))
                Type = TokenType.SignedDoubleNumber;
            else
                Type = TokenType.Invalid;
        }

        public TokenType Type { get; }
        public string RawToken { get; }
        public int StartPos { get; }
        public int EndPos { get => StartPos + RawToken.Length; }
    }
}
