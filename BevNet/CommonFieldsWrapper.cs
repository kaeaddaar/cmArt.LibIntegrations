using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.BevNet
{
    public class CommonFieldsWrapper : IPriceFile
    {
        private ICommonFields _CommonFields;

        public CommonFieldsWrapper(ICommonFields CommonFields)
        {
            _CommonFields = CommonFields ?? (ICommonFields)(new CommonFields());
        }
    }
}
