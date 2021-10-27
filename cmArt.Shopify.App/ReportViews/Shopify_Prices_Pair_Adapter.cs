using cmArt.LibIntegrations;
using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace cmArt.Shopify.App.ReportViews
{
    public class Shopify_Prices_Pair_Adapter : IShopify_Prices_Pair_Flat
    {
        private Shopify_Prices _S5;
        private Shopify_Prices _Shopify;

        private string AsString(IEnumerable<S5PricePair> data)
        {
            IEnumerable<S5PricePair> tmp = data ?? new List<S5PricePair>();
            tmp = tmp.OrderBy((x) => x.Level);
            IEnumerable<string> Ps = tmp.Select(x => x.Level.ToString() + ":" + x.Price.ToString());
            string CommaSeparatedList = string.Join(',', Ps);
            return CommaSeparatedList;
        }
        private IEnumerable<S5PricePair> AsS5PricePairList(string data)
        {
            string CommaSeparatedList = data ?? string.Empty;
            IEnumerable<string> Ps = CommaSeparatedList.Split(',');
            IEnumerable<S5PricePair> tmp = Ps.Select(x =>
            {
                string[] tmpPair = x.Split(':');
                short level;
                decimal price;
                short.TryParse(tmpPair[0], out level);
                decimal.TryParse(tmpPair[1], out price);
                return (new S5PricePair(level, price));
            });
            return tmp;
        }
        public string LeftPrices 
        {
            get
            {
                return AsString(((IShopify_Prices)_S5).Prices);
            }

            set
            {
                ((IShopify_Prices)_S5).Prices = AsS5PricePairList(value);
            }
        }
        public string LeftCat { get => ((IShopify_Identity)_S5).Cat; set => ((IShopify_Identity)_S5).Cat = value; }
        public int LeftInvUnique { get => ((IShopify_Identity)_S5).InvUnique; set => ((IShopify_Identity)_S5).InvUnique = value; }
        public string LeftPartNumber { get => ((IShopify_Identity)_S5).PartNumber; set => ((IShopify_Identity)_S5).PartNumber = value; }
        public decimal LeftWholesaleCost { get => ((IShopify_Prices)_S5).WholesaleCost; set => ((IShopify_Prices)_S5).WholesaleCost = value; }

        public string RightPrices
        {
            get
            {
                return AsString(((IShopify_Prices)_Shopify).Prices);
            }

            set
            {
                ((IShopify_Prices)_Shopify).Prices = AsS5PricePairList(value);
            }
        }
        public string RightCat { get => ((IShopify_Identity)_Shopify).Cat; set => ((IShopify_Identity)_Shopify).Cat = value; }
        public int RightInvUnique { get => ((IShopify_Identity)_Shopify).InvUnique; set => ((IShopify_Identity)_Shopify).InvUnique = value; }
        public string RightPartNumber { get => ((IShopify_Identity)_Shopify).PartNumber; set => ((IShopify_Identity)_Shopify).PartNumber = value; }
        public decimal RightWholesaleCost { get => ((IShopify_Prices)_Shopify).WholesaleCost; set => ((IShopify_Prices)_Shopify).WholesaleCost = value; }


        public Shopify_Prices_Pair_Adapter()
        {
            _Init();
        }
        public Shopify_Prices_Pair_Adapter(Shopify_Prices_Pair shopify_Prices_Pair)
        {
            _Init(shopify_Prices_Pair);
        }
        private void _Init(Shopify_Prices_Pair shopify_Prices_Pair)
        {
            _S5 = shopify_Prices_Pair.S5 ?? new Shopify_Prices();
            _Shopify = shopify_Prices_Pair.Shopify ?? new Shopify_Prices();
        }
        private void _Init()
        {
            _S5 = _S5 ?? new Shopify_Prices();
            _Shopify = _Shopify ?? new Shopify_Prices();
        }
        public void Init(Shopify_Prices_Pair shopify_Prices_Pair)
        {
            _Init(shopify_Prices_Pair);
        }

    }
}
