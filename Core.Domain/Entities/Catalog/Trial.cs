using Signaturit.Domain.Abstractions;

namespace Signaturit.Domain.Entities.Catalog
{
    public class Trial : AuditableEntity
    {
        public string Name { get; set; }
        public string Defense { get; set; }
        public string Prosecutor { get; set; }
        public int Resolution { get; set; }
    }
}