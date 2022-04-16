using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.SerializationService
{
    public interface IGenericSerialization<T>
    {
        List<T> ReadOrDeserializeTable
        (
            string TableName
            , string CachedFilesDirectory
            , int RecordsPerPage
        );
        bool Exists(string CachedFilesDirectory, string TableName);
        void SerializeToJSON(List<T> ToSerialize, string TableName, string CachedFilesDirectory, int RecordsPerPage);
        List<string> GetCachedFileNamesFromDirectory(string directory, string TableName);
        IEnumerable<string> RemoveCachedFileNamesFromDirectory(string directory, string TableName);
    }
}
