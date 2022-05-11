using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace StatefulWebComparer
{
    public class Diff
    {
        public static string Compare(string? left, string? right)
        {
            if (left == null)
            {
                return "Left is not set.";
            }
            if (right == null)
            {
                return "Right is not set.";
            }

            if (left == right)
            {
                return "Inputs were equal.";
            }
            else if (left.Length == right.Length)// should have the same length
            {
                for (int i = 0; i < left.Length; i++)
                {
                    if (left[i] != right[i])
                    {
                        return $"{left} and {right} diff: Offset: {i}, Length: {left.Length - i}.";
                    }
                }
            }
            else if (left.Length != right.Length)
            {
                return "Inputs are of different size!";
            }

            throw new Exception("Unexpected opperation.");
        }
    }
}
