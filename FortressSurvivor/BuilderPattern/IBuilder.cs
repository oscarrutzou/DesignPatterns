using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public interface IBuilder
    {
        public void BuildGameObject();
        public GameObject GetResult();
    }
}
