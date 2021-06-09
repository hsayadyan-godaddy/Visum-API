using System.Collections.Generic;
using Product.DataModels.Enums;

namespace Product.DataModels.Constants
{
    public static class UnitOfMeasureConstants
    {
        public static IReadOnlyCollection<UnitOfMeasure> UnitOfMeasurements =
            new[]
            {
                new UnitOfMeasure
                {
                    SourceToMeasure = SourceToMeasure.Oil, Label = UnitsConstants.Bmp
                },
                new UnitOfMeasure
                {
                    SourceToMeasure = SourceToMeasure.Water, Label = UnitsConstants.Bmp
                },
                new UnitOfMeasure
                {
                    SourceToMeasure = SourceToMeasure.Gas, Label = UnitsConstants.CuFt
                },
                new UnitOfMeasure
                {
                    SourceToMeasure = SourceToMeasure.Depth, Label = UnitsConstants.Ft
                },
                new UnitOfMeasure
                {
                    SourceToMeasure = SourceToMeasure.Pressure, Label = UnitsConstants.Psi
                }
            };
    }
}