// PairingsHandler by Simon Field

namespace PairingsHandler;

public interface IPairHolder<TData1, TData2, TOne, TTwo>
    where TOne : IPairable<TData1>
    where TTwo : IPairable<TData2>
{
    /// <summary>
    /// The pair of <typeparamref name="TData1"/> and <typeparamref name="TData2"/> values' <typeparamref name="TOne"/> object.
    /// </summary>
    TOne Entry1 { get; }

    /// <summary>
    /// The pair of <typeparamref name="TData1"/> and <typeparamref name="TData2"/> values' <typeparamref name="TTwo"/> object.
    /// </summary>
    TTwo Entry2 { get; }

    string ToString();
}
