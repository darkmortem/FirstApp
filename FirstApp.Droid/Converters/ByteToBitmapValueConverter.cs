﻿using Android.Graphics;
using Android.Util;
using MvvmCross.Converters;
using System;
using System.Globalization;

namespace FirstApp.Droid.Converters
{
    public class StringToBitmapValueConverter : MvxValueConverter<string, Bitmap>
    {
        protected override Bitmap Convert(string imageString, Type targetType, object parameter, CultureInfo culture)
        {
            if (imageString.Length == default(int))
            {
                return null;
            }

            byte[] imageAsBytes = Base64.Decode(imageString, Base64Flags.Default);

            return BitmapFactory.DecodeByteArray(imageAsBytes, default(int), imageAsBytes.Length);
        }
    }
}
