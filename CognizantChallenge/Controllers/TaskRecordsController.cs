using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CognizantChallenge.Models;
using CognizantChallenge.Services;

namespace CognizantChallenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskRecordsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ICodeExecutor _codeExecutor;

        public TaskRecordsController(DatabaseContext context, ICodeExecutor codeExecutor)
        {
            _context = context;
            _codeExecutor = codeExecutor;
        }

        // POST: TaskRecords
        [HttpPost]
        public async Task<ActionResult<TaskRecord>> PostTaskRecord(TaskRecord taskRecord)
        {
            try
            {
                var result = await _codeExecutor.Execute(taskRecord.SourceCode);

                if (result == null)
                {
                    throw new Exception();
                }

                taskRecord.Output = result.Output;
                taskRecord.Memory = result.Memory;
                taskRecord.CpuTime = result.CpuTime;

                if (taskRecord.Memory != null && taskRecord.CpuTime != null)
                {
                    _context.Tasks.Add(taskRecord);
                    await _context.SaveChangesAsync();
                }

                return Ok(taskRecord);
            }
            catch(Exception)
            {
                // Log errors
                return StatusCode(500);
            }
        }

        // GET: TaskRecords
        //[HttpGet]
        //public ObjectResult GetTasks()
        //{
        //    return Ok("Service is working");
        //}
    }
}
