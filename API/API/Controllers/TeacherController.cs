using API.DAL;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private TeacherRepository repository;

        public TeacherController(TeacherRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/<TeacherController>
        [HttpGet]
        public IActionResult Get()
        {
            var teachers = repository.GetTeachers();
            return new OkObjectResult(teachers);
        }

        // GET api/<TeacherController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var teacher = repository.GetTeacherById(id);
            return new OkObjectResult(teacher);
        }

        // POST api/<TeacherController>
        [HttpPost]
        public IActionResult Post([FromBody] Teacher teacher)
        {
            if (teacher == null)
            {
                return new NoContentResult();
            }
            using (var scope = new TransactionScope())
            {
                repository.InsertTeacher(teacher);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = teacher.Id }, teacher);
            }
        }

        // PUT api/<TeacherController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Teacher teacher)
        {
            if (teacher == null)
            {
                return new NoContentResult();
            }
            using (var scope = new TransactionScope())
            {
                repository.UpdateTeacher(teacher);
                scope.Complete();
                return new OkResult();
            }
        }

        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            repository.DeleteTeacher(id);
            return new OkResult();
        }
    }
}
