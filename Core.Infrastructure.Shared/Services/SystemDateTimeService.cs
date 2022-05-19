using Signaturit.Application.Interfaces.Shared;
using System;

namespace Signaturit.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}