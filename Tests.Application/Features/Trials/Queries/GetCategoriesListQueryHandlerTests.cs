using Signaturit.Application.Features.Trials.Queries.GetAllCached;
using Signaturit.Application.Interfaces.CacheRepositories;
using Signaturit.Application.Interfaces.Repositories;
using Signaturit.Application.Mappings;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Signaturi.Infrastructure.UnitTests.Mocks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static Signaturit.Application.Features.Trials.Queries.GetById.GetTrialByIdQuery;
using Signaturit.Application.Features.Trials.Queries.GetById;
using Signaturit.Web.Areas.Catalog.Models;
using System.Linq;

namespace Signaturi.Application.UnitTests.Features.Trials.Queries
{
    public class GetTrialsListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITrialCacheRepository> _mockTrialCacheRepository;

        public GetTrialsListQueryHandlerTests()
        {
            _mockTrialCacheRepository = RepositoryMocks.GetTrialCacheRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TrialProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Test]
        public async Task GetCategoriesListTest()
        {
            var handler = new GetAllTrialsCachedQueryHandler(_mockTrialCacheRepository.Object, _mapper);
            var result = await handler.Handle(new GetAllTrialsCachedQuery(), CancellationToken.None);

            Assert.AreEqual(17, result.Data.Count);
        }

        private async Task<GetAllTrialsCachedResponse> GetById(int id)
        {
            var handler = new GetAllTrialsCachedQueryHandler(_mockTrialCacheRepository.Object, _mapper);
            var response = await handler.Handle(new GetAllTrialsCachedQuery(), CancellationToken.None);

            var res = response.Data.FirstOrDefault(q => q.Id == id);

            return res;
        }

        [Test(Description = "No importa si tiene comodin, gana ya con lo que tiene")]
        public async Task GetTest1()
        {
            // DEF: KN
            // PRO: KK#

            var result = await GetById(1);

            Assert.AreEqual("PRO", result.Result);
            Assert.AreEqual("CLOSED", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest2()
        {
            // DEF: KN#
            // PRO: KKV

            var result = await GetById(2);

            Assert.AreEqual("Win: Need K", result.Result);
            Assert.AreEqual("PENDING", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest3()
        {
            // DEF: VN
            // PRO: NV

            var result = await GetById(3);

            Assert.AreEqual("---", result.Result);
            Assert.AreEqual("TIED", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest4()
        {
            // DEF: NNVV
            // PRO: NNV#

            var result = await GetById(4);

            Assert.AreEqual("Win: Need N", result.Result);
            Assert.AreEqual("PENDING", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest5()
        {
            // DEF: NKVVVK#
            // PRO: NNKNNN

            var result = await GetById(5);

            Assert.AreEqual("Win: Need K", result.Result);
            Assert.AreEqual("PENDING", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest6()
        {
            // DEF: KNN
            // PRO: KNN#

            var result = await GetById(6);

            Assert.AreEqual("Win: Need N", result.Result);
            Assert.AreEqual("PENDING", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest7()
        {
            // DEF: NN
            // PRO: NN#

            var result = await GetById(7);

            Assert.AreEqual("Win: Need V", result.Result);
            Assert.AreEqual("PENDING", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest8()
        {
            // DEF: KNN
            // PRO: #NN

            var result = await GetById(8);

            Assert.AreEqual("Tie: Need K", result.Result);
            Assert.AreEqual("PENDING", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest9()
        {
            // DEF: KNN
            // PRO: KNV

            var result = await GetById(9);

            Assert.AreEqual("DEF", result.Result);
            Assert.AreEqual("CLOSED", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest10()
        {
            // DEF: KV
            // PRO: KVVVVV

            var result = await GetById(10);

            Assert.AreEqual("---", result.Result);
            Assert.AreEqual("TIED", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest11()
        {
            // DEF: KKVV
            // PRO: KK#

            var result = await GetById(11);

            Assert.AreEqual("Win: Need N", result.Result);
            Assert.AreEqual("PENDING", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest12()
        {
            // DEF: 
            // PRO:

            var result = await GetById(12);

            Assert.AreEqual("", result.Result);
            Assert.AreEqual("UNSTARTED", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest13()
        {
            // DEF: K
            // PRO: 

            var result = await GetById(13);

            Assert.AreEqual("", result.Result);
            Assert.AreEqual("UNSTARTED", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest14()
        {
            // DEF: 
            // PRO: K

            var result = await GetById(14);

            Assert.AreEqual("", result.Result);
            Assert.AreEqual("UNSTARTED", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest15()
        {
            // DEF: #
            // PRO: 

            var result = await GetById(15);

            Assert.AreEqual("", result.Result);
            Assert.AreEqual("UNSTARTED", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest16()
        {
            // DEF: 
            // PRO: #

            var result = await GetById(16);

            Assert.AreEqual("", result.Result);
            Assert.AreEqual("UNSTARTED", result.Status);
        }

        [Test(Description = "")]
        public async Task GetTest17()
        {
            // DEF: VVVV
            // PRO: N#VV

            var result = await GetById(17);

            Assert.AreEqual("Win: Need V", result.Result);
            Assert.AreEqual("PENDING", result.Status);
        }
    }
}
