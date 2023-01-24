using System.Numerics;
using System.Text.RegularExpressions;

namespace DUCalculator;

public partial class Program
{
    public static void Main(params string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Provide a File Path to Read for the first parameter and a start position as the second parameter");
            return;
        }
        
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