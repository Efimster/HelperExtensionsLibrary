using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Functional
{
    public static class FParamsHelper
    {
        public static FParams<T1> Create<T1>(FParam<T1>? param1)
        {
            return new FParams<T1>(param1);
        }

        public static FParams<T1, T2> Create<T1, T2>(FParam<T1>? param1, FParam<T2>? param2)
        {
            return new FParams<T1, T2>(param1, param2);
        }

        public static FParams<T1, T2, T3> Create<T1, T2, T3>(FParam<T1>? param1, FParam<T2>? param2, FParam<T3>? param3)
        {
            return new FParams<T1, T2, T3>(param1, param2, param3);
        }



    }

    public static class FParamHelper
    {
        public static FParam<T> Create<T>(T param)
        {
            return new FParam<T>() { Value = param };
        }
    }
}
