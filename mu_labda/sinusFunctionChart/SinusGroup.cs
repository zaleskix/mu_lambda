using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace mu_labda
{
    public class SinusGroup
    {
        private readonly Series _seria;
        private readonly List<MyPoint3D> points;


        public SinusGroup(int groupId, Color groupColor)
        {
            points = new List<MyPoint3D>();
            _seria = new Series
            {
                Color = groupColor,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Point,
                MarkerSize = 15
            };
        }

        public void AddPoint(MyPoint3D point)
        {
            points.Add(point);
        }

        public void ClearPoints()
        {
            points.Clear();
        }

        public Series GetGroupSeries()
        {
            return _seria;
        }

        public List<MyPoint3D> GetPoints()
        {
            return points;
        }
    }
}