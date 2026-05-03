using System;
using System.Collections.Generic;
using System.Text;

namespace GoalTracker.Domain.Entities.Base
{
    //Need for optimize speed loading models for user. For example tasks, projects, goals, life areas. We can load only user id and then load user if we need it.
    public abstract class BaseWithUser
    {
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public GoalTrackerUser User { get; set; }
    }
}
