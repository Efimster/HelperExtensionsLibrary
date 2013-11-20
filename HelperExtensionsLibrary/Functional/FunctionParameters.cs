using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Functional
{
    public abstract class FParams : IEquatable<FParams>
    {
        public abstract bool Equals(FParams other);

        public override bool Equals(object obj)
        {
            return Equals(obj as FParams);
        }

        public override int GetHashCode()
        {
            throw new NotSupportedException("should be implemented in derived class");
        }

    }

    public class FParams<T1> : FParams, IEquatable<FParams<T1>>, IEquatable<FParams>
    {
        protected FParam<T1>? FParam1 { get; set; }
        public T1 Param1
        {
            get { return FParam1.HasValue ? FParam1.Value.Value : default(T1); }
        }

        public FParams(FParam<T1>? param1)
        {
            FParam1 = param1;
        }


        public override bool Equals(FParams other)
        {
            return Equals(other as FParams<T1>);
        }

        public bool Equals(FParams<T1> other)
        {
            if (other == null)
                return false;

            return FParam1.Equals(other.FParam1);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(FParam1.HasValue ? FParam1.GetHashCode() : 0).GetHashCode();
        }


        public override string ToString()
        {
            return FParam1.HasValue ? FParam1.Value.Value.ToString() : "_";
        }
    }

    public class FParams<T1, T2> : FParams<T1>, IEquatable<FParams<T1, T2>>
    {
        protected FParam<T2>? FParam2 { get; set; }
        public T2 Param2
        {
            get { return FParam2.HasValue ? FParam2.Value.Value : default(T2); }
        }

        public FParams(FParam<T1>? param1, FParam<T2>? param2)
            : base(param1)
        {
            FParam2 = param2;
        }

        public override bool Equals(FParams other)
        {
            return Equals(other as FParams<T1, T2>);
        }

        public bool Equals(FParams<T1, T2> other)
        {
            if (other == null)
                return false;

            return FParam2.Equals(other.FParam2);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(base.GetHashCode(), (FParam2.HasValue ? FParam2.GetHashCode() : 0)).GetHashCode();
        }

        public override string ToString()
        {
            return string.Concat(base.ToString(), " ", (FParam2.HasValue ? FParam2.Value.Value.ToString() : "_"));
        }
    }

    public class FParams<T1, T2, T3> : FParams<T1, T2>, IEquatable<FParams<T1, T2, T3>>
    {
        protected FParam<T3>? FParam3 { get; set; }
        public T3 Param3
        {
            get { return FParam3.HasValue ? FParam3.Value.Value : default(T3); }
        }


        public FParams(FParam<T1>? param1, FParam<T2>? param2, FParam<T3>? param3)
            : base(param1, param2)
        {
            FParam3 = param3;
        }

        public override bool Equals(FParams other)
        {
            return Equals(other as FParams<T1, T2, T3>);
        }

        public bool Equals(FParams<T1, T2, T3> other)
        {
            if (other == null)
                return false;

            return FParam3.Equals(other.FParam3);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(base.GetHashCode(), (FParam3.HasValue ? FParam3.GetHashCode() : 0)).GetHashCode();
        }

        public override string ToString()
        {
            return string.Concat(base.ToString(), " ", (FParam3.HasValue ? FParam3.Value.Value.ToString() : "_"));
        }
    }

    public class FParams<T1, T2, T3, T4> : FParams<T1, T2, T3>, IEquatable<FParams<T1, T2, T3, T4>>
    {
        public FParam<T4>? FParam4 { get; protected set; }
        public T4 Param4
        {
            get { return FParam4.HasValue ? FParam4.Value.Value : default(T4); }
        }

        public FParams(FParam<T1>? param1, FParam<T2>? param2, FParam<T3>? param3, FParam<T4>? param4)
            : base(param1, param2, param3)
        {
            FParam4 = param4;
        }

        public override bool Equals(FParams other)
        {
            return Equals(other as FParams<T1, T2, T3, T4>);
        }

        public bool Equals(FParams<T1, T2, T3, T4> other)
        {
            if (other == null)
                return false;

            return FParam4.Equals(other.FParam4);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(base.GetHashCode(), (FParam4.HasValue ? FParam4.GetHashCode() : 0)).GetHashCode();
        }

        public override string ToString()
        {
            return string.Concat(base.ToString(), " ", (FParam4.HasValue ? FParam4.Value.Value.ToString() : "_"));
        }
    }

    public struct FParam<T> : IEquatable<FParam<T>>, IEquatable<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }

        public override bool Equals(object obj)
        {
            return Equals((FParam<T>)obj);
        }

        public bool Equals(T other)
        {
            if (other == null)
                return Value == null;

            return Value.Equals(other);
        }

        public bool Equals(FParam<T> other)
        {
            return Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

    }




}
