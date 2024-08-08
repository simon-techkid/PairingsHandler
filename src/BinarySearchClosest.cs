// PairingsHandler by Simon Field

using Logging.Broadcasting;
using System;
using System.Collections.Generic;

namespace PairingsHandler;

public abstract class BinarySearchClosest<THolder, TData1, TData2, TEntry1, TEntry2>(IBroadcaster<string> bcast) :
    PairingsHandlerBase<THolder, TData1, TData2, TEntry1, TEntry2>(bcast),
    IComparable<TData1, TData2>
    where THolder : IPairHolder<TData1, TData2, TEntry1, TEntry2>
    where TEntry1 : IPairable<TData1>
    where TEntry2 : IPairable<TData2>
{
    public abstract int Compare(TData1 value1, TData2 value2);

    /// <summary>
    /// Abstract method to calculate the distance between two values of type <typeparamref name="TEntry1"/>.
    /// </summary>
    /// <param name="value1">The value to calculate the distance to <paramref name="value2"/></param>
    /// <param name="value2">The value <paramref name="value1"/> is being compared to.</param>
    /// <returns>A <see langword="double"/> representing the distance between <paramref name="value1"/> and <paramref name="value2"/>.</returns>
    protected abstract double CalculateDistance(TData1 value1, TData2 value2);

    /// <summary>
    /// The maximum acceptable distance between the target value of type <typeparamref name="TEntry2"/> and the closest value of type <typeparamref name="TEntry1"/>.
    /// Set this value to <see langword="null"/> to disable the tolerance ceiling.
    /// </summary>
    protected virtual double? DistanceToleration => null;

    /// <summary>
    /// Find the closest value in a list of values of type <typeparamref name="TEntry1"/> to a target value of type <typeparamref name="TEntry2"/>.
    /// </summary>
    /// <param name="values">A list of values to search.</param>
    /// <param name="targetValue">The target value to search for.</param>
    /// <param name="closestValue">The closest value to the target value.</param>
    /// <param name="accuracy">The accuracy of the closest value.</param>
    /// <returns>True if a value was found, false otherwise.</returns>
    /// <exception cref="ArgumentException">The values list must not be null or empty.</exception>
    protected bool TryFindClosest(List<TEntry1> values, TEntry2 targetValue, out TEntry1? closestValue)
    {
        closestValue = default;
        double accuracy;

        if (values == null || values.Count == 0)
        {
            throw new ArgumentException("The values list cannot be null or empty.");
        }

        int left = 0;
        int right = values.Count - 1;

        // Perform binary search to narrow down the closest value by comparison
        while (left < right)
        {
            int mid = left + (right - left) / 2;

            if (Compare(values[mid].PairData, targetValue.PairData) < 0)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        // Determine the closest value and its distance
        if (left == 0)
        {
            closestValue = values[0];
            return true;
        }

        if (left >= values.Count)
        {
            closestValue = values[^1];
            return true;
        }

        double leftDistance = CalculateDistance(values[left].PairData, targetValue.PairData);
        double rightDistance = CalculateDistance(values[left - 1].PairData, targetValue.PairData);

        closestValue = leftDistance < rightDistance ? values[left] : values[left - 1];
        accuracy = Math.Min(leftDistance, rightDistance);

        if (DistanceToleration.HasValue)
        {
            return accuracy <= DistanceToleration.Value;
        }

        return true;
    }
}
