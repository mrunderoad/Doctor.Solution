namespace DoctorsOffice.Models
{
  public class DoctorSpecialty
  {
    public int DoctorSpecialtyId { get; set; }
    public int SpecialtyId { get; set; }
    public int DoctorId { get; set; }
    public virtual Specialty Specialty { get; set; }
    public virtual Doctor Doctor { get; set; }
  }
}