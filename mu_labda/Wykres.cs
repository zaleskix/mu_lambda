using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using mu_labda.algorithm;

namespace mu_labda
{
    public partial class Wykres : Form
    {
        private static Label info = new Label();

        private static bool _dataChanged;
        private static bool _algorithmInProgress;
        private static bool _algorithmFinished;

        private readonly IContainer components = null;
        private Chart mainChart = new Chart();

        public Wykres()
        {
            Algorithm.CreateFirstData();
            CreateChart();
        }

        public static void NotifyNewDataCalculated(int iteration, double maxFVal)
        {
            info.Text = "Algorytm pracuje. \nTrwa iteracja numer: " + iteration +
                        "\nNajlepsza wartosc funkcji wynosi: " + Math.Round(maxFVal, 3);
            _dataChanged = true;
            _algorithmInProgress = false;
        }

        public static void NotifyAlgorithmFinished(int iteration, double maxFVal)
        {
            _algorithmFinished = true;
            info.Text = "Algorytm zakończył działanie. \nLiczba iteracji: " + iteration +
                        "\nNajlepsza wartosc funkcji wynosi: " + Math.Round(maxFVal, 3);
        }
    }
}