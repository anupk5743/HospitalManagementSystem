using HMS.API.Models;

public class Billing
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int AppointmentId { get; set; }

    public decimal DoctorFee { get; set; }

    public decimal MedicineCharges { get; set; }

    public decimal LabCharges { get; set; }

    public decimal TotalAmount { get; set; }

    public string PaymentStatus { get; set; }

    public Patient? Patient { get; set; }

    public Appointment? Appointment { get; set; }
}