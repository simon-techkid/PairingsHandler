// PairingsHandler by Simon Field

namespace PairingsHandler;

/// <summary>
/// An interface for comparing two objects, each within the same pairing.
/// The first object being compared is of type <typeparamref name="TOne"/> and the second object is of type <typeparamref name="TTwo"/>.
/// </summary>
/// <typeparam name="TOne"></typeparam>
/// <typeparam name="TTwo"></typeparam>
public interface IComparable<TOne, TTwo>
{
    /// <summary>
    /// Compare two objects.
    /// </summary>
    /// <param name="one">The object to compare to the second object.</param>
    /// <param name="two">The object the first object will be compared to.</param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared.
    /// <para />
    /// Value – Meaning
    /// <para />
    /// Less than zero – <paramref name="one"/> precedes <paramref name="two"/> in the sort order.
    /// <br />
    /// Zero – <paramref name="one"/> is in the same position in the sort order as <paramref name="two"/>.
    /// <br />
    /// Greater than zero – <paramref name="one"/> follows <paramref name="two"/> in the sort order.
    /// </returns>
    public int Compare(TOne one, TTwo two);
}
