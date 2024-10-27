namespace ClinicaDental.ApiService.DataBase;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly AppDbContext context;

    public AppointmentController(AppDbContext context)
    {
        this.context = context;
    }

    // GET: api/Appointment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
    {
        return await this.context.Appointments.ToListAsync();
    }

    // GET: api/Appointment/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(int id)
    {
        var appointment = await this.context.Appointments.FindAsync(id);

        if (appointment == null)
        {
            return this.NotFound();
        }

        return appointment;
    }

    // POST: api/Appointment
    [HttpPost]
    public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
    {
        this.context.Appointments.Add(appointment);
        await this.context.SaveChangesAsync();

        return this.CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
    }

    // PUT: api/Appointment/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
    {
        if (id != appointment.Id)
        {
            return this.BadRequest();
        }

        this.context.Entry(appointment).State = EntityState.Modified;

        try
        {
            await this.context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!this.AppointmentExists(id))
            {
                return this.NotFound();
            }
            else
            {
                throw;
            }
        }

        return this.NoContent();
    }

    // DELETE: api/Appointment/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var appointment = await this.context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return this.NotFound();
        }

        this.context.Appointments.Remove(appointment);
        await this.context.SaveChangesAsync();

        return this.NoContent();
    }

    private bool AppointmentExists(int id)
    {
        return this.context.Appointments.Any(e => e.Id == id);
    }
}