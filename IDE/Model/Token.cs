using System;
using System.Collections.Generic;
using System.Globalization;

namespace IDE.Model
{
    internal enum TokenType
    {
        Identifier = 0,
        Integer,
        Double,
        String,
        Complex,

        Whitespace,
        Comma,
        Semicolon,
        Assignment,

        OpenTemplate,
        CloseTemplate,
        OpenArgument,
        CloseArgument,
        OpenScope,
        CloseScope,

        SignedIntegerNumber,
        SignedDoubleNumber,
        StringLiteral,

        Plus,
        Minus,
        Multiply,
        Divide,
        Module,

        Greater,
        Less,
        GreaterOrEqual,
        LessOrEqual,
        Equal,
        And,
        Or,
        Not,
        True,
        False,

        Invalid
    }

    internal class Token
    {
        public static Dictionary<string, TokenType> DefaultTypes { get; } 
            = new Dictionary<string, TokenType>()
        {
            { "string", TokenType.String },
            { "int", TokenType.Integer },
            { "double", TokenType.Double },
            { "std::complex", TokenType.Complex },

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
