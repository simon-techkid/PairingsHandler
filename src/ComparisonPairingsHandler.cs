// PairingsHandler by Simon Field

using Logging;
using Logging.Broadcasting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PairingsHandler;

public abstract partial class ComparisonPairingsHandler<THolder, TData1, TData2, TEntry1, TEntry2>(StringBroadcaster bcast) :
    PairingsHandlerBase<THolder, TData1, TData2, TEntry1, TEntry2>(bcast),
    IComparable<TEntry1, TEntry2>
    where THolder : IPairHolder<TData1, TData2, TEntry1, TEntry2>
    where TEntry1 : IPairable<TData1>
    where TEntry2 : IPairable<TData2>
{
    protected override List<THolder> PairPoints(List<TEntry1> entry1s, List<TEntry2> entry2s)
    {
        int index = 0; // Index of the pairing
        List<THolder> pairs = [];

        foreach (TEntry1 entry1 in entry1s)
        {
            List<Task> tasks =
            [
                Task.Run(() =>
                {
                    TEntry2 match;

                    List<int> comparisons = entry2s
                        .Select(entry => Compare(entry1, entry))
                        .ToList();

                    int zeroIndex = comparisons.IndexOf(0);

                    if (!zeroIndex.Equals(-1))
                    {
                        match = entry2s[zeroIndex];
                    }
                    else
                    {
                        int LastNegative = comparisons.LastIndexOf(-1);
                        int FirstPositive = comparisons.IndexOf(1);

                        IEnumerable<int> bridge = Enumerable
                            .Range(LastNegative, FirstPositive - LastNegative + 1);

                        List<TEntry2> matchs = bridge.Select(candidate => entry2s[candidate]).ToList();

                        match = NarrowDown(entry1, matchs);
                    }

                    THolder holder = GetHolder(entry1, match);

                    BCaster.Broadcast($"{holder.ToString()}", LogLevel.Pair);

                    lock (pairs)
                    {
                        pairs.Add(holder);
                        index++;
                    }

                    return GetHolder(entry1, entry2s.First());
                })
            ];

            Task[] pairJobs = tasks.ToArray();

            BCaster.Broadcast($"Waiting for {pairJobs.Length} pairs to be created for '{entry1.ToString()}'", LogLevel.Debug);

            Task.WaitAll(pairJobs);

            BCaster.Broadcast($"{pairJobs.Length} pairs created for '{entry1.ToString()}'", LogLevel.Debug);
        }

        return pairs;
    }

    /// <summary>
    /// Get a <typeparamref name="THolder"/> object from a pair of <typeparamref name="TEntry1"/> and <typeparamref name="TEntry2"/> objects.
    /// </summary>
    /// <param name="entry1">The first object of type <typeparamref name="TEntry1"/> to pair.</param>
    /// <param name="entry2">The second object of type <typeparamref name="TEntry2"/> to pair.</param>
    /// <returns></returns>
    protected abstract THolder GetHolder(TEntry1 entry1, TEntry2 entry2);

    public abstract int Compare(TEntry1 entry1, TEntry2 entry2);

    protected abstract TEntry2 NarrowDown(TEntry1 element, IList<TEntry2> elements);
}
