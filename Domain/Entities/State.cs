namespace Domain.Entities
{
    public class State
    {
        public State()
        {
            //PatientAddresses = new HashSet<PatientAddress>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        //public virtual ICollection<PatientAddress> PatientAddresses { get; set; }
    }
}