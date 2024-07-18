// PairingsHandler by Simon Field

using Logging.Broadcasting;
using System.Collections.Generic;

namespace PairingsHandler;

/// <summary>
/// Handle the creation of a list of pairs of type <typeparamref name="THolder"/> by pairing
/// a list of objects of type <typeparamref name="TEntry1"/>, each containing values of type <typeparamref name="TData1"/>
/// and a list of objects of type <typeparamref name="TEntry2"/>, each containing values of type <typeparamref name="TData2"/>.
/// </summary>
/// <typeparam name="THolder">The type of the list of pairs.</typeparam>
/// <typeparam name="TData1">The type of the values of the <typeparamref name="TEntry1"/> objects.</typeparam>
/// <typeparam name="TData2">The type of the values of the <typeparamref name="TEntry2"/> objects.</typeparam>
/// <typeparam name="TEntry1">The type of the first object in the pair.</typeparam>
/// <typeparam name="TEntry2">The type of the second object in the pair.</typeparam>
/// <remarks>
/// Create a handler for pairing <typeparamref name="TEntry1"/> information with <typeparamref name="TEntry2"/> information, stored in a list of type <typeparamref name="THolder"/>.
/// </remarks>
public abstract class PairingsHandlerBase<THolder, TData1, TData2, TEntry1, TEntry2>(StringBroadcaster bcast) :
    StringBroadcasterBase(bcast),
    IPairingsHandler<THolder, TData1, TData2, TEntry1, TEntry2>,
    IEnumerable<THolder>
    where THolder : IPairHolder<TData1, TData2, TEntry1, TEntry2>
    where TEntry1 : IPairable<TData1>
    where TEntry2 : IPairable<TData2>
{
    public abstract string Name { get; }

    protected List<THolder> Pairs { get; set; } = [];

    public virtual void CalculatePairings(List<TEntry1> entry1s, List<TEntry2> entry2s) => Pairs = PairPoints(entry1s, entry2s);

    public virtual void CalculatePairings(List<THolder> pairs) => Pairs = pairs;

    /// <summary>
    /// Create a list of pairs of type <typeparamref name="THolder"/> by pairings lists of type <typeparamref name="TEntry1"/> and of type <typeparamref name="TEntry2"/> objects.
    /// </summary>
    /// <param name="entry1s">A list of type <typeparamref name="TEntry1"/> objects.</param>
    /// <param name="entry2s">A list of type <typeparamref name="TEntry2"/> objects.</param>
    /// <returns>A list of pairs of type <typeparamref name="THolder"/>, based on the lists of type <typeparamref name="TEntry1"/> and <typeparamref name="TEntry2"/>.</returns>
    protected abstract List<THolder> PairPoints(List<TEntry1> entry1s, List<TEntry2> entry2s);

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
