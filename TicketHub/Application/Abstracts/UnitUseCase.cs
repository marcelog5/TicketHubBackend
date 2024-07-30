namespace Application.Abstracts
{
    public abstract class UnitUseCase<TInput>
        where TInput : class
    {
        // 1. The nullary use case has only an input.
        // 2. The use case implements the default command pattern.

        public abstract void Execute(TInput input);
    }
}
