using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    public class RegistStageObjectRequest
    {
        [JsonProperty("stage_id")]
        public int StageID { get; set; }

        [JsonProperty("x")]
        public float X { get; set; }

        [JsonProperty("y")]
        public float Y { get; set; }

        [JsonProperty("rot")]
         public float Rot { get; set; }

        [JsonProperty("object_id")]
        public int ObjectID { get; set; }

}

