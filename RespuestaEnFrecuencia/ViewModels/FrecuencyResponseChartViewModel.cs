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

        public CommandBase CalculateCommand { get; private set; }

        private Action CalculateFrecuencyResponse()
        {
            return RequestCalculate;
        }

        private void RequestCalculate()
        {
            double[] input = this.InitialiceInput();
          
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

        private double[] InitialiceInput()
        {
            List<double> list = new List<double>();
            double value = -10.0;

            for (int i = -40; i < 40; i++)
            {
                value += 0.25;
                list.Add(value);
            }

            return list.ToArray();
        }
    }
}
