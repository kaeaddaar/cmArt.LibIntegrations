using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace cmArt.LibIntegrations.SerializationService
{
    public class GenericSerialization<T>
    {

        public static List<T> ReadOrDeserializeTable
        (
            string TableName
            , string CachedFilesDirectory
            , int RecordsPerPage
        )
        {
            bool cachedDataExists = false;

            string directory = $"{CachedFilesDirectory}";
            IEnumerable<string> Cachedfiles = GetCachedFileNamesFromDirectory(CachedFilesDirectory, TableName);

            cachedDataExists = Cachedfiles.Count() > 0;
            string filePathAndName = string.Empty;
            string fileContents = string.Empty;
            List<T> rows = new List<T>();

            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            };

            if (cachedDataExists)
            {
                Console.Write($"Loading {TableName} from Cache: ");
                Cachedfiles = Cachedfiles.Select(x => x.ToUpper());
                // load from cache
                for (int pageNum = 1; pageNum <= Cachedfiles.Count(); pageNum++)
                {
                    Console.Write($" - Load Pg {pageNum}");
                    filePathAndName = $"{CachedFilesDirectory}tbl{TableName}_page{pageNum}.json";
                    filePathAndName = filePathAndName.ToUpper();
                    if (Cachedfiles.Contains(filePathAndName))
                    {
                        fileContents = File.ReadAllText(filePathAndName);
                        List<T> TableRecords = new List<T>();
                        try
                        {
                            TableRecords = JsonSerializer.Deserialize<List<T>>(fileContents, options);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        foreach (T row in TableRecords)
                        {
                            rows.Add(row);
                        }
                    }
                }
                //Console.WriteLine("");
                //Console.WriteLine($"ReadOrSerializeTable for {TableName} Complete");
                return rows;
            }
            else
            {
                //Console.WriteLine($"Cached Data Doesn't Exist for \"{TableName}\"");
                return new List<T>();
            }

        }

        public static bool Exists(string CachedFilesDirectory, string TableName)
        {
            bool directoryExists = false;

            directoryExists = Directory.Exists(CachedFilesDirectory);
            IEnumerable<string> ListOfFiles = Directory.GetFiles(CachedFilesDirectory);

            bool FilesExist = false;
            try
            {
                FilesExist = ListOfFiles.Where(f => f.Contains($"tbl{TableName}_page")).Count() > 0;
            }
            catch
            {
                FilesExist = false;
            }
            return FilesExist;
        }
        public static void SerializeToJSON(List<T> ToSerialize, string TableName, string CachedFilesDirectory
            , int RecordsPerPage)
        {
            string content = string.Empty;
            List<T> tmpRows = new List<T>();

            int i = 0;
            int pageNum = 0;
            foreach (T row in ToSerialize)
            {
                i++;
                if (i < RecordsPerPage) // Looking for 10MB approx
                {
                    tmpRows.Add(row);
                }
                else
                {
                    pageNum++;
                    i = 0;
                    content = JsonSerializer.Serialize(tmpRows);
                    File.WriteAllText($"{CachedFilesDirectory}\\tbl{TableName}_page{pageNum}.json", content);
                    tmpRows = new List<T>();
                }
            }
            if (tmpRows.Count > 0)
            {
                pageNum++;
                content = JsonSerializer.Serialize(tmpRows);
                File.WriteAllText($"{CachedFilesDirectory}\\tbl{TableName}_page{pageNum}.json", content);
            }

        }

        public static List<string> GetCachedFileNamesFromDirectory(string directory, string TableName)
        {
            //Console.WriteLine("GetCachedFilesInDirectory (Begin), checking for files in " +
            //    TableName);

            bool directoryExists = false;

            directoryExists = Directory.Exists(directory);

            List<string> files = new List<string>();

            if (directoryExists)
            {
                //Console.WriteLine($"{files.Count} Cached file(s) found, loading cached files for " +
                //    TableName);
                foreach (string file in Directory.GetFiles(directory))
                {
                    if (file.Contains($"tbl{TableName}_page"))
                    {
                        files.Add(file);
                    }
                }
            }
            return files;
        }
        public static IEnumerable<string> RemoveCachedFileNamesFromDirectory(string directory, string TableName) // returns files removed
        {
            //Console.WriteLine("RemoveCachedFilesInDirectory (Begin), checking for files in " +
            //    TableName);
            IEnumerable<string> files = GetCachedFileNamesFromDirectory(directory, TableName); ;

            foreach (var file in files)
            {
                File.Delete(file);
            }
            return files;
        }

    }

}
