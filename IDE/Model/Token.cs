﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IDE.Model
{
    internal enum TokenType
    {
        Identifier = 0,
        Complex,
        Integer,
        Double,
        Whitespace,
        Less,
        Greater,
        OpenParenthesis,
        CloseParenthesis,
        Minus,
        IntegerLiteral,
        DoubleLiteral,
        Comma,
        Semicolon,

        String,

        Newline,
        Assignment,

        OpenCurlyBracket,
        CloseCurlyBracket,

        StringLiteral,
        Plus,
        Multiply,
        Divide,
        Module,
 
        GreaterOrEqual,
        LessOrEqual,
        Equal,
        NotEqual,
        And,
        Or,
        Not,
        True,
        False,

        If,
        Else,
        For,
        While,
        Do,

        Instr,

        Invalid
    }

    internal class Token
    {
        public static Dictionary<string, TokenType> DefaultTypes { get; } 
            = new Dictionary<string, TokenType>()
        {
            { "int", TokenType.Integer },
            { "double", TokenType.Double },
            { "std::complex<double>", TokenType.Complex },
            
            { "\n", TokenType.Newline },
            { " ", TokenType.Whitespace },
            { ",", TokenType.Comma },
            { ";", TokenType.Semicolon },
            { "=", TokenType.Assignment },

            { "(", TokenType.OpenParenthesis },
            { ")", TokenType.CloseParenthesis },
            { "{", TokenType.OpenCurlyBracket},
            { "}", TokenType.CloseCurlyBracket },

            { "+", TokenType.Plus },
            { "-", TokenType.Minus },
            { "*", TokenType.Multiply },
            { "/", TokenType.Divide },
            { "%", TokenType.Module },

            { ">", TokenType.Greater},
            { "<", TokenType.Less },
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
            => rawToken.Length != 0 && (char.IsLetter(rawToken.First()) || rawToken.First() == '_') && (rawToken.StartsWith("std::") == (rawToken.Count(x => x == ':') == 2));
        private static bool IsIntegerLiteral(string rawToken)
            => int.TryParse(rawToken, out int _) && !rawToken.StartsWith("0.");
        private static bool IsDoubleLiteral(string rawToken)
        {
            return double.TryParse(rawToken, NumberFormatInfo.InvariantInfo, out double _) 
                && (rawToken.StartsWith("0.") != !rawToken.StartsWith('0'))
                && !rawToken.EndsWith('.');
        }
        
        private static bool IsInstr(string rawToken)
        {
            string instrs = "+-<>,.";
            foreach (char ch in rawToken)
            {
                if (!instrs.Contains(ch))
                {
                    return false;
                }
            }

            return true;
        }


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
            if (IsIntegerLiteral(rawToken))
            {
                return TokenType.IntegerLiteral;
            }
            if (IsDoubleLiteral(rawToken))
            {
                return TokenType.DoubleLiteral;
            }
            if (IsInstr(rawToken))
            {
                return TokenType.Instr;
            }

            return TokenType.Invalid;
        }
    }
}
