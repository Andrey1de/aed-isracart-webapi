using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AedIsracartWebApi.Operations
{
    /// <summary>
    /// This class is stateless with static functions and safe thread operations
    /// </summary>
    public class OperationFactory
    {
        public static readonly ConcurrentDictionary<string, IOperation> DictOper = new ConcurrentDictionary<string, IOperation>();
        /// <summary>
        /// Static Initialization of known operations
        /// </summary>
        static  OperationFactory()
        {
             Initialize();
        }
        /// <summary>
        /// public function propagated by interface for future usage
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static bool AddOperation(string sign, Func<double, double, double> fn)
        {
            return DictOper.TryAdd(sign,new Operation(sign, fn));
        }
     
        /// <summary>
        /// Static iniatization of simple arythmetic operations
        /// </summary>
        static void Initialize()
        {
                AddOperation("+", (x, y) => x + y);
                AddOperation("-", (x, y) => x - y);
                AddOperation("*", (x, y) => x * y);
                AddOperation("/", (x, y) => x / y);
                AddOperation("%", (x, y) => x % y);

        }

        /// <summary>
        /// Get operation by sign
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static IOperation GetOperaton(string sign)
        {
            IOperation op = null;
            return (DictOper.TryGetValue(sign, out op) ? op : null);
        }
        /// <summary>
        /// Get Handler function by sign
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static Func<double, double, double> GetFunction(string sign)
        {
            IOperation op;
            return (DictOper.TryGetValue(sign, out op) ? op.Calculate : null);
        }
        public static List<IOperation> Operatons => new List<IOperation>(DictOper.Values);
        /// <summary>
        /// Getter on registrated operation sign
        /// </summary>
        public static List<string> Signs => new List<string>(DictOper.Keys);
      


    }
}
