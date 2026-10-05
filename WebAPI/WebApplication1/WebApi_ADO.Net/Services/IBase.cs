namespace WebApi_ADO.Net.Services
{
    public interface  IBase<T>
    {
        bool Add(T Info);
        bool Remove(int id);
    }
}
