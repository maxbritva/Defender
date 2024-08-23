namespace Save
{
    public interface IDataProvider
    {
        void Save();

        bool TryLoad();
    }
}