using System.Collections.Generic;

namespace DoctorsOffice.Models
{
  public class Doctor
  {
    public Doctor()
    {
      this.JoinEntities = new HashSet<DoctorPatient>();
      this.JoinEntities1 = new HashSet<DoctorSpecialty>();
    }

    public int DoctorId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<DoctorPatient> JoinEntities { get; set; }
    public virtual ICollection<DoctorSpecialty> JoinEntities1 { get; set; }
  }
}