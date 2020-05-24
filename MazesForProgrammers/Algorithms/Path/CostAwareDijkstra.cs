using System;
using System.Collections.Generic;
using System.Linq;
using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Algorithms
{
    /// <summary>
    /// An algorithm for finding the shortest paths between nodes in a graph.
    /// </summary>
    public class CostAwareDijkstra : ISolveMaze
    {
        private readonly WeightedCell root;
        private readonly Lazy<Distances> distanceFactory;

        public CostAwareDijkstra(WeightedCell root)
        {
            this.root = root;
            distanceFactory = new Lazy<Distances>(GenerateDistances);
        }

        /// <summary>
        /// Gets the distances to each cell from the root cell.
        /// </summary>
        public Distances Distances
        {
            get
            {
                return distanceFactory.Value;
            }
        }

        /// <summary>
        /// Gets the farthest cell and distance.
        /// </summary>
        public CellDistance Max => Distances.Max;

        /// <summary>
        /// Gets the path from a <paramref name="goal"/> to the root cell.
        /// </summary>
        /// <param name="goal">The goal.</param>
        /// <returns>The distances from the root cell to every cell along a single path to the goal cell.</returns>
        public Distances PathToGoal(Cell goal)
        {
            var current = goal;
            var breadcrumbs = new Distances(goal, Distances[goal].Value);

            do
            {
                foreach (var neighbor in current.Links)
                {
                    if (Distances[neighbor] < Distances[current])
                    {
                        breadcrumbs[neighbor] = Distances[neighbor].Value + 1;
                        current = neighbor;
                        break;
                    }
                }
            } while (current != root);

            return breadcrumbs;
        }

        private Distances GenerateDistances()
        {
            var weights = new Distances(root);
            var pending = new List<WeightedCell> { root };

            while (pending.Any())
            {
                var cell = pending.OrderBy(x => x.Weight).First();
                pending.Remove(cell);

                foreach (var neighbor in cell.Links.Cast<WeightedCell>())
                {
                    var total = weights[cell] + neighbor.Weight;
                    if (weights[neighbor] is null || total < weights[neighbor])
                    {
                        pending.Add(neighbor);
                        weights[neighbor] = total;
                    }
                }
            }

            return weights;
        }
    }
}
