// PairingsHandler by Simon Field

using Logging;
using Logging.Broadcasting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PairingsHandler;

/// <summary>
/// Handle the creation of a list of pairs of type <typeparamref name="THolder"/> by pairing
/// a list of objects of type <typeparamref name="TEntry1"/>, each containing values of type <typeparamref name="TData1"/>
/// and a list of objects of type <typeparamref name="TEntry2"/>, each containing values of type <typeparamref name="TData2"/>.
/// </summary>
/// <typeparam name="THolder"></typeparam>
/// <typeparam name="TData1"></typeparam>
/// <typeparam name="TData2"></typeparam>
/// <typeparam name="TEntry1"></typeparam>
/// <typeparam name="TEntry2"></typeparam>
public abstract partial class PairingsHandler<THolder, TData1, TData2, TEntry1, TEntry2> :
    StringBroadcasterBase, IEnumerable<THolder>, IComparable<TEntry1, TEntry2>
    where THolder : IPairHolder<TData1, TData2, TEntry1, TEntry2>
    where TEntry1 : IPairable<TData1>
    where TEntry2 : IPairable<TData2>
{
    protected List<THolder> Pairs { get; set; }

    /// <summary>
    /// The name of this set of pairings.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Create a handler for pairing GPS information with Song information.
    /// </summary>
    protected PairingsHandler(StringBroadcaster bcast) : base(bcast)
    {
        Pairs = new();
    }

    /// <summary>
    /// Create a list of pairs of type <typeparamref name="THolder"/> by pairing lists of type <typeparamref name="TEntry1"/> and of type <typeparamref name="TEntry2"/> objects.
    /// </summary>
    /// <param name="entry1s">A list of type <typeparamref name="TEntry1"/> objects.</param>
    /// <param name="entry2s">A list of type <typeparamref name="TEntry2"/> objects.</param>
    public virtual void CalculatePairings(List<TEntry1> entry1s, List<TEntry2> entry2s) => Pairs = PairPoints(entry1s, entry2s);

    /// <summary>
    /// Create a <see cref="PairingsHandler{THolder, TData1, TData2, TEntry1, TEntry2}"/> from a pre-existing list of pairs of type <typeparamref name="THolder"/>.
    /// </summary>
    /// <param name="pairs">A list of pairs of type <typeparamref name="THolder"/>.</param>
    public virtual void CalculatePairings(List<THolder> pairs) => Pairs = pairs;

    /// <summary>
    /// Create a list of pairs of type <typeparamref name="THolder"/> by pairings lists of type <typeparamref name="TEntry1"/> and of type <typeparamref name="TEntry2"/> objects.
    /// </summary>
    /// <param name="entry1s">A list of type <typeparamref name="TEntry1"/> objects.</param>
    /// <param name="entry2s">A list of type <typeparamref name="TEntry2"/> objects.</param>
    /// <returns>A list of pairs of type <typeparamref name="THolder"/>, based on the lists of type <typeparamref name="TEntry1"/> and <typeparamref name="TEntry2"/>.</returns>
    protected List<THolder> PairPoints(List<TEntry1> entry1s, List<TEntry2> entry2s)
    {
        int index = 0; // Index of the pairing
        List<THolder> pairs = new();

        foreach (TEntry1 entry1 in entry1s)
        {
            List<Task> tasks = new()
            {
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
            };

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

    /// <summary>
    /// Run specific tasks after all pairings have been created.
    /// </summary>
    public virtual void FinalizePairings() { }

    public IEnumerator<THolder> GetEnumerator()
    {
        return Pairs.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
