/**
 * Licensed under a Creative Commons Attribution 4.0 International license (http://creativecommons.org/licenses/by/4.0/)
 * Code by Sanjay - https://sharpsnippets.wordpress.com/2014/03/20/c-extension-random-pastel-colors/
 *                  https://github.com/sanjayatpilcrow/SharpSnippets
 */

using System;
using System.Drawing;

namespace XCOM2Launcher.Mod
{
    public static class ColorExtensions
    {
        private static readonly Random randomizer = new Random((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        /// <summary>
        /// Returns a pastel shade of the color
        /// </summary>
        /// <param name="source">Source  color</param>
        /// <returns></returns>
        public static Color GetPastelShade(this Color source)
        {
            return (generateColor(source, true, new HSB { H = 0, S = 0.2d, B = 255 }, new HSB { H = 360, S = 0.5d, B = 255 }));
        }
        /// <summary>
        /// Returns a random color
        /// </summary>
        /// <param name="source">Ignored(Use RandomShade to get a shade of given color)</param>
        /// <returns></returns>
        public static Color GetRandom(this Color source)
        {
            return (generateColor(source, false, new HSB { H = 0, S = 0, B = 0 }, new HSB { H = 360, S = 1, B = 255 }));
        }
        /// <summary>
        /// Returns a random color within a brightness boundry
        /// </summary>
        /// <param name="source">Ignored (Use GetRandomShade to get a random shade of the color)</param>
        /// <param name="minBrightness">A valued from 0.0 to 1.0, 0 is darkest and 1 is lightest</param>
        /// <param name="maxBrightness">A valued from 0.0 to 1.0</param>
        /// <returns></returns>
        public static Color GetRandom(this Color source, double minBrightness, double maxBrightness)
        {
            if (minBrightness >= 0 && maxBrightness <= 1)
            {
                return (generateColor(source, false, new HSB { H = 0, S = 1 * minBrightness, B = 255 }, new HSB { H = 360, S = 1 * maxBrightness, B = 255 }));
            }
            throw new ArgumentOutOfRangeException();
        }
        /// <summary>
        /// Returns a random shade of the color
        /// </summary>
        /// <param name="source">Base color for the returned shade</param>
        /// <returns></returns>
        public static Color GetRandomShade(this Color source)
        {
            return (generateColor(source, true, new HSB { H = 0, S = 1, B = 0 }, new HSB { H = 360, S = 1, B = 255 }));
        }
        /// <summary>
        /// Returns a random color within a brightness boundry
        /// </summary>
        /// <param name="source">Base color for the returned shade</param>
        /// <param name="minBrightness">A valued from 0.0 to 1.0, 0 is brightest and 1 is lightest</param>
        /// <param name="maxBrightness">A valued from 0.0 to 1.0</param>
        /// <returns></returns>
        public static Color GetRandomShade(this Color source, double minBrightness, double maxBrightness)
        {
            if (minBrightness >= 0 && maxBrightness <= 1)
            {
                return (generateColor(source, true, new HSB { H = 0, S = 1 * minBrightness, B = 255 }, new HSB { H = 360, S = 1 * maxBrightness, B = 255 }));
            }

            throw new ArgumentOutOfRangeException();
        }
        /// <summary>
        /// Process parameters and returns a color
        /// </summary>
        /// <param name="source">Color source</param>
        /// <param name="isaShadeOfSource">Should source be used to generate the new color</param>
        /// <param name="min">Minimum range for HSB</param>
        /// <param name="max">Maximum range for HSB</param>
        /// <returns></returns>
        private static Color generateColor(Color source, bool isaShadeOfSource, HSB min, HSB max)
        {
            var hsbValues = ConvertToHSB(new RGB { R = source.R, G = source.G, B = source.B });
            var h_double = randomizer.NextDouble();
            var b_double = randomizer.NextDouble();

            if (Math.Abs(max.B - min.B) < double.Epsilon)
                b_double = 0; //do not change Brightness

            if (isaShadeOfSource)
            {
                min.H = hsbValues.H;
                max.H = hsbValues.H;
                h_double = 0;
            }
            hsbValues = new HSB
            {
                H = Convert.ToDouble(randomizer.Next(Convert.ToInt32(min.H), Convert.ToInt32(max.H))) + h_double,
                S = Convert.ToDouble((randomizer.Next(Convert.ToInt32(min.S * 100), Convert.ToInt32(max.S * 100))) / 100d),
                B = Convert.ToDouble(randomizer.Next(Convert.ToInt32(min.B), Convert.ToInt32(max.B))) + b_double
            };

            var rgbvalues = ConvertToRGB(hsbValues);

            return Color.FromArgb(source.A, (byte)rgbvalues.R, (byte)rgbvalues.G, (byte)rgbvalues.B);
        }

        public static Color GetContrast(this Color Source, bool PreserveOpacity)
        {
            var inputColor = Source;
            //if RGB values are close to each other by a diff less than 10%, 
            // then if RGB values are lighter side, decrease the blue by 50% (eventually it will increase in conversion below), 
            // if RGB values are on darker side, decrease yellow by about 50% (it will increase in conversion)
            var avgColorValue = (byte)((Source.R + Source.G + Source.B) / 3);
            var diff_r = Math.Abs(Source.R - avgColorValue);
            var diff_g = Math.Abs(Source.G - avgColorValue);
            var diff_b = Math.Abs(Source.B - avgColorValue);
            if (diff_r < 20 && diff_g < 20 && diff_b < 20) //The color is a shade of gray
            {
                inputColor = avgColorValue < 123 
                           ? Color.FromArgb(Source.A, 220, 230, 50) 
                           : Color.FromArgb(Source.A, 255, 255, 50);
            }
            var sourceAlphaValue = Source.A;
            if (!PreserveOpacity)
            {
                sourceAlphaValue = Math.Max(Source.A, (byte)127); //We don't want contrast color to be more than 50% transparent ever.
            }
            var rgb = new RGB { R = inputColor.R, G = inputColor.G, B = inputColor.B };
            var hsb = ConvertToHSB(rgb);
            hsb.H = hsb.H < 180 ? hsb.H + 180 : hsb.H - 180;
            //_hsb.B = _isColorDark ? 240 : 50; //Added to create dark on light, and light on dark
            rgb = ConvertToRGB(hsb);
            return Color.FromArgb(sourceAlphaValue, (int)rgb.R, (int)rgb.G, (int)rgb.B);
        }

        #region Code from MSDN
        internal static RGB ConvertToRGB(HSB hsb)
        {
            // Following code is taken as it is from MSDN. See link below.
            // By: <a href="http://blogs.msdn.com/b/codefx/archive/2012/02/09/create-a-color-picker-for-windows-phone.aspx" title="MSDN" target="_blank">Yi-Lun Luo</a>
            var chroma = hsb.S * hsb.B;
            var hue2 = hsb.H / 60;
            var x = chroma * (1 - Math.Abs(hue2 % 2 - 1));
            var r1 = 0d;
            var g1 = 0d;
            var b1 = 0d;
            if (hue2 >= 0 && hue2 < 1)
            {
                r1 = chroma;
                g1 = x;
            }
            else if (hue2 >= 1 && hue2 < 2)
            {
                r1 = x;
                g1 = chroma;
            }
            else if (hue2 >= 2 && hue2 < 3)
            {
                g1 = chroma;
                b1 = x;
            }
            else if (hue2 >= 3 && hue2 < 4)
            {
                g1 = x;
                b1 = chroma;
            }
            else if (hue2 >= 4 && hue2 < 5)
            {
                r1 = x;
                b1 = chroma;
            }
            else if (hue2 >= 5 && hue2 <= 6)
            {
                r1 = chroma;
                b1 = x;
            }
            var m = hsb.B - chroma;
            return new RGB()
            {
                R = r1 + m,
                G = g1 + m,
                B = b1 + m
            };
        }
        internal static HSB ConvertToHSB(RGB rgb)
        {
            // Following code is taken as it is from MSDN. See link below.
            // By: <a href="http://blogs.msdn.com/b/codefx/archive/2012/02/09/create-a-color-picker-for-windows-phone.aspx" title="MSDN" target="_blank">Yi-Lun Luo</a>
            var r = rgb.R;
            var g = rgb.G;
            var b = rgb.B;

            var max = Max(r, g, b);
            var min = Min(r, g, b);
            var chroma = max - min;
            var hue2 = 0d;
            if (Math.Abs(chroma) > double.Epsilon)
            {
                if (Math.Abs(max - r) < double.Epsilon)
                {
                    hue2 = (g - b) / chroma;
                }
                else if (Math.Abs(max - g) < double.Epsilon)
                {
                    hue2 = (b - r) / chroma + 2;
                }
                else
                {
                    hue2 = (r - g) / chroma + 4;
                }
            }
            var hue = hue2 * 60;
            if (hue < 0)
            {
                hue += 360;
            }
            var brightness = max;
            double saturation = 0;
            if (Math.Abs(chroma) > double.Epsilon)
            {
                saturation = chroma / brightness;
            }
            return new HSB()
            {
                H = hue,
                S = saturation,
                B = brightness
            };
        }
        private static double Max(double d1, double d2, double d3)
        {
            return Math.Max(d1 > d2 ? d1 : d2, d3);
        }
        private static double Min(double d1, double d2, double d3)
        {
            return Math.Min(d1 < d2 ? d1 : d2, d3);
        }
        internal struct RGB
        {
            internal double R;
            internal double G;
            internal double B;
        }
        internal struct HSB
        {
            internal double H;
            internal double S;
            internal double B;
        }
        #endregion //Code from MSDN
    }
}
