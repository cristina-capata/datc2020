using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Student> Get()
        {
           return StudentRepo.Studenti;
        }

        [HttpGet("{id}")]
        public Student GetStudenti([FromRoute] int id)
        {
            return StudentRepo.Studenti.FirstOrDefault(s=> s.ID==id);
        }

        [HttpPost]
        public string Post([FromBody] Student student) {
            try{
                StudentRepo.Studenti.Add(student);
                return "Adaugare realizata cu succes!";
            }
            catch (SystemException e)
            {
                return "Eroare la adaugare: "+e.Message;
                throw;
            }
        }

        [HttpDelete("{id}")]
        public string Delete([FromRoute] int id){
            try
            {
                StudentRepo.Studenti.RemoveAll(s => s.ID ==id);
                return "Stergere realizata cu succes!";
            }
            catch (SystemException e)
            {
                return "Eroare la adaugare: "+e.Message;
                throw;
            }
        }
    }
}
