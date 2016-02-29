using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StockInfoModel
    {
        public decimal now { get; set; }
        public decimal last { get; set; }
        public decimal bp1 { get; set; }
        public int bc1 { get; set; }
        public decimal bp2 { get; set; }
        public int bc2 { get; set; }
        public decimal bp3 { get; set; }
        public int bc3 { get; set; }
        public decimal bp4 { get; set; }
        public int bc4 { get; set; }
        public decimal bp5 { get; set; }
        public int bc5 { get; set; }
        public decimal sp1 { get; set; }
        public int sc1 { get; set; }
        public decimal sp2 { get; set; }
        public int sc2 { get; set; }
        public decimal sp3 { get; set; }
        public int sc3 { get; set; }
        public decimal sp4 { get; set; }
        public int sc4 { get; set; }
        public decimal sp5 { get; set; }
        public int sc5 { get; set; }
    }
}
