using Microsoft.AspNetCore.Mvc.Testing;
using MonstersInc;
using MonstersInc.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;
using IdentityModel.Client;

using IdentityServer4.Models;
using MonstersIncUnitTest.Attributes;
using Newtonsoft.Json;
using MonstersInc.Core.Models;

namespace MonstersIncUnitTest
{
    [TestCaseOrderer("MonstersIncUnitTest.Orderers.PriorityOrderer", "MonstersIncUnitTest")]
    public class WorkdayManagementTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;
        private WebApplicationFactory<Startup> _factory;
        private readonly string _token;

        public WorkdayManagementTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            }) ;

            var client = new HttpClient();

            var disco = client.GetDiscoveryDocumentAsync("https://localhost:44385").Result;

            if (disco.HttpStatusCode == HttpStatusCode.OK)
            {
                var response = client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = "https://localhost:44385/connect/token",

                    ClientId = "unit_test_client",
                    ClientSecret = new Secret("secret".Sha256()).ToString(),
                    Scope = "api1",

                    UserName = "michaelc",
                    Password = "Misha23!"
                }).Result;

                if (response.HttpStatusCode== HttpStatusCode.OK )
                {
                    _token = response.AccessToken;
                    _client.SetBearerToken(_token);
                }
            }
        }

        [Theory(DisplayName = "StartWorkDayTest"), TestPriority(1)]
        [MemberData(nameof(GetStartWorkDayTestData))]
        public async void StartWorkDayTest(Func<HttpStatusCode, bool> checkResult)
        {
            var response = await _client.GetAsync($"WorkdayManagement/StartWorkDay");

            Assert.True(checkResult.Invoke(response.StatusCode));
        }

        public static IEnumerable<object[]> GetStartWorkDayTestData()
        {
            var allData = new List<object[]>
            {
                //start work day saccess
                new object[]
                {
                    (Func<HttpStatusCode,bool>)((q) => { return q == HttpStatusCode.OK; } )
                },
                //duplicate start command = failed
                new object[]
                {
                    (Func<HttpStatusCode,bool>)((q) => { return q == HttpStatusCode.NotFound; } )
                },

            };
            foreach (var item in allData)
            {
                yield return item;
            }
        }

        [Theory(DisplayName = "EndScaringTest"), TestPriority(3)]
        [MemberData(nameof(GetEndScaringTestData))]
        public async void EndScaringTest(object doorId, Func<HttpStatusCode, bool> checkResult)
        {
            var response = await _client.GetAsync($"WorkdayManagement/EndScaring?doorId={doorId}");

            Assert.True(checkResult.Invoke(response.StatusCode));
        }

        public static IEnumerable<object[]> GetEndScaringTestData()
        {
            var allData = new List<object[]>
            {
                // end scaring doesn't existing door - failed
                new object[]
                {
                    -1,
                    (Func<HttpStatusCode,bool>)((q) => { return q == HttpStatusCode.NotFound; } )
                },
                // end scaring doesn't catched previosly door - failed
                new object[]
                {
                    10,
                    (Func<HttpStatusCode,bool>)((q) => { return q == HttpStatusCode.NotFound; } )
                },
                // end scaring - success
                new object[]
                {
                    1,
                    (Func<HttpStatusCode,bool>)((q) => { return q == HttpStatusCode.OK; } )
                },
                // end scaring - success
                new object[]
                {
                    5,
                    (Func<HttpStatusCode,bool>)((q) => { return q == HttpStatusCode.OK; } )
                },
                // end scaring already finished door - failed
                new object[]
                {
                    1,
                    (Func<HttpStatusCode,bool>)((q) => { return q == HttpStatusCode.NotFound; } )
                }
            };
            foreach (var item in allData)
            {
                yield return item;
            }
        }

        [Theory(DisplayName = "StartScaringTest"), TestPriority(2)]
        [MemberData(nameof(GetStartScaringTestData))]
        public async void StartScaringTest(object doorId, Func<HttpStatusCode, bool> checkResult)
        {
            var response = await _client.GetAsync($"WorkdayManagement/StartScaring?doorId={doorId}");

            Assert.True(checkResult.Invoke(response.StatusCode));
        }

        public static IEnumerable<object[]> GetStartScaringTestData()
        {
            var allData = new List<object[]>
            {
                //catch existing door - success
                new object[]
                {
                    1,
                    (Func<HttpStatusCode,bool>)((q) => { return q == HttpStatusCode.OK; } )
                },
                //catch existing door - success
                new object[]
                {
                    5,
                    (Func<HttpStatusCode,bool>)((q) => { return q == HttpStatusCode.OK; } )
                },
                //catch does't existing door - fail
                new object[]
                {
                    -1,
                    (Func<HttpStatusCode,bool>)((q) => { return q == HttpStatusCode.NotFound; } )
                },

            };
            foreach (var item in allData)
            {
                yield return item;
            }
        }

        [Theory(DisplayName = "DailyWorkSummaryTest"), TestPriority(4)]
        [MemberData(nameof(GetDailyWorkSummaryData))]
        public async void DailyWorkSummaryTest(object url, Func<List<WorkDayPerformance>, bool> checkResult)
        {
            var response = await _client.GetAsync(url.ToString());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = JsonConvert.DeserializeObject<List<WorkDayPerformance>>(response.Content.ReadAsStringAsync().Result);
            Assert.True(checkResult.Invoke(result));
             
        }

        public static IEnumerable<object[]> GetDailyWorkSummaryData()
        {
            var allData = new List<object[]>
            {
                //from date in future - return 0
                new object[]
                {
                    "WorkdayManagement/DailyWorkSummary?pFromDateTime=01/01/2030",
                    (Func<List<WorkDayPerformance>,bool>)((q) => {
                        return q.Count == 0; })
                },
                //to date in past - return 0
                new object[]
                {
                    $"WorkdayManagement/DailyWorkSummary?pTtoDateTime=01/01/2020",
                    (Func<List<WorkDayPerformance>,bool>)((q) => {
                        return q.Count == 0; })
                },
                //return 600
                new object[]
                {
                    $"WorkdayManagement/DailyWorkSummary",
                    (Func<List<WorkDayPerformance>,bool>)((q) => {
                        return q.Count == 1 && q[0].ActualEnergyAmount == 600; })
                },

            };
            foreach (var item in allData)
            {
                yield return item;
            }
        }

    }
}
