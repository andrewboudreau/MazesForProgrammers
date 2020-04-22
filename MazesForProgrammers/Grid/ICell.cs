namespace MazesForProgrammers.Grid
{
    public interface ICell<T>
    {
        int X { get; }

        int Y { get; }

        (int X, int Y) Location => (X, Y);

        T Data { get; }

        (int X, int Y, T Data) Item => (X, Y, Data);
    }
}
