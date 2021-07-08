using Product.PseudoData.Basics;
using System;
using System.Collections.Generic;

namespace Product.PseudoData
{
    public class DataGenerator
    {
        #region members

        private readonly DataSettings _dataSettings;
        private BehaviourType _behaviourType;
        private double _mean;
        private double _stdDev;
        private double _lastMax;
        private double _lastMin;
        private double _stdDevKoef;

        #endregion

        #region ctor

        public DataGenerator(DataSettings dataSettings)
        {
            _dataSettings = dataSettings;
            Reset();
        }

        #endregion

        #region publics

        public double Next()
        {
            switch (_behaviourType)
            {
                case BehaviourType.Up:
                    _mean = _lastMax;
                    _stdDev = _lastMax * _stdDevKoef;
                    break;
                case BehaviourType.Down:
                    _mean = _lastMin;
                    _stdDev = _lastMin * _stdDevKoef;
                    break;
                case BehaviourType.Regular:
                default:
                    break;
            }

            var ret = NextNormal(_mean, _stdDev);


            if (_lastMin > ret)
            {
                _lastMin = ret;
            }

            if (_lastMax < ret)
            {
                _lastMax = ret;
            }

            return ret;
        }

        public List<double> Next(int count)
        {
            var ret = new List<double>();

            while (ret.Count < count)
            {
                var num = Next();
                ret.Add(num);
            }

            return ret;
        }

        public void Reset()
        {
            _behaviourType = _dataSettings.BehaviourType;
            _mean = _dataSettings.Mean;
            _stdDev = _dataSettings.StdDev;
            _lastMin = _lastMax = _dataSettings.Mean;
            _stdDevKoef = _dataSettings.StdDev / _dataSettings.Mean;

            Next();

        }

        #endregion

        #region privates

        private double NextNormal(double mean, double stdDev)
        {
            var rand = new Random();
            var randA = 1.0 - rand.NextDouble();
            var randB = 1.0 - rand.NextDouble();
            var rndNormal
                = Math.Sqrt(-2.0 * Math.Log(randA)) *
                  Math.Sin(2.0 * Math.PI * randB);

            var ret = mean + stdDev * rndNormal;

            return ret > 0 ? ret : -ret;
        }

        #endregion
    }
}
