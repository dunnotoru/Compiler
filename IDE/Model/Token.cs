using System;
using System.Collections.Generic;
using System.Globalization;

namespace IDE.Model
{
    internal enum TokenType
    {
        Identifier = 0,
        Integer = 1,
        Double = 2,
        ComplexNumber = 3,

        Whitespace = 4,
        Comma = 5,
        Semicolon = 6,
        Assignment = 7,

        OpenTemplate = 8,
        CloseTemplate = 9,
        OpenArgument = 10,
        CloseArgument = 11,
        OpenScope = 12,
        CloseScope = 13,

        SignedIntegerNumber = 14,
        SignedDoubleNumber = 15,
        StringLiteral = 16,

        Plus = 17,
        Minus = 18,
        Multiply = 19,
        Divide = 20,
        Module = 21,

        Greater = 22,
        Less = 23,
        GreaterOrEqual = 24,
        LessOrEqual = 25,
        Equal = 26,
        And = 27,
        Or = 28,
        Not = 29,
        True = 30,
        False = 31,

        Invalid = 32
    }

    internal class Token
    {
        public static Dictionary<string, TokenType> DefaultTypes { get; } 
            = new Dictionary<string, TokenType>()
        {
            { "int", TokenType.Integer },
            { "double", TokenType.Double },
            { "std::complex", TokenType.ComplexNumber },

            { " ", TokenType.Whitespace },
            { ",", TokenType.Comma },
            { ";", TokenType.Semicolon },
            { "=", TokenType.Assignment },

            { "<", TokenType.OpenTemplate },
            { ">", TokenType.CloseTemplate },
            { "(", TokenType.OpenArgument },
            { ")", TokenType.CloseArgument },
            { "{", TokenType.OpenScope},
            { "}", TokenType.CloseScope },

            { "+", TokenType.Plus },
            { "-", TokenType.Minus },
            { "*", TokenType.Multiply },
            { "/", TokenType.Divide },
            { "%", TokenType.Module },

            { ">>", TokenType.Greater},
            { "<<", TokenType.Less },
            { "==", TokenType.Equal },
            { ">=", TokenType.GreaterOrEqual },
            { "<=", TokenType.LessOrEqual },
            
            { "and", TokenType.And },
            { "not", TokenType.Not },
            { "or", TokenType.Or },
            { "TRUE", TokenType.True },
            { "FALSE", TokenType.False },
        };

        public TokenType Type { get; }
        public string RawToken { get; }
        public int StartPos { get; }
        public int EndPos { get => StartPos + RawToken.Length; }

        public Token(string rawToken, int startPos)
        {
            if (rawToken.Length == 0)
                throw new ArgumentException("raw token is empty");

            RawToken = rawToken;
            StartPos = startPos;
            Type = GetTokenType(rawToken);
        }

        public static bool DefaultTokenExists(string rawToken)
            => DefaultTypes.ContainsKey(rawToken);
        private static bool IsIdentifier(string rawToken)
            => rawToken.Length != 0 && char.IsLetter(rawToken[0]);
        private static bool IsSignedInteger(string rawToken)
            => int.TryParse(rawToken, out int _);
        private static bool IsSignedDouble(string rawToken)
            => double.TryParse(rawToken, NumberFormatInfo.InvariantInfo, out double _);
        private static bool IsStringLiteral(string rawToken)
            => rawToken.StartsWith("\"") && rawToken.EndsWith("\"");

        public static TokenType GetTokenType(string rawToken)
        {
            if (DefaultTokenExists(rawToken))
                return DefaultTypes[rawToken];
            if (IsIdentifier(rawToken) && char.IsLetter(rawToken[0]))
                return TokenType.Identifier;
            if (IsSignedInteger(rawToken))
                return TokenType.SignedIntegerNumber;
            if (IsSignedDouble(rawToken))
                return TokenType.SignedDoubleNumber;
            if (IsStringLiteral(rawToken))
                return TokenType.StringLiteral;

            return TokenType.Invalid;
        }
    }
}
