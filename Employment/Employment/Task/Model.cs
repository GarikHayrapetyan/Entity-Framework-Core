using System;
using System.Collections.Generic;
using System.Text;

namespace Employment.Task
{
    public class Model<T,K>
    {
        public T Key { get; set; }
        public K Arg { get;set; }

        public bool CompareTo(Model<T,K> obj)
        {
            if (Key.Equals(obj.Key) && Arg.Equals(obj.Arg))
                return true;
        
            return false;
        }

        public bool Bigger(Model<T, K> obj)
        {
            int key = int.Parse(obj.Key.ToString());

            if (Key.Equals(obj.Key) && double.Parse(Arg.ToString())<double.Parse(obj.Arg.ToString()))
                return true;

            return false;
        }
    }
}
