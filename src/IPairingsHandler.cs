// PairingsHandler by Simon Field

using System.Collections.Generic;

namespace PairingsHandler;

public interface IPairingsHandler<THolder, TData1, TData2, TEntry1, TEntry2> :
    IEnumerable<THolder>
    where THolder : IPairHolder<TData1, TData2, TEntry1, TEntry2>
    where TEntry1 : IPairable<TData1>
    where TEntry2 : IPairable<TData2>
{
    /// <summary>
    /// The name of this set of pairings.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Create a list of pairs of type <typeparamref name="THolder"/> by pairing lists of type <typeparamref name="TEntry1"/> and of type <typeparamref name="TEntry2"/> objects.
    /// </summary>
    /// <param name="entry1s">A list of type <typeparamref name="TEntry1"/> objects.</param>
    /// <param name="entry2s">A list of type <typeparamref name="TEntry2"/> objects.</param>
    public void CalculatePairings(List<TEntry1> entry1s, List<TEntry2> entry2s);

    /// <summary>
    /// Create a <see cref="IPairingsHandler{THolder, TData1, TData2, TEntry1, TEntry2}"/> from a pre-existing list of pairs of type <typeparamref name="THolder"/>.
    /// </summary>
    /// <param name="pairs">A list of pairs of type <typeparamref name="THolder"/>.</param>
    public void CalculatePairings(List<THolder> pairs);

    /// <summary>
    /// Run specific tasks after all pairings have been created.
    /// </summary>
    public void FinalizePairings();
}
