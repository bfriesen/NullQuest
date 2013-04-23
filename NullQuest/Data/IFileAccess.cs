using System.IO;

namespace NullQuest.Data
{
    public interface IFileAccess
    {
        bool DoesFileExist { get; }
        StreamReader CreateReader();
        StreamWriter CreateWriter();
    }
}
