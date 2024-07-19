namespace BookStore.Api.Contracts
{
    public record class BooksResponse(Guid id, string title, string description, decimal price);
}
