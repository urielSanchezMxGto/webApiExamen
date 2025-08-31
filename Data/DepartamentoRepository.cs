using Microsoft.Data.SqlClient;
using webApiExamen.Models;

namespace webApiExamen.Data
{
    public class DepartamentoRepository
    {

        private readonly string _connectionString;

        public DepartamentoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PersonaDB");
        }

        public async Task<IEnumerable<Departamento>> GetDepartamentos()
        {
            var departamentos = new List<Departamento>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT iddepartamento, descripcion FROM Departamento", conn))
            {
                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    departamentos.Add(new Departamento
                    {
                        iddepartamento = reader.GetInt32(0),
                        descripcion = reader.GetString(1)
                    });
                }
            }
            return departamentos;
        }


    }
}
