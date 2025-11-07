using Core.Features.ShippingAddresses.Commands.AddShippingAddress;
using Core.Features.ShippingAddresses.Commands.SetShippingAddress;
using Core.Features.ShippingAddresses.Commands.EditShippingAddress;
using Core.Features.ShippingAddresses.Commands.DeleteShippingAddress;
using Core.Features.ShippingAddresses.Queries.GetShippingAddressList;
using Core.Features.ShippingAddresses.Queries.GetSingleShippingAddress;

namespace API.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ShippingAddressController : AppControllerBase
    {
        [HttpGet(Router.ShippingAddressRouting.GetAll)]
        public async Task<IActionResult> GetShippingAddressList()
        {
            var response = await Mediator.Send(new GetShippingAddressListQuery());
            return Ok(response);
        }

        [HttpGet(Router.ShippingAddressRouting.GetById)]
        public async Task<IActionResult> GetShippingAddressById([FromRoute] Guid id)
        {
            return NewResult(await Mediator.Send(new GetSingleShippingAddressQuery(id)));
        }

        [HttpPost(Router.ShippingAddressRouting.Create)]
        public async Task<IActionResult> AddShippingAddress([FromForm] AddShippingAddressCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.ShippingAddressRouting.SetShippingAddress)]
        public async Task<IActionResult> SetShippingAddress([FromBody] SetShippingAddressCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.ShippingAddressRouting.Edit)]
        public async Task<IActionResult> EditShippingAddress([FromBody] EditShippingAddressCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.ShippingAddressRouting.Delete)]
        public async Task<IActionResult> DeleteShippingAddress([FromRoute] Guid id)
        {
            return NewResult(await Mediator.Send(new DeleteShippingAddressCommand(id)));
        }
    }
}
