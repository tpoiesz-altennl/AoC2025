using System.Diagnostics.CodeAnalysis;

namespace AoC2025.Utilities
{
    public class Grid
    {
        public enum Direction : ushort
        {
            NorthWest = 0,
            North,
            NorthEast,
            West,
            East,
            SouthWest,
            South,
            SouthEast
        }

        public static Direction GetOppositeDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.NorthWest:
                    return Direction.SouthEast;
                case Direction.North:
                    return Direction.South;
                case Direction.NorthEast:
                    return Direction.SouthWest;
                case Direction.West:
                    return Direction.East;
                case Direction.East:
                    return Direction.West;
                case Direction.SouthWest:
                    return Direction.NorthEast;
                case Direction.South:
                    return Direction.North;
                case Direction.SouthEast:
                    return Direction.NorthWest;
                default:
                    throw new ArgumentException($"Invalid direction: {dir}");
            }
        }

        public class Coord : Tuple<int, int>
        {
            public Coord(int row, int col) : base(row, col)
            {
            }

            public Coord(Vec2 v) : base((int)v.Y, (int)v.X)
            {
            }

            public static implicit operator Coord(Vec2 v) => new Coord(v);

            public int Row => (int)this.Item1;
            public int Col => (int)this.Item2;

            public override bool Equals([NotNullWhen(true)] object? obj)
            {
                if (obj is null)
                    return false;
                return typeof(Coord) == obj.GetType() && this.Row == ((Coord)obj).Row && this.Col == ((Coord)obj).Col;
            }

            // Compiler complains if we don't override this
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public static bool operator ==(Coord? lhs, Coord? rhs)
            {
                if (lhs is null)
                    return rhs is null;
                return lhs.Equals(rhs);
            }
            public static bool operator !=(Coord? lhs, Coord? rhs)
            {
                return !(lhs == rhs);
            }

            public Coord Next(Grid.Direction d)
            {
                switch (d)
                {
                    case Direction.NorthWest:
                        return new Coord(Row - 1, Col - 1);
                    case Direction.North:
                        return new Coord(Row - 1, Col);
                    case Direction.NorthEast:
                        return new Coord(Row - 1, Col + 1);
                    case Direction.West:
                        return new Coord(Row, Col - 1);
                    case Direction.East:
                        return new Coord(Row, Col + 1);
                    case Direction.SouthWest:
                        return new Coord(Row + 1, Col - 1);
                    case Direction.South:
                        return new Coord(Row + 1, Col);
                    case Direction.SouthEast:
                        return new Coord(Row + 1, Col + 1);
                    default:
                        return new Coord(-1, -1);
                }
            }
        }

        private List<string> _grid;
        public Grid(List<string> grid)
        {
            _grid = grid;
        }
        public Grid(Grid grid)
        {
            _grid = [];
            foreach (string row in grid._grid)
            {
                _grid.Add(new string(row));
            }
        }

        public string this[int row]
        {
            get { return _grid[row]; }
            set { _grid[row] = value; }
        }

        public char? Get(Coord c)
        {
            if (!IsValidIndex(c))
                return null;
            return _grid[c.Row][c.Col];
        }
        public void Set(int row, string value)
        {
            _grid[row] = value;
        }
        public void Set(Coord c, char value)
        {
            char[] r = _grid[c.Row].ToCharArray();
            r[c.Col] = value;
            _grid[c.Row] = new string(r);
        }
        public int Width()
        {
            if (_grid.Count == 0)
                return 0;
            else
                return _grid[0].Length;
        }
        public int Height()
        {
            return _grid.Count;
        }

        public void AddRow(string row)
        {
            _grid.Add(row);
        }
        public void AddColumn(string column)
        {
            if (column.Length != _grid.Count)
                return;
            for (int i = 0; i < _grid.Count; ++i)
            {
                _grid[i] += column[i];
            }
        }

        public bool IsValidIndex(Coord c)
        {
            return c.Row >= 0 && c.Row < _grid.Count && c.Col >= 0 && c.Col < _grid[c.Row].Length;
        }

        public char? Next(Coord c, Direction d)
        {
            int row = c.Row, col = c.Col;
            switch (d)
            {
                case Direction.NorthWest:
                    row -= 1; col -= 1; break;
                case Direction.North:
                    row -= 1; break;
                case Direction.NorthEast:
                    row -= 1; col += 1; break;
                case Direction.West:
                    col -= 1; break;
                case Direction.East:
                    col += 1; break;
                case Direction.SouthWest:
                    row += 1; col -= 1; break;
                case Direction.South:
                    row += 1; break;
                case Direction.SouthEast:
                    row += 1; col += 1; break;
                default:
                    return null;
            }
            if (IsValidIndex(new Coord(row, col)))
                return _grid[row][col];
            else
                return null;
        }

        public bool GetFirstOccurrence(ref Coord c, char search)
        {
            for (int row = 0; row < _grid.Count; ++row)
            {
                int col = _grid[row].IndexOf(search);
                if (col != -1)
                {
                    c = new Coord(row, col);
                    return true;
                }
            }
            return false;
        }
    }
}
