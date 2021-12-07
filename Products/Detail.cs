using System;

namespace Store.Products
{
    class Detail : IDetail, IEquatable<IDetail>
    {
        readonly DetailFormat _format;
        string _value;

        public Detail(DetailFormat format, string value)
        {
            _format = format;
            Value = value;
        }

        public string Name => _format.Name;

        public string Value
        {
            get => _value;
            set
            {
                if (_format.Validator?.Invoke(value) == false)
                    throw new FormatException("Detail value format is wrong");
                _value = value;
            }
        }

        public IDetailFormat Format => _format;

        public override string ToString() => $"[{Name}, {_value}]";

        public override int GetHashCode() => Name.GetHashCode();

        public bool Equals(IDetail other) => Name.Equals(other.Name);

        public override bool Equals(object obj)
            => obj is IDetail && Equals(obj as IDetail);
    }
}
