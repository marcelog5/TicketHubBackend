namespace Application.Abstracts
{
    public interface IUseCase<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        Task<TOutput> Execute(TInput input, CancellationToken cancellationToken = default);
    }
}
