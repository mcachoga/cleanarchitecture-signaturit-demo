namespace Signaturit.Application.Features.Trials.Queries.GetById
{
    public class GetTrialByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Defense { get; set; }
        public string Prosecutor { get; set; }
        public int Resolution { get; set; }
        
    }
}