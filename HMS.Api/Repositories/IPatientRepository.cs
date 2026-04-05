using HMS.API.Models;

namespace HMS.API.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAll();

        Task<Patient> GetById(int id);

        Task<Patient> Add(Patient patient);

        Task<Patient> Update(Patient patient);

        Task<bool> Delete(int id);
    }
}