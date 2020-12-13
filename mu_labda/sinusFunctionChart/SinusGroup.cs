using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace mu_labda
{
    public class SinusGroup
    {
        private List<My3DPoint> points;
        private readonly Series _seria;
        
        
        public SinusGroup(int groupId, Color groupColor)
        {
            this.points = new List<My3DPoint>();
            this._seria = new Series
            {
                Color = groupColor,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Point,
                MarkerSize = 15
            };
        }
        
        public void AddPoint(My3DPoint point)
        {
            this.points.Add(point);
        }

        public void ClearPoints()
        {
            this.points.Clear();
        }

        public Series GetGroupSeries()
        {
            return this._seria;
        }

        public List<My3DPoint> GetPoints()
        {
            return this.points;
        }
    }
}