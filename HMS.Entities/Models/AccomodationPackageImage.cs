using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities.Models
{
    public class AccomodationPackageImage
    {
        public int ID { get; set; }
        public int AccomodationPackageID { get; set; }
        public int ImageID { get; set; }
        public virtual Image Image { get; set; }

    }
}
