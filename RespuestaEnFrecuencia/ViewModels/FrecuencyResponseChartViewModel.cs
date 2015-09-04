using System;
using System.Collections.Generic;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using RespuestaEnFrecuencia.Business;
using RespuestaEnFrecuencia.Common;
using RespuestaEnFrecuencia.Model;

namespace RespuestaEnFrecuencia.ViewModels
{
    public class FrecuencyResponseChartViewModel : ViewModelBase
    {

        public FrecuencyResponseChartViewModel()
        {
            this.CalculateCommand = new CommandBase(CalculateFrecuencyResponse());
            this.range = 1;
            this.minValue = -10.0;
            this.maxValue = 10.0;
        }

        private EnumerableDataSource<ChartValuesResult> data;
        public EnumerableDataSource<ChartValuesResult> Data
        {
            get { return data; }
            set
            {
                data = value;
                NotifyPropertyChanged("Data");
            }
        }

        private double range;
        public double Range
        {
            get { return range; }
            set
            {
                range = value;
                NotifyPropertyChanged("Range");
            }
        }

        private double minValue;
        public double MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                NotifyPropertyChanged("MinValue");
            }
        }

        private double maxValue;
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                NotifyPropertyChanged("MaxValue");
            }
        }

        public CommandBase CalculateCommand { get; private set; }

        private Action CalculateFrecuencyResponse()
        {
            return RequestCalculate;
        }

        private void RequestCalculate()
        {
            double[] input = this.InitialiceInput(this.range, this.minValue, this.maxValue);
          
            FrecuencyResponse calc = new FrecuencyResponse();
            double[] output = calc.Calculate(input);

            ChartValueCollection results = this.CreateChartValues(input, output);

            var dataSource = new EnumerableDataSource<ChartValuesResult>(results);

            dataSource.SetXMapping(x => x.input);
            dataSource.SetYMapping(y => y.dBm);

            this.Data = dataSource;
        }

        private ChartValueCollection CreateChartValues(double[] input, double[] output)
        {
            ChartValueCollection results = new ChartValueCollection();

            for (int i = 0; i < input.Length; i++)
            {
                ChartValuesResult result = new ChartValuesResult()
                {
                    input = input[i],
                    dBm = output[i]
                };

                results.Add(result);
            }

            return results;
        }

        private double[] InitialiceInput(double rgn, double min, double max)
        {
            List<double> list = new List<double>();
            double value = min;

            for (int i = 0; i < max - min; i++)
            {
                value += rgn;
                list.Add(value);
            }

            return list.ToArray();
        }
    }
}
