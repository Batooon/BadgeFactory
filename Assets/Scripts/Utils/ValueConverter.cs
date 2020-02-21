using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

//Developer: Antoshka

internal static class ValueConverter
{
    public static char[] suffixes = new char[] { ' ', 'K', 'M', 'B', 'T', 'q', 'Q', 's', 'S', 'O', 'N', 'd', 'U' };

    public static string ConvertValue(this float origin)
    {
        string number = origin.ToString();
        int length = number.Length;
        int thousands = 0;

        while (origin > 1000)
        {
            origin /= 1000;
            thousands += 1;
        }

        string value = origin.ToString("F", CultureInfo.InstalledUICulture) + suffixes[thousands];

        return value;
    }
}
