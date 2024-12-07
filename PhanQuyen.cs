using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.HamPhuTro
{
    public class PhanQuyen
    {
        public static PhanQuyen instance;
        public string MaNV { get; set; }
        public string CongViec {  get; set; }


        private PhanQuyen() { }

        public static PhanQuyen Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new PhanQuyen();
                }
                return instance;
            }
        }
    }
}
