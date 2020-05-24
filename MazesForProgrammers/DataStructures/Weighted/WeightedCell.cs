using System;
using System.Collections.Generic;

using MazesForProgrammers.Algorithms;

namespace MazesForProgrammers.DataStructures
{
    public class WeightedCell : RectangleCell
    {
        private readonly List<WeightedCell> neighbors;
        private readonly Lazy<CostAwareDijkstra> dijkstra;

        public WeightedCell(int row, int column, int weight = 1)
            : base(row, column)
        {
            Weight = weight;
            neighbors = new List<WeightedCell>(4);
            dijkstra = new Lazy<CostAwareDijkstra>(() => new CostAwareDijkstra(this));
        }

        public int Weight { get; set; }

        new public IEnumerable<WeightedCell> Neighbors => neighbors;

        public override Distances Distances
        {
            get
            {
                return dijkstra.Value.Distances;
            }
        }
    }
}

