using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using mu_labda.algorithm;

namespace mu_labda
{
    partial class Wykres
    {
        void timer_Tick(object sender, EventArgs e)
        {
            if (!_algorithmFinished)
            {
                if (_dataChanged)
                {
                    foreach (Series series in this.mainChart.Series.Where(s => s.Name.Equals("AlgorithmSeries")))
                    {
                        series.Points.Clear();
                    }
                    
                    _dataChanged = false;
                    UpdateAlgorythmData();
                    mainChart.Update();
                }
                else if (!_algorithmInProgress)
                {
                    _algorithmInProgress = true;
                    Algorithm.RunAlgorithm();
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

         private void CreateChart()
        {
            
            ChartArea chartArea = new ChartArea();
            Legend legend = new Legend();
            this.mainChart = new Chart();
            Wykres.info = new Label();
            mainChart.BeginInit();
            this.SuspendLayout();
            
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisX.Maximum = 100;
            chartArea.AxisY.Maximum = 100;

            //
            // Konfiguracja wykresu glownego
            //
            mainChart.Name = "Wykres";
            this.mainChart.ChartAreas.Add(chartArea);
            legend.Name = "Legenda";
            this.mainChart.Legends.Add(legend);
            this.mainChart.Location = new Point(100, 50);
            this.mainChart.Name = "Wykres";
            this.mainChart.Size = new Size(1000, 600);
            this.mainChart.TabIndex = 0;
            this.mainChart.Text = "Wykres glowny";

            // 
            // Konfiguracja informacji
            // 
            Wykres.info.Location = new Point(200, 700);
            Wykres.info.Name = "Info";
            Wykres.info.Size = new Size(1000, 100);
            Wykres.info.TabIndex = 0;
            Wykres.info.Text = "Algorytm rozpoczyna pracę. \nTrwa iteracja numer: [algorithm_not_started_yet]\nNajlepsza wartosc funkcji wynosi: [algorithm_not_started_yet]";
            Font font = new Font(FontFamily.GenericMonospace, 20, FontStyle.Bold);
            Wykres.info.Font = font;
            
            //
            // Wykres
            //
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 800);
            this.Controls.Add(this.mainChart);
            this.Controls.Add(Wykres.info);
            this.Text = "Wykres";
            this.Name = "Wykres";
            this.Load += new EventHandler(this.ReadChart);
            mainChart.EndInit();
            this.ResumeLayout(true);
        }
         
         private void ReadChart(object sender, EventArgs e)
         {
             mainChart.Series.Clear();
             PrintSinusFunctionChart();
             UpdateAlgorythmData();
             
             mainChart.Invalidate();
             
             Timer timer = new Timer() { Interval = 1000};
             timer.Tick += timer_Tick;
             timer.Start();
         }

         private void PrintSinusFunctionChart()
         {
             SinusFunctionChartLogic.ReadSinFunction();
             SinusFunctionChartLogic.CalculateSinusGroups();
             foreach (SinusGroup group in SinusFunctionChartLogic.SinusGroups)
             {
                 this.mainChart.Series.Add(group.GetGroupSeries());
                 foreach (MyPoint3D point in group.GetPoints())
                 {
                     group.GetGroupSeries().Points.AddXY(point.GetX(), point.GetY());
                 }
             }
             this.mainChart.Series.Add(Algorithm.GetSeries());
         }
         
         private void UpdateAlgorythmData()
         {
             foreach (MyPoint2D point in Algorithm.GetData())
             {
                 Algorithm.GetSeries().Points.AddXY(point.GetX(), point.GetY());
             }
         }
         
    }
}

