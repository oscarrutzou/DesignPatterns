using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public interface IMovementPattern
    {
        public void Move();
        public void OnRelease();
    }
}
