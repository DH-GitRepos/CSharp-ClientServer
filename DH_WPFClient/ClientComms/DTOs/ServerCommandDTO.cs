using System;
using System.Collections.Generic;

namespace DH_GUIClientComms.DTOs
{
    [Serializable]
    public class ServerCommandDTO : IDto
    {
        public int Task_ID { get; set; }
        public Dictionary<string, object> Params { get; set; }

        public ServerCommandDTO(int task_id, Dictionary<string, object> param_dict)
        {
            this.Task_ID = task_id;
            this.Params = param_dict;
        }
    }
}
