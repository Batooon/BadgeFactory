using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

//Developer: Antoshka

internal static class ValueConverter
{
    public static char[] suffixes = new char[] { ' ', 'K', 'M', 'B', 'T', 'q', 'Q', 's', 'S', 'O', 'N', 'd', 'U' };

    public static string ConvertValue(this int origin)
    {
        float number = origin;
        int thousands = 0;

        while (number > 1000)
        {
            number /= 1000;
            thousands += 1;
        }

        string value = number.ToString("F", CultureInfo.InstalledUICulture) + suffixes[thousands];

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

        string value = number.ToString("F", CultureInfo.InstalledUICulture) + suffixes[thousands];

        return value;
    }
}
