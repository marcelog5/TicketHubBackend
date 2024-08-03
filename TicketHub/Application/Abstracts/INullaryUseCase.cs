namespace Application.Abstracts
{
    public interface INullaryUseCase<TOutput>
        where TOutput : class
    {
        // 1. The nullary use case has only an output. Doesn't return a entity, agregate or value object.
        // 2. The use case implements the default command pattern.

        public abstract TOutput Execute();
    }
}
