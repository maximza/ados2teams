using NUnit.Framework;
using ADOS2Teams.ADOSSearch;
using System.Net.Http;
using System;
using Microsoft.Bot.Schema.Teams;
using Microsoft.Bot.Schema;

namespace ADOS2Teams.ADOSSearch.Tests
{
    public class SearchHandlerTests
    {

        HttpClient _client;
        [SetUp]
        public void Setup()
        {
            _client = new HttpClient
            {
                BaseAddress = new System.Uri(TestContext.Parameters["ADOSUrl"])
            };
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "", "bg35z5mtqizyxtylug5fjas6mgq7sutjwr2zlm6tsvxeb735srda"))));
        
        }

        [Test]
        public void CheckBaseSearch()
        {
            string query = TestContext.Parameters["basicQuery"];
            ADOSSearchHandler se = new ADOSSearchHandler(_client);
            MessagingExtensionResult me = se.GetSearchResultAsync(query).Result;
            
            Assert.Pass();
        }


        [Test]
        public void CheckEpicIcon()
        {
            string query = TestContext.Parameters["epicId"];
            ADOSSearchHandler se = new ADOSSearchHandler(_client);
            MessagingExtensionResult me = se.GetSearchResultAsync(query).Result;

            Assert.AreEqual("image/jpeg;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCA0NDggNDQ4Ij48cGF0aCBkPSJNNDQ4IDk2YzAgMTcuNjcyLTE0LjMyNiAzMi0zMiAzMnYyODhIMzJWMTI4Yy0xNy42NzQgMC0zMi0xNC4zMjgtMzItMzIgMC0xNy42NzQgMTQuMzI2LTMyIDMyLTMyczMyIDE0LjMyNiAzMiAzMmMwIDExLjE5MS02LjA5NCAyMC41NjQtMTQuNzk3IDI2LjI4M0wxMzYuNzI3IDI1NiAyMTYuNzkgOTQuNTQzQzIwMi42OTkgOTEuMTkxIDE5MiA3OS4xMTMgMTkyIDY0YzAtMTcuNjc0IDE0LjMyNi0zMiAzMi0zMnMzMiAxNC4zMjYgMzIgMzJjMCAxNS4xMTMtMTAuNjk5IDI3LjE5MS0yNC43ODkgMzAuNTQzTDMxMS4yNzMgMjU2bDg3LjUyMy0xMzMuNzE3QzM5MC4wOTQgMTE2LjU2NCAzODQgMTA3LjE5MSAzODQgOTZjMC0xNy42NzQgMTQuMzI2LTMyIDMyLTMyczMyIDE0LjMyNiAzMiAzMnoiIGZpbGw9IiNmZjdiMDAiIC8+PC9zdmc+", ((ThumbnailCard)me.Attachments[0].Content).Images[0].Url);
        }
    }
}