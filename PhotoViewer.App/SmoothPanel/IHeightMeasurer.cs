using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoothPanel
{
    internal interface IHeightMeasurer
    {
        /// <summary>
        /// Получает предполагаемую высоту элемента.
        /// </summary>
        /// <param name="availableWidth"></param>
        /// <returns></returns>
        double GetEstimatedHeight(double availableWidth);
    }
}
