using apps_hub.DTO;
using apps_hub.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace apps_hub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordController : ControllerBase
    {

        protected readonly DBContext DBContext;

        public RecordController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetRecords")]
        public async Task<ActionResult<List<RecordDTO>>> Get()
        {
            var ListData = await  DBContext.TempTables.Select(
                s => new RecordDTO
                {
                    id = s.id,
                    title = s.title,
                    img = s.img,
                    link = s.link,
                }).ToListAsync();

            if (ListData.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return ListData;
            }
        }

        [HttpGet("GetLinkById/{id}")]
        public async Task <ActionResult<RecordDTO>> GetUserById(int id)
        {
            RecordDTO Record = await DBContext.TempTables.Select(
                s => new RecordDTO
                {
                    id = s.id,
                    title = s.title,
                    img = s.img,
                    link = s.link,
                }).FirstOrDefaultAsync(s => s.id == id);
            if (Record == null)
            {
                return NotFound();
            } else { 
                return Record; 
            }
        }

        [HttpPost("InsertRecord")]
        public async Task < HttpStatusCode > InsertRecord(RecordDTO Record)
        {
            var entity = new TempTable()
            {
                id = Record.id,
                title = Record.title,
                img = Record.img,
                link = Record.link,
            };

            DBContext.TempTables.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateRecord")]
        public async Task<HttpStatusCode> UpdateRecord(RecordDTO Record)
        {
            var entity = await DBContext.TempTables.FirstOrDefaultAsync(s=>s.id == Record.id);
            entity.title = Record.title;
            entity.img = Record.img;
            entity.link = Record.link;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteRecord/{id}")]
        public async Task < HttpStatusCode > DeleteRecord(int id)
        {
            var entity = new TempTable()
            {
                id = id
            };
            DBContext.TempTables.Attach(entity);
            DBContext.TempTables.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

    }
}
