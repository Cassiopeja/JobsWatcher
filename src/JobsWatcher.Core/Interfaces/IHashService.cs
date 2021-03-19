namespace JobsWatcher.Core.Interfaces
{
    public interface IHashService
    {
        byte[] GetHash(string inputString);
        string GetHashString(string inputString);
    }
}