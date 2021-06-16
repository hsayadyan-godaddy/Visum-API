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
                    SourceToMeasure = SourceType.Oil, Label = UnitsConstants.Bmp
                },
                new UnitOfMeasure
                {
                    SourceToMeasure = SourceType.Water, Label = UnitsConstants.Bmp
                },
                new UnitOfMeasure
                {
                    SourceToMeasure = SourceType.Gas, Label = UnitsConstants.CuFt
                },
                new UnitOfMeasure
                {
                    SourceToMeasure = SourceType.Depth, Label = UnitsConstants.Ft
                },
                new UnitOfMeasure
                {
                    SourceToMeasure = SourceType.Pressure, Label = UnitsConstants.Psi
                }
            };
    }
}