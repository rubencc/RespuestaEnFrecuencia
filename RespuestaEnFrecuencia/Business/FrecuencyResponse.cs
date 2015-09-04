namespace RespuestaEnFrecuencia.Business
{
    public class FrecuencyResponse
    {
        public double[] Calculate(double[] input)
        {
            double[] output = new double[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = this.CalculateOutput(input[i]);
            }

            return output;
        }

        private double CalculateOutput(double input)
        {              

            if (input > 0)
            {
                return 1;
            }

            if (input < 0)
            {
                return -1;
            }

            return 0;
        }
    }
}
