// PairingsHandler by Simon Field

namespace PairingsHandler;

public interface IPairable<TData>
{
    TData PairData { get; }
    string ToString();
}
