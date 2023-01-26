using System.Numerics;
using System.Text.RegularExpressions;

namespace DUCalculator;

public partial class Program
{
    public static void Main(params string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Provide command, a File Path to Read for the first parameter and a start position as the second parameter");
            Console.WriteLine("Ie: path \"c:\\user\\AR_Waypoints.lya\" ::pos{...}");
            Console.WriteLine("Ie: up,down,left,right,forward,back 200000 ::pos{...}");
            return;
        }

        switch (args[0])
        {
            case "path":
                CalculatePath(args);
                break;
            case "directions":
                CalculateDirections(args);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(args));
        }
    }

    private static void CalculateDirections(IEnumerable<string> args)
    {
        var queue = new Queue<string>(args);
        queue.Dequeue();

        var directionVectors = new Dictionary<string, Vector3>
        {
            {"up", Vector3.UnitZ},
            {"down", -Vector3.UnitZ},
            {"left", -Vector3.UnitY},
            {"right", Vector3.UnitY},
            {"forward", Vector3.UnitX},
            {"back", -Vector3.UnitX},
        };
        
        var directions = queue.Dequeue().Split(",", StringSplitOptions.RemoveEmptyEntries);
        if (directions is ["all"])
        {
            directions = directionVectors.Keys.ToArray();
        }
        var distance = float.Parse(queue.Dequeue());
        var position = queue.Dequeue().PositionToVector3();

        var result = new List<PositionEntry>();
        
        foreach (var d in directions)
        {
            var dv = directionVectors[d];
            var v = (dv * distance) + position;

            var pe = new PositionEntry(d.ToUpper(), v);
            
            result.Add(pe);
            
            Console.WriteLine(pe);
        }
    }
    
    private static void CalculatePath(IReadOnlyList<string> args)
    {
        var filePath = args[0];
        var startPosition = args[1].PositionToVector3();

        var lines = File.ReadAllLines(filePath);

        var entries = new HashSet<PositionEntry>();

        foreach (var line in lines)
        {
            var sanitizedLine = line.Replace("\t", string.Empty)
                .Trim();

            var matches = MyRegex().IsMatch(sanitizedLine);

            if (matches)
            {
                var sanitized = line.Replace(" ", string.Empty)
                    .Replace("\t", string.Empty)
                    .Replace("'", string.Empty);
                var pieces = sanitized.Split("=");
                var queue = new Queue<string>(pieces);

                var entry = new PositionEntry(queue.Dequeue(), queue.Dequeue());

                entries.Add(entry);
            }
        }

        var vectorArray = PathFinding.GetShortestPath(
            startPosition,
            entries.Select(x => x.Position).ToArray()
        );

        var orderedEntries = new List<PositionEntry>();

        foreach (var v in vectorArray)
        {
            // will exclude the origin point
            if (!entries.Contains(new PositionEntry(v)))
            {
                continue;
            }

            var entry = entries.Single(x => x == v);
            orderedEntries.Add(entry);

            Console.WriteLine(entry.ToString());
        }
    }

    [GeneratedRegex("^[a0-z9_]+?\\s?=\\s?'(.*?)',")]
    private static partial Regex MyRegex();

    public readonly struct PositionEntry
    {
        public string Name { get; }
        public Vector3 Position { get; }

        public PositionEntry(string name, string position)
        {
            Name = name;
            Position = position.PositionToVector3();
        }

        public PositionEntry(Vector3 position)
        {
            Name = string.Empty;
            Position = position;
        }

        public PositionEntry(string name, Vector3 position)
        {
            Name = name;
            Position = position;
        }

        public override string ToString()
        {
            return $"{Name} = '{Position.Vector3ToPosition()}',";
        }

        public bool Equals(PositionEntry other)
        {
            return Position.Equals(other.Position);
        }

        public override bool Equals(object? obj)
        {
            return obj is PositionEntry other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public static bool operator ==(PositionEntry left, PositionEntry right)
        {
            return left.Equals(right);
        }
        
        public static bool operator !=(PositionEntry left, PositionEntry right)
        {
            return !(left == right);
        }
        
        public static bool operator ==(PositionEntry left, Vector3 right)
        {
            return left.Equals(new PositionEntry(right));
        }
        
        public static bool operator !=(PositionEntry left, Vector3 right)
        {
            return !(left == right);
        }
    }
}