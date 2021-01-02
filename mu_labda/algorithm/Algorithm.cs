using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace mu_labda.algorithm
{
    public static class Algorithm
    {
        private const int LAMBDA = 10;
        private const int MU = 4;
        private const int TOURNAMENT_SIZE = 2;
        private const int MUTATION_RANGE = 10;
        private const int MAX_ITERATIONS = 5;
        
        private static readonly Random RANDOM = new Random();
        private static readonly Series ALGORITHM_SERIES = new Series
        {
            Name = "AlgorithmSeries",
            Color = Color.Black,
            IsVisibleInLegend = false,
            IsXValueIndexed = false,
            ChartType = SeriesChartType.Point,
            MarkerSize = 8
        };
        
        
        private static int currentIteration;
        private static List<MyPoint2D> currentGens = new List<MyPoint2D>();
        
        public static Series GetSeries()
        {
            return ALGORITHM_SERIES;
        }

        public static void RunAlgorithm()
        {
            if (currentIteration >= MAX_ITERATIONS)
            {
                Wykres.NotifyAlgorithmFinished(currentIteration,
                    currentGens.OrderBy(point => point.GetFunctionValue()).First().GetFunctionValue());
                return;
            }

            List<MyPoint2D> nextGens = new List<MyPoint2D>();
            for (int i = 0; i < LAMBDA; i++)
            {
                MyPoint2D nextGenPoint = GetBestCurrentPointUsingTournament();
                nextGens.Add(MutatePoint(nextGenPoint));
            }

            List<MyPoint2D> muAndLambda = new List<MyPoint2D>(currentGens);
            muAndLambda.AddRange(nextGens);
            currentGens = muAndLambda.OrderByDescending(point => point.GetFunctionValue()).Take(MU).ToList();

            Wykres.NotifyNewDataCalculated(currentIteration + 1,
                currentGens.OrderBy(point => point.GetFunctionValue()).First().GetFunctionValue());
            currentIteration++;
        }

        public static void CreateFirstData()
        {
            for (int i = 0; i < MU; i++)
            {
                Thread.Sleep(RANDOM.Next(200, 1500));
                currentGens.Add(new MyPoint2D(RANDOM.Next(0, 100), RANDOM.Next(0, 100)));
            }
        }

        public static List<MyPoint2D> GetData()
        {
            return currentGens;
        }
        
        private static MyPoint2D MutatePoint(MyPoint2D pointToMutate)
        {
            MyPoint2D pointAfterMutate = new MyPoint2D();

            double xAfterMutation = pointToMutate.GetX() + RANDOM.Next(-MUTATION_RANGE, MUTATION_RANGE);
            double yAfterMutation = pointToMutate.GetY() + RANDOM.Next(-MUTATION_RANGE, MUTATION_RANGE);

            pointAfterMutate.SetX(xAfterMutation < 0 ? 0 : xAfterMutation > 100 ? 100 : xAfterMutation);
            pointAfterMutate.SetY(yAfterMutation < 0 ? 0 : yAfterMutation > 100 ? 100 : yAfterMutation);
            pointAfterMutate.SetFunctionValue(SinusFunctionChartLogic.Function(xAfterMutation, yAfterMutation));
            return pointAfterMutate;
        }

        private static MyPoint2D GetBestCurrentPointUsingTournament()
        {
            MyPoint2D bestCurrentGenPoint = new MyPoint2D();

            foreach (MyPoint2D point in GetRandomPointsToTournament())
                if (bestCurrentGenPoint.GetFunctionValue() < point.GetFunctionValue())
                    bestCurrentGenPoint = point;

            return bestCurrentGenPoint;
        }

        private static List<MyPoint2D> GetRandomPointsToTournament()
        {
            List<MyPoint2D> ossTournament = new List<MyPoint2D>();

            for (int i = 0; i < TOURNAMENT_SIZE; i++)
            {
                MyPoint2D randomPoint = currentGens[RANDOM.Next(currentGens.Count)];
                if (ossTournament.Contains(randomPoint))
                {
                    i--;
                    continue;
                }

                ossTournament.Add(randomPoint);
            }

            return ossTournament;
        }
    }
}