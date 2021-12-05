using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCodeDemo.Entities;

namespace MyCodeDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanViensController : ControllerBase
    {
        private readonly eStore20Context _context;

        public NhanViensController(eStore20Context context)
        {
            _context = context;
        }

        // GET: api/NhanViens
        [HttpGet]
        public async Task<IActionResult> GetNhanVien()
        {
            return this.Ok(await _context.NhanVien.ToListAsync());
        }

        // GET: api/NhanViens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNhanVien(string id)
        {
            var nhanVien = await _context.NhanVien.FindAsync(id);

            if (nhanVien == null)
            {
                return NotFound();
            }

            return Ok(nhanVien);
        }

        // PUT: api/NhanViens/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhanVien(string id, NhanVien nhanVien)
        {
            if (id != nhanVien.MaNv)
            {
                return BadRequest();
            }

            _context.Entry(nhanVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/NhanViens
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NhanVien>> PostNhanVien(NhanVien nhanVien)
        {
            _context.NhanVien.Add(nhanVien);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NhanVienExists(nhanVien.MaNv))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNhanVien", new { id = nhanVien.MaNv }, nhanVien);
        }

        // DELETE: api/NhanViens/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NhanVien>> DeleteNhanVien(string id)
        {
            var nhanVien = await _context.NhanVien.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            _context.NhanVien.Remove(nhanVien);
            await _context.SaveChangesAsync();

            return nhanVien;
        }

        private bool NhanVienExists(string id)
        {
            return _context.NhanVien.Any(e => e.MaNv == id);
        }
    }
}
