// PairingsHandler by Simon Field

namespace PairingsHandler;

public interface IPairingsFactory<THolder, TData1, TData2, TEntry1, TEntry2>
    where THolder : IPairHolder<TData1, TData2, TEntry1, TEntry2>
    where TEntry1 : IPairable<TData1>
    where TEntry2 : IPairable<TData2>
{
    /// <summary>
    /// Create a <see cref="IPairingsHandler{THolder, TData1, TData2, TEntry1, TEntry2}"/>
    /// for pairing objects of type <typeparamref name="TEntry1"/> and <typeparamref name="TEntry2"/>,
    /// each containing <typeparamref name="TData1"/> and <typeparamref name="TData2"/> data values respectively.
    /// </summary>
    /// <returns>A <see cref="IPairingsHandler{THolder, TData1, TData2, TEntry1, TEntry2}"/> for handling this type of pairing.</returns>
    public IPairingsHandler<THolder, TData1, TData2, TEntry1, TEntry2> GetHandler();
}
