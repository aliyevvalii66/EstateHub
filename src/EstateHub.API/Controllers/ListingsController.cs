using EstateHub.Application.Features.Listings.Commands.ApproveListing;
using EstateHub.Application.Features.Listings.Commands.CreateListing;
using EstateHub.Application.Features.Listings.Commands.RejectListing;
using EstateHub.Application.Features.Listings.Queries.GetAllListings;
using EstateHub.Application.Features.Listings.Queries.GetListingById;
using EstateHub.Application.Features.Listings.Queries.GetPendingListings;
using EstateHub.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EstateHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ListingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllListingsQuery query)
    {
        var result = await _mediator.Send(query);
        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return Ok(result.Data);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetListingByIdQuery(id));
        return Ok(result.Data);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateListingCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();

        var commandWithOwner = command with { OwnerId = Guid.Parse(userId) };

        var result = await _mediator.Send(commandWithOwner);
        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, new { id = result.Data });
    }

    [HttpGet("pending")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> GetPending([FromQuery] GetPendingListingsQuery query)
    {
        var result = await _mediator.Send(query);
        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return Ok(result.Data);
    }

    [HttpPut("{id:guid}/approve")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Approve(Guid id)
    {
        var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminId == null)
            return Unauthorized();

        var result = await _mediator.Send(new ApproveListingCommand
        {
            ListingId = id,
            AdminId = Guid.Parse(adminId)
        });

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return Ok(new { message = "Elan uğurla təsdiqləndi." });
    }

    [HttpPut("{id:guid}/reject")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Reject(Guid id, [FromBody] RejectListingCommand command)
    {
        var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminId == null)
            return Unauthorized();

        var commandWithAdmin = command with
        {
            ListingId = id,
            AdminId = Guid.Parse(adminId)
        };

        var result = await _mediator.Send(commandWithAdmin);
        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return Ok(new { message = "Elan rədd edildi." });
    }
}