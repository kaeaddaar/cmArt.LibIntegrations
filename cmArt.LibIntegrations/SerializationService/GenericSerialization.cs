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
            string fileName = string.Empty;
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
                // load from cache
                for (int pageNum = 1; pageNum <= Cachedfiles.Count(); pageNum++)
                {
                    Console.Write($" - Load Pg {pageNum}");
                    fileName = $"{CachedFilesDirectory}\\tbl{TableName}_page{pageNum}.json";
                    if (Cachedfiles.Contains(fileName))
                    {
                        fileContents = File.ReadAllText(fileName);
                        List<T> TableRecords;
                        TableRecords = JsonSerializer.Deserialize<List<T>>(fileContents, options);
                        foreach (T row in TableRecords)
                        {
                            rows.Add(row);
                        }
                    }
                }
                Console.WriteLine("");
                Console.WriteLine($"ReadOrSerializeTable for {TableName} Complete");
                return rows;
            }
            else
            {
                Console.WriteLine($"Cached Data Doesn't Exist for \"{TableName}\"");
                //Console.Write($"Loading {TableName} from Database.");
                //List<T> TableRecords = Load_Table_FromDatabase();
                //GenericSerialization<T>.SerializeToJSON(TableRecords, TableName, CachedFilesDirectory, RecordsPerPage);
                //Console.Write($"Finished Loading {TableName} from Database.");
                //return TableRecords;
                return new List<T>();
            }

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

        private static List<string> GetCachedFileNamesFromDirectory(string directory, string TableName)
        {
            Console.WriteLine("GetCachedFilesInDirectory (Begin), checking for files in " +
                TableName);

            bool directoryExists = false;

            directoryExists = Directory.Exists(directory);

            List<string> files = new List<string>();

            if (directoryExists)
            {
                Console.WriteLine($"{files.Count} Cached file(s) found, loading cached files for " +
                    TableName);
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

    }

}
