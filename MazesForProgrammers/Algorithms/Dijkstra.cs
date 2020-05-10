using System;
using System.Collections.Generic;
using System.Linq;
using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Algorithms
{
    public class Dijkstra : ISolveMaze
    {
        private readonly Cell root;
        private readonly Lazy<Distances> distanceFactory;

        public Dijkstra(Cell root)
        {
            this.root = root;
            distanceFactory = new Lazy<Distances>(GenerateDistances);
        }

        public Distances Distances
        {
            get
            {
                return distanceFactory.Value;
            }
        }

        public CellDistance Max => Distances.Max;

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
            var distances = new Distances(root);
            var frontier = new List<Cell>() { root };
            var nextFrontier = new List<Cell>();

            while (frontier.Any())
            {
                nextFrontier.Clear();
                foreach (var cell in frontier)
                {
                    foreach (var linked in cell.Links)
                    {
                        if (distances[linked] is null)
                        {
                            distances[linked] = distances[cell].Value + 1;
                            nextFrontier.Add(linked);
                        }
                    }
                }

                // Copy the list with `ToList` calling `=` just updates the refernces.
                frontier = nextFrontier.ToList();
            }

            return distances;
        }
    }
}
