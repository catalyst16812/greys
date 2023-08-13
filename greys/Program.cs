﻿using greys;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ManagedColorPlayground
{
    using static NativeMethods;

    class Program
    {

        static void Main(string[] args)
        {
            float[] identity = BuiltinMatrices.Identity.Cast<float>().ToArray();
            float[] Negative = BuiltinMatrices.Negative.Cast<float>().ToArray();
            float[] GrayScale = BuiltinMatrices.GrayScale.Cast<float>().ToArray();
            float[] NegativeGrayScale = BuiltinMatrices.NegativeGrayScale.Cast<float>().ToArray();
            float[] Red = BuiltinMatrices.Red.Cast<float>().ToArray();
            float[] NegativeRed = BuiltinMatrices.NegativeRed.Cast<float>().ToArray();
            float[] Sepia = BuiltinMatrices.Sepia.Cast<float>().ToArray();
            float[] NegativeSepia = BuiltinMatrices.NegativeSepia.Cast<float>().ToArray();
            float[] HueShift180 = BuiltinMatrices.HueShift180.Cast<float>().ToArray();
            float[] NegativeHueShift180 = BuiltinMatrices.NegativeHueShift180.Cast<float>().ToArray();

            List<string> colorFilters = new List<string> {
                "Neutral",
                "Negative",
                "GrayScale",
                "NegativeGrayScale",
                "Red",
                "NegativeRed",
                "Sepia",
                "NegativeSepia",
                "HueShift180",
                "NegativeHueShift180"
              };
            colorFilters = colorFilters.ConvertAll(d => d.ToLower());
            Dictionary<string, string> descriptions = new Dictionary<string, string>();
            descriptions.Add(colorFilters[0], "no color transformation");
            descriptions.Add(colorFilters[1], "simple colors transformation.");
            descriptions.Add(colorFilters[2], "simple colors transformation.");
            descriptions.Add(colorFilters[3], "simple colors transformation.");
            descriptions.Add(colorFilters[4], "simple colors transformation.");
            descriptions.Add(colorFilters[5], "simple colors transformation.");
            descriptions.Add(colorFilters[6], "simple colors transformation.");
            descriptions.Add(colorFilters[7], "simple colors transformation.");
            descriptions.Add(colorFilters[8], "simple colors transformation.");
            descriptions.Add(colorFilters[9], "simple colors transformation.");

            foreach (KeyValuePair<string, string> kvp in descriptions)
            {
                Console.WriteLine("Color filter = {0}, description = {1}", kvp.Key, kvp.Value);
            }
            Console.WriteLine("");
            Console.WriteLine("Select a color filter to see elements on the screen better. Press X to exit: ");



            string callSign;
            string exitKey = "x";
            var magEffectInvert = new MAGCOLOREFFECT
            {
                transform = Negative
            };
            while ((callSign = Console.ReadLine().ToLower()) != exitKey)
            {
                if (colorFilters.Contains(callSign))
                {
                    Console.WriteLine($"\"{callSign}\" Color filter set correctly");
                    MagInitialize();
                    switch (callSign)
                    {
                        case "neutral":
                            MagUninitialize();
                            break;
                        case "negative":
                            magEffectInvert = new MAGCOLOREFFECT
                            {
                                transform = Negative
                            };
                            MagSetFullscreenColorEffect(ref magEffectInvert);
                            break;
                        case "grayscale":
                            magEffectInvert = new MAGCOLOREFFECT
                            {
                                transform = GrayScale
                            };
                            MagSetFullscreenColorEffect(ref magEffectInvert);
                            break;
                        case "negativegrayscale":
                            magEffectInvert = new MAGCOLOREFFECT
                            {
                                transform = NegativeGrayScale
                            };
                            MagSetFullscreenColorEffect(ref magEffectInvert);
                            break;
                        case "red":
                            magEffectInvert = new MAGCOLOREFFECT
                            {
                                transform = Red
                            };
                            MagSetFullscreenColorEffect(ref magEffectInvert);
                            break;
                        case "negativered":
                            magEffectInvert = new MAGCOLOREFFECT
                            {
                                transform = NegativeRed
                            };
                            MagSetFullscreenColorEffect(ref magEffectInvert);
                            break;
                        case "sepia":
                            magEffectInvert = new MAGCOLOREFFECT
                            {
                                transform = Sepia
                            };
                            MagSetFullscreenColorEffect(ref magEffectInvert);
                            break;
                        case "negativesepia":
                            magEffectInvert = new MAGCOLOREFFECT
                            {
                                transform = NegativeSepia
                            };
                            MagSetFullscreenColorEffect(ref magEffectInvert);
                            break;
                        case "hueshift180":
                            magEffectInvert = new MAGCOLOREFFECT
                            {
                                transform = HueShift180
                            };
                            MagSetFullscreenColorEffect(ref magEffectInvert);
                            break;
                        case "negativehueshift180":
                            magEffectInvert = new MAGCOLOREFFECT
                            {
                                transform = NegativeHueShift180
                            };
                            MagSetFullscreenColorEffect(ref magEffectInvert);
                            break;
                        default:
                            // code block
                            break;
                    }

                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine($"\"{callSign}\" color filter does not exist in our list");
                }
                Console.WriteLine("");
                Console.WriteLine("Select a color filter to see elements on the screen better. Press X to exit: ");
            }

            Console.ReadLine();
            MagUninitialize();

        }

    }

    static class NativeMethods
    {
        const string Magnification = "Magnification.dll";

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagInitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagUninitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagSetFullscreenColorEffect(ref MAGCOLOREFFECT pEffect);

        public struct MAGCOLOREFFECT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            public float[] transform;
        }
    }
}