using cmArt.LibIntegrations;
using cmArt.Reece.ShopifyConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace cmArt.Shopify.App.ReportViews
{

    public class Shopify_Quantities_Pair_Adapter : IShopify_Quantities_Pair_Flat
    {
        private Shopify_Quantities _S5;
        private Shopify_Quantities _Shopify;
        private string AsString(IEnumerable<S5QtyPair> data)
        {
            IEnumerable<S5QtyPair> tmp = data ?? new List<S5QtyPair>();
            tmp = tmp.OrderBy((x) => x.Location);
            IEnumerable<string> Ps = tmp.Select(x => x.Location.ToString() + ":" + x.Qty.ToString());
            string CommaSeparatedList = string.Join(',', Ps);
            return CommaSeparatedList;
        }
        private IEnumerable<S5QtyPair> AsS5QtyPairList(string data)
        {
            string CommaSeparatedList = data ?? string.Empty;
            IEnumerable<string> Ps = CommaSeparatedList.Split(',');
            IEnumerable<S5QtyPair> tmp = Ps.Select(x =>
            {
                string[] tmpPair = x.Split(':');
                short level;
                decimal price;
                short.TryParse(tmpPair[0], out level);
                decimal.TryParse(tmpPair[1], out price);
                return (new S5QtyPair(level, price));
            });
            return tmp;
        }
        public string LeftQuantities
        {
            get
            {
                return AsString(((IShopify_Quantities)_S5).Quantities);
            }

            set
            {
                ((IShopify_Quantities)_S5).Quantities = AsS5QtyPairList(value);
            }
        }

        public string LeftCat { get => ((IShopify_Identity)_S5).Cat; set => ((IShopify_Identity)_S5).Cat = value; }
        public int LeftInvUnique { get => ((IShopify_Identity)_S5).InvUnique; set => ((IShopify_Identity)_S5).InvUnique = value; }
        public string LeftPartNumber { get => ((IShopify_Identity)_S5).PartNumber; set => ((IShopify_Identity)_S5).PartNumber = value; }

        public string RightCat { get => ((IShopify_Identity)_Shopify).Cat; set => ((IShopify_Identity)_Shopify).Cat = value; }
        public int RightInvUnique { get => ((IShopify_Identity)_Shopify).InvUnique; set => ((IShopify_Identity)_Shopify).InvUnique = value; }
        public string RightPartNumber { get => ((IShopify_Identity)_Shopify).PartNumber; set => ((IShopify_Identity)_Shopify).PartNumber = value; }
        public string RightQuantities
        {
            get
            {
                return AsString(((IShopify_Quantities)_Shopify).Quantities);
            }

            set
            {
                ((IShopify_Quantities)_Shopify).Quantities = AsS5QtyPairList(value);
            }
        }


        public Shopify_Quantities_Pair_Adapter()
        {
            _Init();
        }
        public Shopify_Quantities_Pair_Adapter(Generic_Pair<Shopify_Quantities> shopify_Product_Pair)
        {
            _Init(shopify_Product_Pair);
        }
        private void _Init(Generic_Pair<Shopify_Quantities> shopify_Product_Pair)
        {
            _S5 = shopify_Product_Pair.S5 ?? new Shopify_Quantities();
            _Shopify = shopify_Product_Pair.External ?? new Shopify_Quantities();
        }
        private void _Init()
        {
            _S5 = _S5 ?? new Shopify_Quantities();
            _Shopify = _Shopify ?? new Shopify_Quantities();
        }
        public void Init(Generic_Pair<Shopify_Quantities> shopify_Product_Pair)
        {
            _Init(shopify_Product_Pair);
        }

    }

}
