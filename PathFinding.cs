namespace DUCalculator;

using System.Linq;
using System.Numerics;

public static class PathFinding
{
    /// <summary>
    /// Brute Force Algo to Get the Shortest Path
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="points"></param>
    /// <returns></returns>
    public static IEnumerable<Vector3> GetShortestPath(Vector3 startPoint, Vector3[] points)
    {
        // Create a copy of the input list of points
        var remainingPoints = (Vector3[])points.Clone();

        // Initialize an empty list to store the final path
        var finalPath = new Vector3[points.Length + 1];

        // The current point starts as the start point
        var currentPoint = startPoint;

        // add the start point to the finalPath
        finalPath[0] = startPoint;

        // While there are still points in the list
        for (var i = 1; i < finalPath.Length; i++)
        {
            // Find the closest point to the current point
            var closestDistance = float.MaxValue;
            var closestPoint = new Vector3();
            
            foreach (var point in remainingPoints)
            {
                var distance = Vector3.Subtract(currentPoint, point);
                var length = Vector3.Dot(distance,distance);
                if (length < closestDistance)
                {
                    closestDistance = length;
                    closestPoint = point;
                }
            }

            // Add the closest point to the final path
            finalPath[i] = closestPoint;

            // Remove the closest point from the remaining points list
            remainingPoints = remainingPoints.Where(val => val != closestPoint).ToArray();

            // Update the current point
            currentPoint = closestPoint;
        }

        return finalPath;
    }
}