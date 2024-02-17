using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUygulamaProje1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbiController : ControllerBase
    {
        // GET: api/<AbiController>                                                //READ 
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AbiController>/5                                              //READ
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AbiController>                                         //CREATE
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AbiController>/5
        [HttpPut("{id}")]                                                  //UPDATE
        public void Put(int id, [FromBody] string value)
        {
        }
            
        // DELETE api/<AbiController>/5                                      //DELETE           
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
