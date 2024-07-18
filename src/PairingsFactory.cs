// PairingsHandler by Simon Field

using Logging.Broadcasting;

namespace PairingsHandler;

public abstract class PairingsFactory<THolder, TData1, TData2, TEntry1, TEntry2> :
    StringBroadcasterBase
    where THolder : IPairHolder<TData1, TData2, TEntry1, TEntry2>
    where TEntry1 : IPairable<TData1>
    where TEntry2 : IPairable<TData2>
{
    protected PairingsFactory(StringBroadcaster BCast) : base(BCast) { }

    public abstract PairingsHandler<THolder, TData1, TData2, TEntry1, TEntry2> GetHandler();
}
