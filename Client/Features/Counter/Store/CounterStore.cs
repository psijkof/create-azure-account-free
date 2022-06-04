namespace BlazorApp.Client.Features.Counter.Store
{

    /// <summary>
    /// The Counter state
    /// </summary>
    /// <param name="Counter"></param>
    public record CounterState(int Counter);

    /// <summary>
    /// THe Counter reducers
    /// </summary>
    public static class CounterReducers
    {
        [ReducerMethod(typeof(CounterIncrementAction))]
        public static CounterState Decrement(CounterState state)
        {
            return state with { Counter = state.Counter + 1 };
        }

        [ReducerMethod(typeof(CounterDecrementAction))]
        public static CounterState Increment(CounterState state)
        {
            return state with { Counter = state.Counter - 1 };
        }
    }

    /// <summary>
    /// The Counter feature
    /// </summary>
    public class Feature : Feature<CounterState>
    {
        public override string GetName() => "Counter";

        protected override CounterState GetInitialState()
        {
            return new CounterState(0);
        }
    }

    #region CounterActions
    public record CounterIncrementAction();
    public record CounterDecrementAction();
    #endregion
}
