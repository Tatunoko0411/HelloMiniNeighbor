using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistUserResponse
{
    [JsonProperty("user_id")]
    public int UserID
    {
        get; set;
    }


    [JsonProperty("apiToken")]
    public string APIToken { get; set; }
}
