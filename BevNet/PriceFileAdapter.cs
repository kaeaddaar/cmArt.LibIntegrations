using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public class PriceFileAdapter : Adapter<PriceFile_Clean, IPriceFile, CommonFields, ICommonFields>, ICommonFields
    {
        public PriceFileAdapter()
        {
        }
        public override ICommonFields CopyFrom(ICommonFields From)
        {
            ((ICommonFields)this).CopyFrom(From);
            return this;
        }
        public string SupplierPartNumber
        {
            get
            {
                return _state.PROD_ITEM ?? string.Empty;
            }
            set
            {
                _state.PROD_ITEM = value ?? _state.PROD_ITEM ?? string.Empty;
            }
        }
        public string SupplierName
        {
            get
            {
                return _state.WHOLE_NAME ?? string.Empty;
            }
            set
            {
                _state.WHOLE_NAME = value ?? _state.WHOLE_NAME ?? string.Empty;
            }
        }
        public string SupplierCode
        {
            get
            {
                return _state.WHOLESALER ?? string.Empty;
            }
            set
            {
                _state.WHOLESALER = value ?? _state.WHOLESALER ?? string.Empty;
            }
        }
        public decimal WholesaleCost
        {
            get
            {
                return _state.BESTBOT;
            }
            set
            {
                _state.BESTBOT = value;
            }
        }
        public decimal PriceSchedule1_MSRP // MSRP
        {
            get
            {
                return _state.FRONT_NYC;
            }
            set
            {
                _state.FRONT_NYC = value;
            }
        }
        public decimal PriceSchedule2_MinPrice // minimum price
        {
            get
            {
                return _state.BOT_PRICE;
            }
            set
            {
                _state.BOT_PRICE = value;
            }
        }

    }
}
