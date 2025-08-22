using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    internal class RegistStageRequest
    {
    [JsonProperty("user_id")]
    public int UserID
    {
        get; set;
    }


    [JsonProperty("name")]
    public string Name { get; set; }
}

