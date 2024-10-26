// PairingsHandler by Simon Field

namespace PairingsHandler;

/// <summary>
/// An interface representing an object containing an object of type <typeparamref name="TData"/> that can be paired with another object.
/// </summary>
/// <typeparam name="TData">The data type of the required contained object.</typeparam>
public interface IPairable<TData>
{
    /// <summary>
    /// The object of type <typeparamref name="TData"/> contained within this object, used to create a pair.
    /// </summary>
    public TData PairData { get; }

    /// <summary>
    /// A <see cref="string"/> representation of the object.
    /// </summary>
    /// <returns>A <see cref="string"/> representation of this object.</returns>
    public string ToString();
}
