using System.Collections.Generic;
using System.Globalization;
using System;
using ControlzEx.Standard;

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

        Invalid = 30
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
            { ">>=", TokenType.GreaterOrEqual },
            { "<<=", TokenType.LessOrEqual },
            
            { "and", TokenType.LessOrEqual },
            { "not", TokenType.LessOrEqual },
            { "or", TokenType.LessOrEqual },

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

        private static bool IsStringLiteral(string rawToken)
        {
             return rawToken.StartsWith("\"") && rawToken.EndsWith("\"");
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
            else if (IsStringLiteral(rawToken))
                Type = TokenType.StringLiteral;
            else
                Type = TokenType.Invalid;
        }

        public TokenType Type { get; }
        public string RawToken { get; }
        public int StartPos { get; }
        public int EndPos { get => StartPos + RawToken.Length; }
    }
}
