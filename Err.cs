namespace CompilerPascal
{
    public struct Err
    {
        public TextPosition ErrorPosition { get; set; }
        public byte ErrorCode { get; set; }

        public Err(TextPosition position, byte code)
        {
            ErrorPosition = position;
            ErrorCode = code;
        }
    }
}