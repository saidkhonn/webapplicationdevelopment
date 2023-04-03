using API.DAL;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly SubjectRepository repository;

        public SubjectController(SubjectRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Subject
        [HttpGet]
        public IActionResult Get()
        {
            var subjects = repository.GetGategories();
            return new OkObjectResult(subjects);
        }

        // GET api/<SubjectController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var subject = repository.GetSubjectById(id);
            return new OkObjectResult(subject);
        }

        // POST api/<SubjectController>
        [HttpPost]
        public IActionResult Post([FromBody] Subject subject)
        {
            if (subject == null)
            {
                return new NoContentResult();
            }
            using (var scope = new TransactionScope())
            {
                repository.InsertSubject(subject);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = subject.Id }, subject);
            }
        }

        // PUT api/<SubjectController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Subject subject)
        {
            if (subject == null)
            {
                return new NoContentResult();
            }
            using (var scope = new TransactionScope())
            {
                repository.UpdateSubject(subject);
                scope.Complete();
                return new OkResult();
            }
        }

        // DELETE api/<SubjectController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            repository.DeleteSubject(id);
            return new OkResult();
        }
    }
}
