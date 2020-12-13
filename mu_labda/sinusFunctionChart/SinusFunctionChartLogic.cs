using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace mu_labda
{
    public class SinusFunctionChartLogic
    {
        private static readonly List<My3DPoint> SinusFunction = new List<My3DPoint>();
        public static readonly List<SinusGroup> SinusGroups = new List<SinusGroup>();
        private static readonly List<Color> GroupColors = new List<Color>();

        private static double Function(int x1, int x2)
        {
            double f = Math.Sin(0.05 * x1) + Math.Sin(0.05 * x2) + 0.4 * Math.Sin(x1 * 0.15) * Math.Sin(x2 * 0.15);
            return f;
        }

        public static void ReadSinFunction()
        {
            for (int x1=0; x1 < 100; x1++)
            {
                for (int x2 = 0; x2 < 100; x2++)
                {
                    SinusFunction.Add(new My3DPoint(x1, x2, Function(x1,x2)));
                }
            }
        }

        public static void CalculateSinusGroups()
        {
            CreateSinusGroups();
            
            double sinusFunctionMax = SinusFunction.Max(point => point.GetZ());
            double sinusFunctionMin = SinusFunction.Min(point => point.GetZ());
            double distance = sinusFunctionMax - sinusFunctionMin;

            foreach (My3DPoint point in SinusFunction)
            {
                for (int i = 0; i < 10; i++)
                {
                    double compartmentMin = sinusFunctionMin + i * (distance / 10);
                    double compartmentMax = sinusFunctionMin + (i+1) * (distance/10);

                    if (i == 0)
                    {
                        if (point.GetZ() >= compartmentMin && point.GetZ() <= compartmentMax)
                        {
                            SinusGroups[i].AddPoint(point);
                            break;
                        }
                    }
                    else
                    {
                        if (point.GetZ() > compartmentMin && point.GetZ() <= compartmentMax)
                        {
                            SinusGroups[i].AddPoint(point);
                            break;
                        }
                    }
                }
            }
        }

        private static void CreateSinusGroups()
        {
            CreateGradientPalette();
            
            for (int i = 0; i < 10; i++)
            {
                SinusGroups.Add(new SinusGroup(i + 1, GroupColors[i]));
                Thread.Sleep(200);
            }
        }
        
        private static void CreateGradientPalette()
        {
            GroupColors.Add(Color.FromArgb(186,189,255));
            GroupColors.Add(Color.FromArgb(186,225,255));
            GroupColors.Add(Color.FromArgb(186,254,255));
            GroupColors.Add(Color.FromArgb(186,255,220));
            GroupColors.Add(Color.FromArgb(186,255,201));
            GroupColors.Add(Color.FromArgb(226,255,186));
            GroupColors.Add(Color.FromArgb(255,255,186));
            GroupColors.Add(Color.FromArgb(250, 222, 203));
            GroupColors.Add(Color.FromArgb(247, 208, 213));
            GroupColors.Add(Color.FromArgb(255,179,186));
        }
    }
}