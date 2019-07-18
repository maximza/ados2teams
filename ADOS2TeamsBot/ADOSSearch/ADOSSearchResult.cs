using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ADOS2Teams.ADOSSearch.Model
{
    public partial class ADOSSearchResult
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("results")]
        public WorkItemResult[] Results { get; set; }

        [JsonProperty("infoCode")]
        public long InfoCode { get; set; }

        [JsonProperty("facets")]
        public Facets Facets { get; set; }
    }

    public partial class Facets
    {
        [JsonProperty("System.TeamProject")]
        public SystemAssignedToElement[] SystemTeamProject { get; set; }

        [JsonProperty("System.WorkItemType")]
        public SystemAssignedToElement[] SystemWorkItemType { get; set; }

        [JsonProperty("System.State")]
        public SystemAssignedToElement[] SystemState { get; set; }

        [JsonProperty("System.AssignedTo")]
        public SystemAssignedToElement[] SystemAssignedTo { get; set; }
    }

    public partial class SystemAssignedToElement
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resultCount")]
        public long ResultCount { get; set; }
    }

    public partial class WorkItemResult
    {
        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("fields")]
        public Dictionary<string, string> Fields { get; set; }

        [JsonProperty("hits")]
        public Hit[] Hits { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Hit
    {
        [JsonProperty("fieldReferenceName")]
        public string FieldReferenceName { get; set; }

        [JsonProperty("highlights")]
        public string[] Highlights { get; set; }
    }

    public partial class Project
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }

    public partial class ADOSSearchResult
    {
        public static ADOSSearchResult FromJson(string json) => JsonConvert.DeserializeObject<ADOSSearchResult>(json, ADOS2Teams.ADOSSearch.Model.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ADOSSearchResult self) => JsonConvert.SerializeObject(self, ADOS2Teams.ADOSSearch.Model.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
