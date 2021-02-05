using System;
using System.Collections.Generic;
using System.Text;

namespace cmArt.LibIntegrations.OdbcService
{
    public static class OdbcDataReaderExtensions
    {
        public static DateTime CDbNull(this object readerItem, DateTime replacementValue)
        {
            if (readerItem is DBNull)
            {
                return replacementValue;
            }
            else
            {
                return (DateTime)readerItem;
            }
        }
    }

}
