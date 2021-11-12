﻿using cmArt.System5.Data;
using cmArt.System5.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArt.WebJaguar.Data
{
    public class adapterS5_from_InvAss : IS5_CommonFields_In_WJ
    {
        private IS5InvAssembled _InvAss;
        public adapterS5_from_InvAss()
        {
            _InvAss = _InvAss ?? new S5InvAssembled();
        }

        public IEnumerable<string> barcodes
        {
            get
            {
                IEnumerable<string> result;
                result = _InvAss.AltSuplies_PerInventry_27.Select(alt => alt.PartNumber.TrimEnd());
                return result;
            }
            set
            {
                throw new NotImplementedException("need more than just a string to build these records, not sure if it can be done.");
            }
        }
        public string Description
        {
            get
            {
                return _InvAss.Inv.Description;
            }
            set
            {
                _InvAss.Inv.Description = value ?? string.Empty;
            }
        }
        public int InvUnique
        {
            get
            {
                return _InvAss.Inv.InvUnique;
            }
            set
            {
                _InvAss.Inv.InvUnique = value;
            }
        }
        public string PartNumber
        {
            get
            {
                return _InvAss.Inv.Part;
            }
            set
            {
                _InvAss.Inv.Part = value ?? string.Empty;
            }
        }
        public string WebDescription 
        {
            get => string.Empty; 
            set => throw new NotImplementedException(); 
        }
        public float weight
        {
            get
            {
                return _InvAss.Inv.Weight;
            }
            set
            {
                _InvAss.Inv.Weight = value;
            }
        }

        public string Cat
        {
            get { return _InvAss.Inv.Cat ?? "000"; }
            set { _InvAss.Inv.Cat = value ?? "000"; }
        }
        public string FF22
        {
            get 
            {
                IComments tmp = _InvAss.CommentsLines_PerInventry_27.Where(x => x.LineNo == 22).FirstOrDefault() ?? new Comments();
                return tmp.Comment ?? string.Empty; 
            }
            set 
            {
                IComments tmp = _InvAss.CommentsLines_PerInventry_27.Where(x => x.LineNo == 22).FirstOrDefault() ?? new Comments();
                tmp.Comment = value ?? string.Empty;
            }
        }

        public void init(IS5InvAssembled data)
        {
            _InvAss = data ?? new S5InvAssembled();
        }

    }
}
