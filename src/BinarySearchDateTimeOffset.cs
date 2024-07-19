// PairingsHandler by Simon Field

using Logging.Broadcasting;
using System;

namespace PairingsHandler;

public abstract class BinarySearchDateTimeOffset<THolder, TEntry1, TEntry2>(StringBroadcaster bcast) :
    BinarySearchClosest<THolder, DateTimeOffset, DateTimeOffset, TEntry1, TEntry2>(bcast)
    where THolder : IPairHolder<DateTimeOffset, DateTimeOffset, TEntry1, TEntry2>
    where TEntry1 : IPairable<DateTimeOffset>
    where TEntry2 : IPairable<DateTimeOffset>
{
    public override int Compare(DateTimeOffset value1, DateTimeOffset value2)
    {
        return value1.CompareTo(value2);
    }

    protected override double CalculateDistance(DateTimeOffset value1, DateTimeOffset value2)
    {
        return Math.Abs((value1 - value2).TotalSeconds);
    }
}