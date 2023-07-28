#nullable disable

namespace Domain.Entities
{
    public class Gender
    {
        public Gender()
        {
            //Patients = new HashSet<Patient>();
        }

        public int ID { get; set; }
        public string Value { get; set; }
        public bool Active { get; set; }

        //public virtual ICollection<Patient> Patients { get; set; }
    }
}