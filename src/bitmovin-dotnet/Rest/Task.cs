using System.Collections.Generic;
using com.bitmovin.Api.Enums;

namespace com.bitmovin.Api.Rest
{
    public class Task
    {
        public Status? Status { get; set; }

        public string Name { get; set; }

        public double? Eta { get; set; }

        public int? Progress { get; set; }

        public List<Message> Messages { get; set; }

        public List<Subtask> Subtasks { get; set; }
    }
}