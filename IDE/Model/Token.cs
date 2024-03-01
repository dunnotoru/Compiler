﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IDE.Model
{
    internal enum TokenType
    {
        Identifier = 0,
        Integer,
        Double,
        String,

        Newline,
        Whitespace,
        Comma,
        Semicolon,
        Assignment,

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

        If,
        ElseIf,
        Else,
        For,
        While,
        Do,

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
            { "std::complex", TokenType.Identifier },

            { "\n", TokenType.Newline },
            { " ", TokenType.Whitespace },
            { ",", TokenType.Comma },
            { ";", TokenType.Semicolon },
            { "=", TokenType.Assignment },

            { "(", TokenType.OpenArgument },
            { ")", TokenType.CloseArgument },
            { "{", TokenType.OpenScope},
            { "}", TokenType.CloseScope },

            { "+", TokenType.Plus },
            { "-", TokenType.Minus },
            { "*", TokenType.Multiply },
            { "/", TokenType.Divide },
            { "%", TokenType.Module },

            { ">", TokenType.Greater},
            { "<", TokenType.Less },
            { "==", TokenType.Equal },
            { ">=", TokenType.GreaterOrEqual },
            { "<=", TokenType.LessOrEqual },
            
            { "and", TokenType.And },
            { "not", TokenType.Not },
            { "or", TokenType.Or },
            { "true", TokenType.True },
            { "false", TokenType.False },

            { "if", TokenType.If },
            { "else if", TokenType.ElseIf},
            { "else", TokenType.Else },
            { "for", TokenType.For },
            { "while", TokenType.While },
            { "do", TokenType.Do },
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
            => rawToken.Length != 0 && (char.IsLetter(rawToken.First()) || rawToken.First() == '_');
        private static bool IsSignedInteger(string rawToken)
            => int.TryParse(rawToken, out int _) && rawToken.First() != '0';
        private static bool IsSignedDouble(string rawToken)
            => double.TryParse(rawToken, NumberFormatInfo.InvariantInfo, out double _) && rawToken.First() != '0' && rawToken.Last() != '.';
        private static bool IsStringLiteral(string rawToken)
            => rawToken.StartsWith("\"") && rawToken.EndsWith("\"") && !rawToken.Contains('\n') && rawToken.Length > 1;

        public static TokenType GetTokenType(string rawToken)
        {
            if (DefaultTokenExists(rawToken))
            {
                return DefaultTypes[rawToken];
            }
            if (IsIdentifier(rawToken))
            {
                return TokenType.Identifier;
            }
            if (IsSignedInteger(rawToken))
            {
                return TokenType.SignedIntegerNumber;
            }
            if (IsSignedDouble(rawToken))
            {
                return TokenType.SignedDoubleNumber;
            }
            if (IsStringLiteral(rawToken))
            {
                return TokenType.StringLiteral;
            }

            return TokenType.Invalid;
        }
    }
}
