namespace Application.Abstracts
{
    public abstract class UseCase<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        // 1. Every use case has an input and output. Doesn't return a entity, agregate or value object.
        // 2. The use case implements the default command pattern.

        public abstract TOutput Execute(TInput input);
    }
}
