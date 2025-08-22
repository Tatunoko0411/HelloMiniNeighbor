using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class RegistStageButtonRequest
    {
    [JsonProperty("stage_id")]
    public int StageID
    {
        get; set;
    }


    [JsonProperty("object_id")]
    public string ObjectID { get; set; }
}

