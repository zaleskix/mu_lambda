using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace mu_labda
{
    public partial class Wykres : Form
    {

        private readonly IContainer components = null;
        private Chart mainChart = new Chart();
        private static Label info = new Label();
        
        private static bool _dataChanged = false;
        private static bool _algorithmInProgress = false;
        private static bool _algorithmFinished = false;
        
        public Wykres()
        {
            CreateChart();
        }
    }
}