namespace OpinionatedApiExample.Shared.Rest
{
    public class GetRequest
    {
        public int PageNumber { get; set; } = 1;
        public int NumberOfRecords { get; set; } = 50;
    }
}