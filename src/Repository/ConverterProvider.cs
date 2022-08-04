using System.Collections;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Helpers;

public static class ConverterProvider
{
    public static ValueConverter<bool, BitArray> GetBoolToBitArrayConverter()
    {
        return new ValueConverter<bool, BitArray>(
            value => new BitArray(new[] { value }),
            value => value.Get(0));
    }
}