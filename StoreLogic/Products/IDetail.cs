namespace StoreLogic.Products
{
    public interface IDetail
    {
        string Name { get; }
        string Value { get; set; }
        IDetailFormat Format { get; }
    }
}
