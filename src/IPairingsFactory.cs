// PairingsHandler by Simon Field

namespace PairingsHandler;

/// <summary>
/// An interface for creating a <see cref="IPairingsHandler{THolder, TData1, TData2, TEntry1, TEntry2}"/> object.
/// </summary>
/// <typeparam name="THolder">The object containing both objects of type <typeparamref name="TEntry1"/> and <typeparamref name="TEntry2"/> in the pair.</typeparam>
/// <typeparam name="TData1">The data type of the data within the first pairable object, of type <typeparamref name="TOne"/>.</typeparam>
/// <typeparam name="TData2">The data type of the data within the second pairable object, of type <typeparamref name="TTwo"/>.</typeparam>
/// <typeparam name="TEntry1">The first pairable object, containing an object of type <typeparamref name="TData1"/>.</typeparam>
/// <typeparam name="TEntry2">The second pairable object, containing an object of type <typeparamref name="TData2"/>.</typeparam>
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
