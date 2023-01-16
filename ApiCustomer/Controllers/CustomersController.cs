using Microsoft.AspNetCore.Mvc;
using ApiCustomer.Services;
namespace ApiCustomer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _Service;

        public CustomersController(ICustomerService service)
        {
            _Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost(Name = "PostCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Customer body)
        {
            try
            {
                var Id = _Service.AddCustomer(body);
                return Created("New user created. User Id: ", Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{Id}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] long Id)
        {
            try
            {
                _Service.DeleteCustomer(Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{Id}", Name = "PutCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put([FromRoute] long Id, [FromBody] Customer customer)
        {
            try
            {
                _Service.UpdateCustomer(Id, customer);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpGet(Name = "GetCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<Customer> Get()
        {
            return _Service.GetCustomers();
        }

        [HttpGet("{Id}", Name = "GetCustomerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerById([FromRoute] long Id)
        {
            try
            {
                return Ok(_Service.GetCustomerById(Id));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    };

}