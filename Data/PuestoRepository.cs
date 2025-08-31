using Microsoft.Data.SqlClient;
using webApiExamen.Models;

namespace webApiExamen.Data
{
    public class PuestoRepository
    {

        private readonly string _connectionString;

        public PuestoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PersonaDB");
        }

        public async Task<IEnumerable<Puesto>> GetPuestos()
        {
            var puestos = new List<Puesto>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT idpuesto, descripcion FROM Puesto", conn))
            {
                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    puestos.Add(new Puesto
                    {
                        idpuesto = reader.GetInt32(0),
                        descripcion = reader.GetString(1)
                    });
                }
            }
            return puestos;
        }


    }
}
