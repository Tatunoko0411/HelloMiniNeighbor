using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class UpdateUserCountReqest
    {

    [JsonProperty("play_time")]
    public int playTime { get; set; }

    [JsonProperty("clear_time")]
    public int clearTime { get; set; }

    [JsonProperty("create_stage")]
    public int createStagge { get; set; }
}

