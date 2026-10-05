using System;

namespace CompilerPascal
{
    class LexicalAnalyzer
    {
        public const byte Star = 21;
        public const byte Slash = 60;
        public const byte Equal = 16;
        public const byte Comma = 20;
        public const byte Semicolon = 14;
        public const byte Colon = 5;
        public const byte Point = 61;
        public const byte Arrow = 62;
        public const byte Leftpar = 9;
        public const byte Rightpar = 10;
        public const byte Lbracket = 11;
        public const byte Rbracket = 12;
        public const byte Later = 65;
        public const byte Greater = 66;
        public const byte Laterequal = 67;
        public const byte Greaterequal = 68;
        public const byte Latergreater = 69;
        public const byte Plus = 70;
        public const byte Minus = 71;
        public const byte Assign = 51;
        public const byte Twopoints = 74;
        
        public const byte Ident = 2;
        public const byte Intc = 15;
        public const byte Floatc = 82;
        public const byte Charc = 80;

        public const byte Casesy = 31;
        public const byte Elsesy = 32;
        public const byte Filesy = 57;
        public const byte Gotosy = 33;
        public const byte Thensy = 52;
        public const byte Typesy = 34;
        public const byte Untilsy = 53;
        public const byte Dosy = 54;
        public const byte Withsy = 37;
        public const byte Ifsy = 56;
        public const byte Insy = 100;
        public const byte Ofsy = 101;
        public const byte Orsy = 102;
        public const byte Tosy = 103;
        public const byte Endsy = 104;
        public const byte Varsy = 105;
        public const byte Divsy = 106;
        public const byte Andsy = 107;
        public const byte Notsy = 108;
        public const byte Forsy = 109;
        public const byte Modsy = 110;
        public const byte Nilsy = 111;
        public const byte Setsy = 112;
        public const byte Beginsy = 113;
        public const byte Whilesy = 114;
        public const byte Arraysy = 115;
        public const byte Constsy = 116;
        public const byte Labelsy = 117;
        public const byte Downtosy = 118;
        public const byte Packedsy = 119;
        public const byte Recordsy = 120;
        public const byte Repeatsy = 121;
        public const byte Programsy = 122;
        public const byte Functionsy = 123;
        public const byte Procedurensy = 124;

        public byte Symbol { get; set; }
        public TextPosition Token { get; set; }
        public string AddrName { get; set; }
        public int NmbInt { get; set; }
        public float NmbFloat { get; set; }
        public char CharValue { get; set; }

        private static int _level = 0;
        private static TextPosition _unclosedParenPos = new TextPosition(0, 0);
        private bool _pendingTwoPoints = false;

        public LexicalAnalyzer()
        {
            Token = new TextPosition();
            AddrName = "";
            NmbInt = 0;
            NmbFloat = 0;
            CharValue = '\0';
        }

        public static void CheckParenBalance()
        {
            if (_level > 0)
            {
                InputOutput.Error(105, _unclosedParenPos);
            }
        }

        public byte NextSym()
        {
            if (_pendingTwoPoints)
            {
                _pendingTwoPoints = false;
                Symbol = Twopoints;
                InputOutput.NextCh();
                return Symbol;
            }

            while (!InputOutput.EndOfFile && InputOutput.Ch == ' ')
            {
                InputOutput.NextCh();
            }

            if (InputOutput.EndOfFile) return 0;

            Token = InputOutput.PositionNow;
            char ch = InputOutput.Ch;

            if (char.IsLetter(ch) || ch == '_')
            {
                string name = "";
                while (!InputOutput.EndOfFile && (char.IsLetterOrDigit(InputOutput.Ch) || InputOutput.Ch == '_'))
                {
                    name += InputOutput.Ch;
                    InputOutput.NextCh();
                }
                
                byte code = Keywords.GetCode(name.ToLower());
                if (code != 0)
                {
                    Symbol = code;
                }
                else
                {
                    Symbol = Ident;
                    AddrName = name;
                }
                return Symbol;
            }

            if (char.IsDigit(ch))
            {
                const int maxint = 32767;
                NmbInt = 0;
                bool overflow = false;
                TextPosition startPos = Token;

                while (!InputOutput.EndOfFile && char.IsDigit(InputOutput.Ch))
                {
                    int digit = InputOutput.Ch - '0';
                    if (NmbInt > (maxint - digit) / 10)
                    {
                        InputOutput.Error(203, startPos);
                        overflow = true;
                        while (!InputOutput.EndOfFile && char.IsDigit(InputOutput.Ch))
                            InputOutput.NextCh();
                        NmbInt = 0;
                        break;
                    }
                    NmbInt = NmbInt * 10 + digit;
                    InputOutput.NextCh();
                }

                if (!overflow && !InputOutput.EndOfFile && InputOutput.Ch == '.')
                {
                    TextPosition dotPos = InputOutput.PositionNow;
                    InputOutput.NextCh();
                    
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '.')
                    {
                        _pendingTwoPoints = true; 
                        Symbol = Intc;
                        return Symbol; 
                    }
                    else if (!InputOutput.EndOfFile && char.IsDigit(InputOutput.Ch))
                    {
                        float fraction = 0;
                        float divisor = 1;
                        while (!InputOutput.EndOfFile && char.IsDigit(InputOutput.Ch))
                        {
                            fraction = fraction * 10 + (InputOutput.Ch - '0');
                            divisor *= 10;
                            InputOutput.NextCh();
                        }
                        NmbFloat = NmbInt + fraction / divisor;
                        Symbol = Floatc;
                        return Symbol;
                    }
                    else
                    {
                        InputOutput.Error(147, dotPos);
                        Symbol = Intc;
                        return Symbol;
                    }
                }
                Symbol = Intc;
                return Symbol;
            }

            switch (ch)
            {
                case ',': Symbol = Comma; InputOutput.NextCh(); break;
                case ';': Symbol = Semicolon; InputOutput.NextCh(); break;
                case '+': Symbol = Plus; InputOutput.NextCh(); break;
                case '-': Symbol = Minus; InputOutput.NextCh(); break;
                case '=': Symbol = Equal; InputOutput.NextCh(); break;
                case '[': Symbol = Lbracket; InputOutput.NextCh(); break;
                case ']': Symbol = Rbracket; InputOutput.NextCh(); break;
                case '^': Symbol = Arrow; InputOutput.NextCh(); break;
                case '/':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '/')
                    {
                        while (!InputOutput.EndOfFile && InputOutput.PositionNow.CharNumber < InputOutput.LastInLine)
                            InputOutput.NextCh();
                        if (!InputOutput.EndOfFile) InputOutput.NextCh();
                        return NextSym();
                    }
                    else Symbol = Slash;
                    break;
                case '*': Symbol = Star; InputOutput.NextCh(); break;
                case '(':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '*')
                    {
                        InputOutput.NextCh();
                        while (!InputOutput.EndOfFile)
                        {
                            if (InputOutput.Ch == '*')
                            {
                                InputOutput.NextCh();
                                if (!InputOutput.EndOfFile && InputOutput.Ch == ')')
                                {
                                    InputOutput.NextCh();
                                    break;
                                }
                            }
                            else InputOutput.NextCh();
                        }
                        return NextSym();
                    }
                    else
                    {
                        Symbol = Leftpar;
                        if (_level == 0) _unclosedParenPos = Token;
                        _level++;
                    }
                    break;
                case ')':
                    if (_level == 0) InputOutput.Error(106, InputOutput.PositionNow);
                    else
                    {
                        _level--;
                        if (_level == 0) _unclosedParenPos = new TextPosition(0, 0);
                    }
                    Symbol = Rightpar;
                    InputOutput.NextCh();
                    break;
                case ':':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '=')
                    {
                        Symbol = Assign;
                        InputOutput.NextCh();
                    }
                    else Symbol = Colon;
                    break;
                case '<':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '=') { Symbol = Laterequal; InputOutput.NextCh(); }
                    else if (!InputOutput.EndOfFile && InputOutput.Ch == '>') { Symbol = Latergreater; InputOutput.NextCh(); }
                    else Symbol = Later;
                    break;
                case '>':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '=') { Symbol = Greaterequal; InputOutput.NextCh(); }
                    else Symbol = Greater;
                    break;
                case '.':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '.')
                    {
                        Symbol = Twopoints;
                        InputOutput.NextCh();
                    }
                    else Symbol = Point;
                    break;
                case '{':
                    InputOutput.NextCh();
                    while (!InputOutput.EndOfFile && InputOutput.Ch != '}') InputOutput.NextCh();
                    if (InputOutput.Ch == '}') InputOutput.NextCh();
                    return NextSym();
                case '}':
                    InputOutput.NextCh();
                    return NextSym();
                default:
                    InputOutput.Error(1, InputOutput.PositionNow);
                    InputOutput.NextCh();
                    return NextSym();
            }
            return Symbol;
        }
    }
}