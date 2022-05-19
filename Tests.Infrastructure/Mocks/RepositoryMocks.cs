using Signaturit.Application.Interfaces.CacheRepositories;
using Signaturit.Application.Interfaces.Repositories;
using Signaturit.Domain.Entities.Catalog;
using Moq;
using System.Collections.Generic;

namespace Signaturit.Infrastructure.UnitTests.Mocks
{
    public static class RepositoryMocks
    {
        private static List<Trial> trials = new List<Trial>
            {
                new Trial { Id = 1, Name = "Test 01", Defense = "KN", Prosecutor = "KK#", Resolution = 0 },
                new Trial { Id = 2, Name = "Test 02", Defense = "KN#", Prosecutor = "KKV", Resolution = 0 },
                new Trial { Id = 3, Name = "Test 03", Defense = "VN", Prosecutor = "NV", Resolution = 0 },
                new Trial { Id = 4, Name = "Test 04", Defense = "NNVV", Prosecutor = "NNV#", Resolution = 0 },
                new Trial { Id = 5, Name = "Test 05", Defense = "NKVVVK#", Prosecutor = "NNKNNN", Resolution = 0 },
                new Trial { Id = 6, Name = "Test 06", Defense = "KNN", Prosecutor = "KNN#", Resolution = 0 },
                new Trial { Id = 7, Name = "Test 07", Defense = "NN", Prosecutor = "NN#", Resolution = 0 },
                new Trial { Id = 8, Name = "Test 08", Defense = "KNN", Prosecutor = "#NN", Resolution = 0 },
                new Trial { Id = 9, Name = "Test 09", Defense = "KNN", Prosecutor = "KNV", Resolution = 0 },
                new Trial { Id = 10, Name = "Test 10", Defense = "KV", Prosecutor = "KVVVVV", Resolution = 0 },
                new Trial { Id = 11, Name = "Test 11", Defense = "KKVV", Prosecutor = "KK#", Resolution = 0 },
                new Trial { Id = 12, Name = "Test 12", Defense = "", Prosecutor = "", Resolution = 0 },
                new Trial { Id = 13, Name = "Test 13", Defense = "K", Prosecutor = "", Resolution = 0 },
                new Trial { Id = 14, Name = "Test 14", Defense = "", Prosecutor = "K", Resolution = 0 },
                new Trial { Id = 15, Name = "Test 15", Defense = "#", Prosecutor = "", Resolution = 0 },
                new Trial { Id = 16, Name = "Test 16", Defense = "", Prosecutor = "#", Resolution = 0 },
                new Trial { Id = 17, Name = "Test 17", Defense = "N#VV", Prosecutor = "VVVV", Resolution = 0 },
            };

        public static Mock<ITrialRepository> GetTrialRepository()
        {
            var mockCategoryRepository = new Mock<ITrialRepository>();
            mockCategoryRepository.Setup(repo => repo.GetListAsync()).ReturnsAsync(trials);

            mockCategoryRepository.Setup(repo => repo.InsertAsync(It.IsAny<Trial>())).ReturnsAsync(
                (Trial trial) =>
                {
                    trials.Add(trial);
                    return trial.Id;
                });

            return mockCategoryRepository;
        }

        public static Mock<ITrialCacheRepository> GetTrialCacheRepository()
        {
            var mockCategoryRepository = new Mock<ITrialCacheRepository>();
            mockCategoryRepository.Setup(repo => repo.GetCachedListAsync()).ReturnsAsync(trials);

            return mockCategoryRepository;
        }
    }
}
