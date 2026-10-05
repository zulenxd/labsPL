using System.Collections.Generic;

namespace CompilerPascal
{
    public enum DataType 
    { 
        Unknown, Integer, Real, Char, Boolean, Array 
    }

    public enum SymbolKind 
    { 
        Variable, Procedure, Parameter 
    }

    public class ParameterInfo
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value?.ToLower();
        }
        public DataType Type { get; set; }
        public bool IsVar { get; set; }
    }

    public class Symbol
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value?.ToLower(); 
        }

        public SymbolKind Kind { get; set; }
        public DataType Type { get; set; }
        public List<ParameterInfo> Parameters { get; set; }

        public DataType ElementType { get; set; }
        public int ArrayStart { get; set; }
        public int ArrayEnd { get; set; }

        public Symbol()
        {
            Parameters = new List<ParameterInfo>();
        }
    }

    public class SymbolTable
    {
        private Stack<Dictionary<string, Symbol>> _scopes;

        public SymbolTable()
        {
            _scopes = new Stack<Dictionary<string, Symbol>>();
            PushScope();
        }

        public void PushScope() => 
            _scopes.Push(new Dictionary<string, Symbol>(System.StringComparer.OrdinalIgnoreCase));

        public void PopScope() => _scopes.Pop();

        public bool AddSymbol(Symbol sym)
        {
            var currentScope = _scopes.Peek();
            if (currentScope.ContainsKey(sym.Name))
            {
                return false;
            }

            currentScope[sym.Name] = sym;
            return true;
        }

        public Symbol Find(string name)
        {
            if (name == null) return null;
            
            string searchName = name.ToLower();
            foreach (var scope in _scopes)
            {
                if (scope.TryGetValue(searchName, out Symbol sym))
                {
                    return sym;
                }
            }
            return null;
        }
    }
}