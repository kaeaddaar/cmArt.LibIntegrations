//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace cmArt.LibIntegrations.CsvFileReaderService
//{
//    public static class csvLine
//    {
//        public delegate char[] RuleType(char[] remainingCharactersIn);
//        public static IEnumerable<string> GetFields(string line)
//        {

//        }
//        private static char[] ReadLine(char[] line, RuleType[] Rules)
//        {
//            if (line.Length == 0) {  return new char[0]; }
//            char[] remainingCharacters = line;

//            while(remainingCharacters.Length > 0)
//            {
//                char[] field = ReadField(remainingCharacters);
//                remainingCharacters = remainingCharacters.Skip(field.Length).ToArray();
//            }
//        }
//        private static char[] ReadField(char[] remainingCharactersIn)
//        {

//            if (remainingCharactersIn.Length == null { throw new ArgumentNullException(); }
//            if (remainingCharactersIn.Length == 0) { return new char[0]; }
//            char[] remainingCharacters = remainingCharactersIn;

//            // if a Quote then skip to next character and go on to reading characters of a field
//            remainingCharacters = remainingCharacters.Skip(1).ToArray();
//            char NextChar = remainingCharacters.FirstOrDefault();
//            RuleType[] Rules = csvLine.FieldRules();

//            List<char> field = new List<char>();

//            while (NextChar != null)
//            {
//                foreach (var AppyRule in Rules)
//                {
//                    char[] tmp = AppyRule(remainingCharacters);
//                }
//                NextChar = remainingCharacters.Skip(1).FirstOrDefault();
//            }
//        }
//        private static char[] ReadField_Quoted(char[] remainingCharactersIn)
//        {
//            if (remainingCharactersIn.Length == null { throw new ArgumentNullException(); }
//            if (remainingCharactersIn.Length == 0) { return new char[0]; }
//            char[] remainingCharacters = remainingCharactersIn;

//            char first = remainingCharacters[0];
//            RuleType[] Rules = csvLine.ReadField_Quoted_Rules();

//        }
//        private static char[] Rule_Char_Quoted(char[] remainingCharactersIn) // skip quote and process ass Quoted Field
//        {

//        }
//        private static RuleType[] ReadField_Quoted_Rules()
//        {

//            List<RuleType> rules = new List<RuleType>();
//            rules.Add(csvLine.Rule_Char_Quoted);
//            rules.Add(csvLine.Rule_Char_Escape);
//            return rules.ToArray();

//        }
//        private static RuleType[] FieldRules()
//        {

//            List<RuleType> rules = new List<RuleType>();
//            rules.Add(csvLine.Rule_Quote);

//            return rules.ToArray();

//        }
//    }
//}
