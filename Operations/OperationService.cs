using System;
using System.Collections.Generic;

namespace AedIsracartWebApi.Operations
{


    //Opreation service did registrated as normar Service of NetCore

    public interface IOperationService
    {
        double TryCalculate(double x, double y, string operation, out string error);
        List<string> Signs { get; }
        List<IOperation> Operations { get; }

        bool AddOperation(string sign, Func<double, double, double> fn);
    }


    public class OperationService : IOperationService
    {
        public List<IOperation> Operations => OperationFactory.Operatons;

        public List<string> Signs => OperationFactory.Signs;

        public bool AddOperation(string sign, Func<double, double, double> fn)
        {
            return OperationFactory.AddOperation(sign, fn);
        }

        public double TryCalculate(double x, double y, string sign , out string error)
        {
            error = "";

            Func<double, double, double>fn = OperationFactory.GetFunction(sign);
            double result = 0;
            try
            {
                if (fn != null)
                {
                    result = fn(x, y);
                }
                else
                {
                    error = $"Unknown operation: { sign}";

                }
            }
            catch (Exception ex)
            {

                error = $"For operation: {sign} was Exception: { ex.Message}";
            }

            return result;
        }
    }
}
