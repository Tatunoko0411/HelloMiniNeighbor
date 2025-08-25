using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GetStageResponse
{
    [JsonProperty("id")]
    public int ID
    {
        get; set;
    }


    [JsonProperty("user_id")]
    public int UserID { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("point")]
    public int Point { get; set; }

    [JsonProperty("play_time")]
    public int playTime { get; set; }

    [JsonProperty("clear_time")]
    public int clearTime { get; set; }

}

