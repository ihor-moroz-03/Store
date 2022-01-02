using System.Text.RegularExpressions;

namespace StoreLogic.Products
{
    public delegate bool DValidator(string detail);

    class DetailFormat : IDetailFormat
    {
        public readonly string Name;
        public readonly DValidator Validator;
        public readonly string Description;

        public DetailFormat(string name, DValidator validator)
            : this(name, validator, "")
        { }

        public DetailFormat(string name, DValidator validator, string description)
        {
            Name = name; Validator = validator; Description = description;
        }

        string IDetailFormat.Name => Name;

        string IDetailFormat.Description => Description;

        public static DetailFormat Parse(string s)
        {
            Match m = Regex.Match(s, @"\[(\w+), (.+), (.+)\]");
            return new DetailFormat(
                m.Groups[1].Value,
                m.Groups[3].Success ? new Regex(m.Groups[3].Value).IsMatch : null,
                m.Groups[2].Value
                );
        }

        public override string ToString() => $"[{Name}, {Description}]";

        public override int GetHashCode() => Name.GetHashCode();

        public override bool Equals(object obj) => obj is DetailFormat && Name == (obj as DetailFormat).Name;
    }
}
