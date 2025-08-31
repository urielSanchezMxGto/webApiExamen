using Microsoft.Data.SqlClient;
using System.Data;
using webApiExamen.Models;

namespace webApiExamen.Data
{
    public class PersonaRepository
    {

        private readonly string _connectionString;

        public PersonaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PersonaDB");
        }


        public async Task<IEnumerable<Persona>> GetPersonas(Persona? persona = null)
        {
            persona ??= new Persona();
            var personas = new List<Persona>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("BuscarPersona", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nombrePersona", SqlDbType.NVarChar, 100)
                    .Value = (object?)persona.nombre ?? DBNull.Value;
                cmd.Parameters.Add("@descripcionDepartamento", SqlDbType.NVarChar, 100)
                    .Value = (object?)persona.departamento ?? DBNull.Value;
                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    personas.Add(new Persona
                    {
                        idpersona = reader.GetInt32(reader.GetOrdinal("idpersona")),
                        nombre = reader.GetString(reader.GetOrdinal("nombre")),
                        edad = reader.GetInt32(reader.GetOrdinal("edad")),
                        correo = reader.GetString(reader.GetOrdinal("correo")),
                        iddepartamento = reader.GetInt32(reader.GetOrdinal("iddepartamento")),
                        idpuesto = reader.GetInt32(reader.GetOrdinal("idpuesto")),
                        puesto = reader.GetString(reader.GetOrdinal("puesto")),
                        departamento = reader.GetString(reader.GetOrdinal("departamento")),
                    });
                }
            }
            return personas;
        }

        public async Task<string> InsertarPersona(Persona persona)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("InsertarPersona", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", persona.nombre);
            cmd.Parameters.AddWithValue("@edad", persona.edad);
            cmd.Parameters.AddWithValue("@correo", persona.correo ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@idpuesto", persona.idpuesto);
            cmd.Parameters.AddWithValue("@iddepartamento", persona.iddepartamento);
            await conn.OpenAsync();
            await cmd.ExecuteScalarAsync();
            return "persona insertada";
        }


    }
}
