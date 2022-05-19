namespace Signaturit.Application.Features.Trials.Queries.GetAllCached
{
    public class GetAllTrialsCachedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Defense { get; set; }
        public string Prosecutor { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
    }
}