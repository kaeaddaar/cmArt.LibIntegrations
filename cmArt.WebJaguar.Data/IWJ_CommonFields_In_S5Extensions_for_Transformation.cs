using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.Data
{
    public class CatMap
    { 
        public string S5Cat { get; set; }
        public string WJ_IDs { get; set; }
    }
    public static class IWJ_CommonFields_In_S5Extensions_for_Transformation
    {
        public static IWJ_CommonFields_In_S5 CopyFrom(this IWJ_CommonFields_In_S5 to, IWJ_CommonFields_In_S5 from)
        {
            IWJ_CommonFields_In_S5 _from = from ?? new WJ_CommonFields();

            to.longDesc = _from.longDesc;
            to.name = _from.name;
            to.shortDesc = _from.shortDesc;
            to.sku = _from.sku;
            to.upc = _from.upc;
            to.weight = _from.weight;

            if (from.catIds != null)
            {
                to.catIds = new List<int>(_from.catIds);
            }
            else
            {
                to.catIds = new List<int>();
            }

            to.cost = _from.cost;
            to.field12 = from.field12;//S5 Sub Cat
            to.inventory = from.inventory;
            to.inventoryAFS = from.inventoryAFS;
            to.priceTable1 = from.priceTable1;
            to.priceTable2 = from.priceTable2;
            to.priceTable3 = from.priceTable3;
            to.priceTable4 = from.priceTable4;
            to.priceTable5 = from.priceTable5;
            to.priceTable6 = from.priceTable6;
            to.priceTable7 = from.priceTable7;
            to.priceTable8 = from.priceTable8;
            to.priceTable9 = from.priceTable9;
            to.priceTable10 = from.priceTable10;

            return to;
        }
        public static IEnumerable<int> MapWJ_IDs_Instead_of_Copying_From_FF(string S5Cat)
        {
            //int _S5Cat;
            //int.TryParse(S5Cat, out _S5Cat);
            IEnumerable<CatMap> CatMaps = new List<CatMap>();
            try
            {
                CatMaps = (List<CatMap>)System.Text.Json.JsonSerializer.Deserialize(GetJsonMapping(), typeof(List<CatMap>));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error trying to deserialize JSON for Category Mapping: " + e.Message);
            }
            CatMap result = CatMaps.Where(x => x.S5Cat == S5Cat.TrimEnd()).FirstOrDefault() ?? new CatMap();
            string ToSplit = result.WJ_IDs ?? string.Empty;
            IEnumerable<string> strIDs = ToSplit.Split(',');
            IEnumerable<int> IDs = strIDs.ToList().Select(x => 
            {
                int tmp;
                int.TryParse(x, out tmp);
                return tmp;
            } );
            return IDs;
        }
        private static string GetJsonMapping()
        {
            return @"[
  {
    ""S5Cat"": ""203"",
    ""WJ_IDs"": ""419,420,423""
  },
  {
    ""S5Cat"": ""126"",
    ""WJ_IDs"": ""253,254,255""
  },
  {
    ""S5Cat"": ""127"",
    ""WJ_IDs"": ""253,254,256""
  },
  {
    ""S5Cat"": ""128"",
    ""WJ_IDs"": ""253,254,257""
  },
  {
    ""S5Cat"": ""129"",
    ""WJ_IDs"": ""253,254,258""
  },
  {
    ""S5Cat"": ""176"",
    ""WJ_IDs"": ""253,259,260""
  },
  {
    ""S5Cat"": ""177"",
    ""WJ_IDs"": ""253,259,261""
  },
  {
    ""S5Cat"": ""178"",
    ""WJ_IDs"": ""253,259,262""
  },
  {
    ""S5Cat"": ""179"",
    ""WJ_IDs"": ""253,259,263""
  },
  {
    ""S5Cat"": ""104"",
    ""WJ_IDs"": ""253,259,264""
  },
  {
    ""S5Cat"": ""157"",
    ""WJ_IDs"": ""253,259,264""
  },
  {
    ""S5Cat"": ""180"",
    ""WJ_IDs"": ""253,259,264""
  },
  {
    ""S5Cat"": ""231"",
    ""WJ_IDs"": ""253,259,264""
  },
  {
    ""S5Cat"": ""357"",
    ""WJ_IDs"": ""253,259,264""
  },
  {
    ""S5Cat"": ""386"",
    ""WJ_IDs"": ""253,259,264""
  },
  {
    ""S5Cat"": ""530"",
    ""WJ_IDs"": ""253,259,264""
  },
  {
    ""S5Cat"": ""602"",
    ""WJ_IDs"": ""253,259,264""
  },
  {
    ""S5Cat"": ""679"",
    ""WJ_IDs"": ""253,259,264""
  },
  {
    ""S5Cat"": ""702"",
    ""WJ_IDs"": ""253,259,264""
  },
  {
    ""S5Cat"": ""181"",
    ""WJ_IDs"": ""253,259,265""
  },
  {
    ""S5Cat"": ""576"",
    ""WJ_IDs"": ""253,266,267""
  },
  {
    ""S5Cat"": ""577"",
    ""WJ_IDs"": ""253,266,268""
  },
  {
    ""S5Cat"": ""578"",
    ""WJ_IDs"": ""253,266,269""
  },
  {
    ""S5Cat"": ""579"",
    ""WJ_IDs"": ""253,266,270""
  },
  {
    ""S5Cat"": ""580"",
    ""WJ_IDs"": ""253,266,271""
  },
  {
    ""S5Cat"": ""601"",
    ""WJ_IDs"": ""253,272,273""
  },
  {
    ""S5Cat"": ""603"",
    ""WJ_IDs"": ""253,272,275""
  },
  {
    ""S5Cat"": ""604"",
    ""WJ_IDs"": ""253,272,276""
  },
  {
    ""S5Cat"": ""605"",
    ""WJ_IDs"": ""253,272,277""
  },
  {
    ""S5Cat"": ""606"",
    ""WJ_IDs"": ""253,272,278""
  },
  {
    ""S5Cat"": ""726"",
    ""WJ_IDs"": ""253,279,280""
  },
  {
    ""S5Cat"": ""727"",
    ""WJ_IDs"": ""253,279,281""
  },
  {
    ""S5Cat"": ""728"",
    ""WJ_IDs"": ""253,279,282""
  },
  {
    ""S5Cat"": ""729"",
    ""WJ_IDs"": ""253,279,283""
  },
  {
    ""S5Cat"": ""730"",
    ""WJ_IDs"": ""253,279,284""
  },
  {
    ""S5Cat"": ""731"",
    ""WJ_IDs"": ""253,279,285""
  },
  {
    ""S5Cat"": ""276"",
    ""WJ_IDs"": ""253,286,287""
  },
  {
    ""S5Cat"": ""277"",
    ""WJ_IDs"": ""253,286,288""
  },
  {
    ""S5Cat"": ""278"",
    ""WJ_IDs"": ""253,286,289""
  },
  {
    ""S5Cat"": ""279"",
    ""WJ_IDs"": ""253,286,290""
  },
  {
    ""S5Cat"": ""281"",
    ""WJ_IDs"": ""253,286,292""
  },
  {
    ""S5Cat"": ""282"",
    ""WJ_IDs"": ""253,286,293""
  },
  {
    ""S5Cat"": ""283"",
    ""WJ_IDs"": ""253,286,294""
  },
  {
    ""S5Cat"": ""284"",
    ""WJ_IDs"": ""253,286,295""
  },
  {
    ""S5Cat"": ""285"",
    ""WJ_IDs"": ""253,286,296""
  },
  {
    ""S5Cat"": ""286"",
    ""WJ_IDs"": ""253,286,297""
  },
  {
    ""S5Cat"": ""287"",
    ""WJ_IDs"": ""253,286,298""
  },
  {
    ""S5Cat"": ""288"",
    ""WJ_IDs"": ""253,286,299""
  },
  {
    ""S5Cat"": ""289"",
    ""WJ_IDs"": ""253,286,300""
  },
  {
    ""S5Cat"": ""290"",
    ""WJ_IDs"": ""253,286,301""
  },
  {
    ""S5Cat"": ""301"",
    ""WJ_IDs"": ""253,302,303""
  },
  {
    ""S5Cat"": ""302"",
    ""WJ_IDs"": ""253,302,304""
  },
  {
    ""S5Cat"": ""303"",
    ""WJ_IDs"": ""253,302,305""
  },
  {
    ""S5Cat"": ""304"",
    ""WJ_IDs"": ""253,302,306""
  },
  {
    ""S5Cat"": ""305"",
    ""WJ_IDs"": ""253,302,307""
  },
  {
    ""S5Cat"": ""306"",
    ""WJ_IDs"": ""253,302,308""
  },
  {
    ""S5Cat"": ""401"",
    ""WJ_IDs"": ""253,309,310""
  },
  {
    ""S5Cat"": ""402"",
    ""WJ_IDs"": ""253,309,311""
  },
  {
    ""S5Cat"": ""403"",
    ""WJ_IDs"": ""253,309,312""
  },
  {
    ""S5Cat"": ""404"",
    ""WJ_IDs"": ""253,309,313""
  },
  {
    ""S5Cat"": ""405"",
    ""WJ_IDs"": ""253,309,314""
  },
  {
    ""S5Cat"": ""455"",
    ""WJ_IDs"": ""253,309,314""
  },
  {
    ""S5Cat"": ""651"",
    ""WJ_IDs"": ""253,315,316""
  },
  {
    ""S5Cat"": ""652"",
    ""WJ_IDs"": ""253,315,317""
  },
  {
    ""S5Cat"": ""653"",
    ""WJ_IDs"": ""253,315,318""
  },
  {
    ""S5Cat"": ""654"",
    ""WJ_IDs"": ""253,315,319""
  },
  {
    ""S5Cat"": ""155"",
    ""WJ_IDs"": ""253,320,321""
  },
  {
    ""S5Cat"": ""206"",
    ""WJ_IDs"": ""253,320,321""
  },
  {
    ""S5Cat"": ""701"",
    ""WJ_IDs"": ""253,320,321""
  },
  {
    ""S5Cat"": ""676"",
    ""WJ_IDs"": ""253,323,324""
  },
  {
    ""S5Cat"": ""677"",
    ""WJ_IDs"": ""253,323,325""
  },
  {
    ""S5Cat"": ""678"",
    ""WJ_IDs"": ""253,323,326""
  },
  {
    ""S5Cat"": ""680"",
    ""WJ_IDs"": ""253,323,328""
  },
  {
    ""S5Cat"": ""426"",
    ""WJ_IDs"": ""253,329,330""
  },
  {
    ""S5Cat"": ""427"",
    ""WJ_IDs"": ""253,329,331""
  },
  {
    ""S5Cat"": ""428"",
    ""WJ_IDs"": ""253,329,332""
  },
  {
    ""S5Cat"": ""429"",
    ""WJ_IDs"": ""253,329,333""
  },
  {
    ""S5Cat"": ""451"",
    ""WJ_IDs"": ""253,334,335""
  },
  {
    ""S5Cat"": ""452"",
    ""WJ_IDs"": ""253,334,336""
  },
  {
    ""S5Cat"": ""556"",
    ""WJ_IDs"": ""253,334,336""
  },
  {
    ""S5Cat"": ""453"",
    ""WJ_IDs"": ""253,334,337""
  },
  {
    ""S5Cat"": ""454"",
    ""WJ_IDs"": ""253,334,338""
  },
  {
    ""S5Cat"": ""526"",
    ""WJ_IDs"": ""253,340,341""
  },
  {
    ""S5Cat"": ""628"",
    ""WJ_IDs"": ""253,340,341""
  },
  {
    ""S5Cat"": ""527"",
    ""WJ_IDs"": ""253,340,342""
  },
  {
    ""S5Cat"": ""528"",
    ""WJ_IDs"": ""253,340,343""
  },
  {
    ""S5Cat"": ""529"",
    ""WJ_IDs"": ""253,340,344""
  },
  {
    ""S5Cat"": ""476"",
    ""WJ_IDs"": ""346,347,348""
  },
  {
    ""S5Cat"": ""477"",
    ""WJ_IDs"": ""346,347,349""
  },
  {
    ""S5Cat"": ""478"",
    ""WJ_IDs"": ""346,347,350""
  },
  {
    ""S5Cat"": ""479"",
    ""WJ_IDs"": ""346,347,351""
  },
  {
    ""S5Cat"": ""480"",
    ""WJ_IDs"": ""346,347,352""
  },
  {
    ""S5Cat"": ""481"",
    ""WJ_IDs"": ""346,347,353""
  },
  {
    ""S5Cat"": ""482"",
    ""WJ_IDs"": ""346,347,354""
  },
  {
    ""S5Cat"": ""483"",
    ""WJ_IDs"": ""346,347,355""
  },
  {
    ""S5Cat"": ""484"",
    ""WJ_IDs"": ""346,347,356""
  },
  {
    ""S5Cat"": ""485"",
    ""WJ_IDs"": ""346,347,357""
  },
  {
    ""S5Cat"": ""486"",
    ""WJ_IDs"": ""346,347,358""
  },
  {
    ""S5Cat"": ""487"",
    ""WJ_IDs"": ""346,347,359""
  },
  {
    ""S5Cat"": ""489"",
    ""WJ_IDs"": ""346,347,360""
  },
  {
    ""S5Cat"": ""001"",
    ""WJ_IDs"": ""346,361,362""
  },
  {
    ""S5Cat"": ""002"",
    ""WJ_IDs"": ""346,361,363""
  },
  {
    ""S5Cat"": ""003"",
    ""WJ_IDs"": ""346,361,364""
  },
  {
    ""S5Cat"": ""004"",
    ""WJ_IDs"": ""346,361,365""
  },
  {
    ""S5Cat"": ""005"",
    ""WJ_IDs"": ""346,361,366""
  },
  {
    ""S5Cat"": ""006"",
    ""WJ_IDs"": ""346,361,367""
  },
  {
    ""S5Cat"": ""007"",
    ""WJ_IDs"": ""346,361,368""
  },
  {
    ""S5Cat"": ""008"",
    ""WJ_IDs"": ""346,361,369""
  },
  {
    ""S5Cat"": ""009"",
    ""WJ_IDs"": ""346,361,370""
  },
  {
    ""S5Cat"": ""010"",
    ""WJ_IDs"": ""346,361,371""
  },
  {
    ""S5Cat"": ""328"",
    ""WJ_IDs"": ""372,373,376""
  },
  {
    ""S5Cat"": ""554"",
    ""WJ_IDs"": ""372,373,376""
  },
  {
    ""S5Cat"": ""329"",
    ""WJ_IDs"": ""372,373,377""
  },
  {
    ""S5Cat"": ""555"",
    ""WJ_IDs"": ""372,373,377""
  },
  {
    ""S5Cat"": ""330"",
    ""WJ_IDs"": ""372,373,378""
  },
  {
    ""S5Cat"": ""376"",
    ""WJ_IDs"": ""372,379,380""
  },
  {
    ""S5Cat"": ""377"",
    ""WJ_IDs"": ""372,379,381""
  },
  {
    ""S5Cat"": ""378"",
    ""WJ_IDs"": ""372,379,382""
  },
  {
    ""S5Cat"": ""379"",
    ""WJ_IDs"": ""372,379,383""
  },
  {
    ""S5Cat"": ""381"",
    ""WJ_IDs"": ""372,379,385""
  },
  {
    ""S5Cat"": ""391"",
    ""WJ_IDs"": ""372,379,387""
  },
  {
    ""S5Cat"": ""501"",
    ""WJ_IDs"": ""372,388,389""
  },
  {
    ""S5Cat"": ""502"",
    ""WJ_IDs"": ""372,388,390""
  },
  {
    ""S5Cat"": ""503"",
    ""WJ_IDs"": ""372,388,391""
  },
  {
    ""S5Cat"": ""504"",
    ""WJ_IDs"": ""372,388,392""
  },
  {
    ""S5Cat"": ""505"",
    ""WJ_IDs"": ""372,388,393""
  },
  {
    ""S5Cat"": ""506"",
    ""WJ_IDs"": ""372,388,394""
  },
  {
    ""S5Cat"": ""507"",
    ""WJ_IDs"": ""372,388,395""
  },
  {
    ""S5Cat"": ""508"",
    ""WJ_IDs"": ""372,388,396""
  },
  {
    ""S5Cat"": ""509"",
    ""WJ_IDs"": ""372,388,397""
  },
  {
    ""S5Cat"": ""380"",
    ""WJ_IDs"": ""372,388,398""
  },
  {
    ""S5Cat"": ""510"",
    ""WJ_IDs"": ""372,388,398""
  },
  {
    ""S5Cat"": ""511"",
    ""WJ_IDs"": ""372,388,399""
  },
  {
    ""S5Cat"": ""512"",
    ""WJ_IDs"": ""372,388,400""
  },
  {
    ""S5Cat"": ""513"",
    ""WJ_IDs"": ""372,388,401""
  },
  {
    ""S5Cat"": ""514"",
    ""WJ_IDs"": ""372,388,402""
  },
  {
    ""S5Cat"": ""551"",
    ""WJ_IDs"": ""372,403,404""
  },
  {
    ""S5Cat"": ""552"",
    ""WJ_IDs"": ""372,403,405""
  },
  {
    ""S5Cat"": ""327"",
    ""WJ_IDs"": ""372,403,406""
  },
  {
    ""S5Cat"": ""553"",
    ""WJ_IDs"": ""372,403,406""
  },
  {
    ""S5Cat"": ""151"",
    ""WJ_IDs"": ""410,411""
  },
  {
    ""S5Cat"": ""152"",
    ""WJ_IDs"": ""410,412""
  },
  {
    ""S5Cat"": ""153"",
    ""WJ_IDs"": ""410,413""
  },
  {
    ""S5Cat"": ""154"",
    ""WJ_IDs"": ""410,414""
  },
  {
    ""S5Cat"": ""156"",
    ""WJ_IDs"": ""410,416""
  },
  {
    ""S5Cat"": ""158"",
    ""WJ_IDs"": ""410,418""
  },
  {
    ""S5Cat"": ""627"",
    ""WJ_IDs"": ""410,418""
  },
  {
    ""S5Cat"": ""201"",
    ""WJ_IDs"": ""419,420,421""
  },
  {
    ""S5Cat"": ""202"",
    ""WJ_IDs"": ""419,420,422""
  },
  {
    ""S5Cat"": ""204"",
    ""WJ_IDs"": ""419,420,424""
  },
  {
    ""S5Cat"": ""205"",
    ""WJ_IDs"": ""419,420,425""
  },
  {
    ""S5Cat"": ""207"",
    ""WJ_IDs"": ""419,420,427""
  },
  {
    ""S5Cat"": ""208"",
    ""WJ_IDs"": ""419,420,428""
  },
  {
    ""S5Cat"": ""209"",
    ""WJ_IDs"": ""419,420,429""
  },
  {
    ""S5Cat"": ""210"",
    ""WJ_IDs"": ""419,420,430""
  },
  {
    ""S5Cat"": ""351"",
    ""WJ_IDs"": ""431,432,433""
  },
  {
    ""S5Cat"": ""352"",
    ""WJ_IDs"": ""431,432,434""
  },
  {
    ""S5Cat"": ""280"",
    ""WJ_IDs"": ""431,432,435""
  },
  {
    ""S5Cat"": ""353"",
    ""WJ_IDs"": ""431,432,435""
  },
  {
    ""S5Cat"": ""354"",
    ""WJ_IDs"": ""431,432,436""
  },
  {
    ""S5Cat"": ""355"",
    ""WJ_IDs"": ""431,432,437""
  },
  {
    ""S5Cat"": ""356"",
    ""WJ_IDs"": ""431,432,438""
  },
  {
    ""S5Cat"": ""358"",
    ""WJ_IDs"": ""431,432,440""
  },
  {
    ""S5Cat"": ""359"",
    ""WJ_IDs"": ""431,432,441""
  },
  {
    ""S5Cat"": ""360"",
    ""WJ_IDs"": ""431,432,442""
  },
  {
    ""S5Cat"": ""101"",
    ""WJ_IDs"": ""443,444""
  },
  {
    ""S5Cat"": ""102"",
    ""WJ_IDs"": ""443,445""
  },
  {
    ""S5Cat"": ""103"",
    ""WJ_IDs"": ""443,446""
  },
  {
    ""S5Cat"": ""106"",
    ""WJ_IDs"": ""443,448""
  },
  {
    ""S5Cat"": ""226"",
    ""WJ_IDs"": ""449,450""
  },
  {
    ""S5Cat"": ""227"",
    ""WJ_IDs"": ""449,451""
  },
  {
    ""S5Cat"": ""228"",
    ""WJ_IDs"": ""449,452""
  },
  {
    ""S5Cat"": ""229"",
    ""WJ_IDs"": ""449,453""
  },
  {
    ""S5Cat"": ""230"",
    ""WJ_IDs"": ""449,454""
  },
  {
    ""S5Cat"": ""232"",
    ""WJ_IDs"": ""449,456""
  },
  {
    ""S5Cat"": ""233"",
    ""WJ_IDs"": ""449,457""
  },
  {
    ""S5Cat"": ""234"",
    ""WJ_IDs"": ""449,458""
  },
  {
    ""S5Cat"": ""251"",
    ""WJ_IDs"": ""449,459,460""
  },
  {
    ""S5Cat"": ""252"",
    ""WJ_IDs"": ""449,459,461""
  }
]";
        }
    }
}
