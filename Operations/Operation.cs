using System;

namespace AedIsracartWebApi.Operations
{
    /// <summary>
    ///  Interface for  store handler of single operation
    /// </summary>
    public interface IOperation
    {
        string Sign { get; }
        double Calculate(double x, double y);
    }
  
    /// <summary>
    ///  Class to store handler of single operation
    /// </summary>
    public class Operation : IOperation
    {
        /// <summary>
        /// HAndler of operation (x,y)=>res
        /// </summary>
        readonly Func<double, double, double> Fn;
        public string Sign { get; init; }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="fn"></param>
        public Operation(string sign, Func<double,double,double> fn)
        {
            Fn = fn;
            Sign = sign;
        }
        /// <summary>
        /// Calculates the result by args
        /// </summary>
        /// <param name="x">arg</param>
        /// <param name="y">arg</param>
        /// <returns>res</returns>
        public double Calculate(double x, double y) => Fn(x, y);
      
    }
}
