namespace GoalTracker.Domain.Entities
{
    //Reaction for example lead decline call, just write message to lead
    public class Reaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Will be know how manytime was Scucceded and how many times was Failed
        public int SuccedeCount { get; set; }
        public int FailCount { get; set; }
    }
}
