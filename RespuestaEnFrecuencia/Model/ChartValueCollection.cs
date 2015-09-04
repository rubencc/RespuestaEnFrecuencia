using Microsoft.Research.DynamicDataDisplay.Common;

namespace RespuestaEnFrecuencia.Model
{
    public class ChartValueCollection : RingArray<ChartValuesResult>
    {
        private const int totalPoints = 1000;

        public ChartValueCollection()
            : base(totalPoints)
        {
        }
    }
}
