// PairingsHandler by Simon Field

namespace PairingsHandler;

/// <summary>
/// An interface for holding a pair of objects, each within the same pairing.
/// </summary>
/// <typeparam name="TData1">The data type of the data within the first pairable object, of type <typeparamref name="TOne"/>.</typeparam>
/// <typeparam name="TData2">The data type of the data within the second pairable object, of type <typeparamref name="TTwo"/>.</typeparam>
/// <typeparam name="TOne">The first pairable object, containing an object of type <typeparamref name="TData1"/>.</typeparam>
/// <typeparam name="TTwo">The second pairable object, containing an object of type <typeparamref name="TData2"/>.</typeparam>
public interface IPairHolder<TData1, TData2, TOne, TTwo>
    where TOne : IPairable<TData1>
    where TTwo : IPairable<TData2>
{
    /// <summary>
    /// The pair of <typeparamref name="TData1"/> and <typeparamref name="TData2"/> values' <typeparamref name="TOne"/> object.
    /// </summary>
    public TOne Entry1 { get; }

    /// <summary>
    /// The pair of <typeparamref name="TData1"/> and <typeparamref name="TData2"/> values' <typeparamref name="TTwo"/> object.
    /// </summary>
    public TTwo Entry2 { get; }

    /// <summary>
    /// A <see cref="string"/> representation of the pair.
    /// </summary>
    /// <returns>A <see cref="string"/> representation of this pairing.</returns>
    public string ToString();
}
