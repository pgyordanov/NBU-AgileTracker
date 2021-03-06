﻿namespace AgileTracker.Common.Events.Models
{
    using System;

    public class TaskFinishedEventModel
    {
        public int ProjectGroupId { get; set; }

        public int ProjectId { get; set; }

        public int TaskId { get; set; }

        public DateTime TaskFinishedOn { get; set; }
    }
}
