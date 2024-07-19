namespace BookStore.Api.Contracts
{
    public record class BooksRequest(string title, string description, decimal price);
}
