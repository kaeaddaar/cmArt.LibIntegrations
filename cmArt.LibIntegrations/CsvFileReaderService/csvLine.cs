using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.LibIntegrations.CsvFileReaderService
{
    public static class csvLine
    {
        public enum Action
        {
            ReadingLine,    
            ReadingField,   
            ReadingQuotedField
        }
        public delegate Action RuleType(char[] remainingCharactersIn);
        public static IEnumerable<string> GetFields(string line)
        {
            Action current = Action.ReadingLine;
            List<string> fields = new List<string>();

            List<char> currentField = new List<char>();

            Func<char, string> fNextField = (x) =>
            {
                string strField = string.Join(string.Empty, currentField);
                currentField = new List<char>();
                fields.Add(strField);
                current = Action.ReadingLine;
                return strField;
            };

            char[] line_chars = line.ToCharArray();
            for (int i = 0; i < line_chars.Length; i++)
            {
                Func<char[], char> fEscapedCharacter = (x) =>
                {
                    i++;
                    return x[1];
                };
                char currChar = line[i];
                switch (current)
                {
                    case Action.ReadingLine:
                        if (IsQuote(currChar)) { current = Action.ReadingQuotedField; break; }
                        if (IsComma(currChar)) { string strField = fNextField(currChar); break; }

                        current = Action.ReadingField;
                        currentField.Add(currChar);
                        break;
                    case Action.ReadingField:
                        if (IsComma(currChar)) { current = Action.ReadingLine; string strField = fNextField(currChar); break; }

                        currentField.Add(currChar);
                        break;
                    case Action.ReadingQuotedField:
                        if (IsQuote(currChar)) { current = Action.ReadingLine; string strField = fNextField(currChar); break; }
                        if (IsEscape(currChar)) { currChar = fEscapedCharacter(line_chars); break; }

                        currentField.Add(currChar);
                        break;
                    default:
                        
                        break;
                }
            }
            fNextField('\n');
            return fields;
        }

        private static bool IsQuote(char first) { return first == '"'; }
        private static bool IsComma(char first) { return first == ','; }
        private static bool IsEscape(char first) { return first == '\\'; }
    }

}
