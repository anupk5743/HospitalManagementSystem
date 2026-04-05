using AutoMapper;
using HMS.API.DTOs;
using HMS.API.Models;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Appointment, AppointmentDTO>()
            .ForMember(dest => dest.PatientName,
                opt => opt.MapFrom(src => src.Patient.FullName))

            .ForMember(dest => dest.DoctorName,
                opt => opt.MapFrom(src => src.Doctor.Name))

            .ForMember(dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Doctor.Department.Name));
    }
}