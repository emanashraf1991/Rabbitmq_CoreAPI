using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{   
         private readonly IMessageProducer _messageProducer;
//inmemory db
public static readonly List<Booking> bookings=new();
    public BookingsController(  IMessageProducer messageProducer)
    {
         _messageProducer=messageProducer;
    }

    [HttpPost(Name = "CreateBooking")]
    public IActionResult CreateBooking(Booking newBooking)
    { 
        if(!ModelState.IsValid)
            return BadRequest();

        bookings.Add(newBooking);
_messageProducer.SendingMessages<Booking>(newBooking);
return Ok();
    }
}
