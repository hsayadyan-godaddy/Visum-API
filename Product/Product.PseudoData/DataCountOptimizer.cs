using Product.PseudoData.Abstraction;
using System;
using System.Collections.Generic;

namespace Product.PseudoData
{
    public class DataCountOptimizer
    {
        #region publics

        public List<T> OptimizeCount<T>(List<T> data, int avgCount) where T : IDataValue
        {
            var ret = new List<T>();

            var extremesCount = avgCount / 2.0;
            var maxRange = (int)Math.Ceiling(data.Count / extremesCount);

            if (data.Count >= maxRange)
            {
                var pointer = 0;
                while (true)
                {
                    var remains = data.Count - pointer;
                    if (remains > 0)
                    {
                        var count = remains > maxRange ? maxRange : remains;
                        var portion = data.GetRange(pointer, count);
                        var tmp = GetExtremes(portion);

                        ret.AddRange(tmp);

                        pointer += count;
                    }
                    else
                    {
                        break;
                    }
                }

                while (ret.Count > avgCount)
                {
                    ret.RemoveAt(0);
                }
            }
            else
            {
                ret = data;
            }

            return ret;
        }

        #endregion

        #region privates

        private List<T> GetExtremes<T>(List<T> value) where T : IDataValue
        {
            var ret = new List<T>();

            (int, double) min = (-1, double.MaxValue);
            (int, double) max = (-1, double.MinValue);

            for (int i = 0; i < value.Count; i++)
            {
                var itm = value[i];

                if (min.Item2 > itm.DataValue)
                {
                    min = (i, itm.DataValue);
                }

                if (max.Item2 < itm.DataValue)
                {
                    max = (i, itm.DataValue);
                }
            }

            if (max.Item2 != min.Item2)
            {
                if (max.Item1 < min.Item1)
                {
                    if (max.Item1 >= 0)
                    {
                        ret.Add(value[max.Item1]);
                    }

                    if (min.Item1 >= 0)
                    {
                        ret.Add(value[min.Item1]);
                    }
                }
                else
                {
                    if (min.Item1 >= 0)
                    {
                        ret.Add(value[min.Item1]);
                    }

                    if (max.Item1 >= 0)
                    {
                        ret.Add(value[max.Item1]);
                    }
                }
            }
            else
            {
                if (max.Item1 >= 0)
                {
                    ret.Add(value[max.Item1]);
                }
                else if (min.Item1 >= 0)
                {
                    ret.Add(value[min.Item1]);
                }
            }

            return ret;
        }

        #endregion
    }
}
