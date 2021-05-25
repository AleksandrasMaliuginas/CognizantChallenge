using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CognizantChallenge;
using CognizantChallenge.Models;
using CognizantChallenge.Services;

namespace CognizantChallenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskRecordsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TaskRecordsController(DatabaseContext context)
        {
            _context = context;
        }

        // POST: TaskRecords
        [HttpPost]
        public async Task<ActionResult<TaskRecord>> PostTaskRecord(TaskRecord taskRecord)
        {
            try
            {
                var codeExecutor = new CodeExecutor();
                var result = await codeExecutor.Execute(taskRecord.SourceCode);

                if (result == null)
                {
                    throw new Exception();
                }

                taskRecord.Output = result.Output;
                taskRecord.Memory = result.Memory;
                taskRecord.CpuTime = result.CpuTime;

                _context.Tasks.Add(taskRecord);
                await _context.SaveChangesAsync();

                //return CreatedAtAction("GetTaskRecord", new { id = taskRecord.Id }, taskRecord);
                return Ok(taskRecord);
            }
            catch(Exception ex)
            {
                // Log errors

                return StatusCode(500);
            }
            
        }

        // GET: api/TaskRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskRecord>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }
    }
}
