using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public class PriceFileAdapter : ICommonFields
    {
        private IPriceFile _PriceFile;
        public PriceFileAdapter(IPriceFile PriceFile)
        {
            _PriceFile = PriceFile ?? (IPriceFile)(new PriceFile());
        }

        public string SupplierName 
        {
            get 
            {
                return _PriceFile.WHOLE_NAME ?? string.Empty;
            }
            set
            {
                _PriceFile.WHOLE_NAME = value ?? _PriceFile.WHOLE_NAME ?? string.Empty;
            }
        }
        public string SupplierCode 
        {
            get
            {
                return _PriceFile.WHOLESALER ?? string.Empty;
            }
            set
            {
                _PriceFile.WHOLESALER = value ?? _PriceFile.WHOLESALER ?? string.Empty;
            } 
        }
        public decimal WholesaleCost 
        {
            get
            {
                return _PriceFile.BESTBOT;
            }
            set
            {
                _PriceFile.BESTBOT = value;
            } 
        }
        public decimal PriceSchedule1_MSRP // MSRP
        { 
            get
            {
                return _PriceFile.FRONT_NYC;
            }
            set
            {
                _PriceFile.FRONT_NYC = value;
            } 
        }
        public decimal PriceSchedule2_MinPrice // minimum price
        {
            get
            {
                return _PriceFile.BOT_PRICE;
            }
            set
            {
                _PriceFile.BOT_PRICE = value;
            }
        }
    }
}
