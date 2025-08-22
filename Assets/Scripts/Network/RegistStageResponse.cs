using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class RegistStageResponse
    {

        [JsonProperty("stage_id")]
        public int StageID
        {
            get; set;
        }

    }

