using HMS.Api.Models;
using HMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HMS.API.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DepartmentDbContext _context;

        public PatientRepository(DepartmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetById(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<Patient> Add(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> Update(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> Delete(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
                return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}