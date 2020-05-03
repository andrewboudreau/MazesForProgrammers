﻿using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid.Configuration
{
    public interface IConfigureNeighbors
    {
        void ConfigureNeighbors<T>(ICell<T> cell, IGrid<T> grid);
    }
}