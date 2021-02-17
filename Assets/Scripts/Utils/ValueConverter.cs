using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using System.Numerics;

//Developer: Antoshka

internal static class ValueConverter
{
    private static char[] _suffixes = new char[] { ' ', 'K', 'M', 'B', 'T', 'q', 'Q', 's', 'S', 'O', 'N', 'd', 'U' };

    public static string ConvertValue(this BigInteger origin)
    {
        int thousands = 0;

        while (origin > 1000)
        {
            origin /= 1000;
            thousands += 1;
        }

        string value = origin.ToString("F", CultureInfo.InstalledUICulture) + _suffixes[thousands];

        return value;
    }

    public static string ConvertValue(this long origin)
    {
        int thousands = 0;

        float number = origin;

        while (number > 1000)
        {
            number /= 1000;
            thousands += 1;
        }

        string value = number.ToString("F", CultureInfo.InstalledUICulture) + _suffixes[thousands];

        return value;
    }
}
