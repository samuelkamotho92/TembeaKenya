using BookingService.Data;
using BookingService.Dto;
using BookingService.Models;
using BookingService.Services.IService;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace BookingService.Services
{
    public class BookingServices : IBookingService
    {
        private readonly BookingDbContext _context;
        private readonly ITour _tourService;
        public BookingServices(BookingDbContext context,ITour tourService)
        {
            _context = context;
            _tourService = tourService;
        }
        public async Task<string> AddBooking(Booking booking)
        {
            try
            {
                await  _context.bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                return "Booking added successfully";
            }catch (Exception ex)
            {
                return $"{ex.InnerException}";
            }
        }

        public async Task<string> DeleteBooking(Booking booking)
        {
            try
            {
                _context.bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return "Booking removed successfully";
            }catch(Exception ex)
            {
                return $"{ex.InnerException}";
            }
        }

        public async Task<Booking> GetABooking(Guid Id)
        {
            try
            {
              var booking = await _context.bookings.Where(x=>x.Id == Id).FirstOrDefaultAsync();
               return booking;
            }catch(Exception ex)
            {
                return new Booking();
            }
        }



        public async Task<List<Booking>> GetAllBooking(Guid UserId)
        {
          var bookings = await  _context.bookings.Where(x=>x.UserId == UserId).ToListAsync();
            return bookings;
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            var bookings = await _context.bookings.ToListAsync();
            return bookings;
        }

        public async Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto)
        {
            //get a booking and tour
            var booking = await _context.bookings.Where(x => x.Id == stripeRequestDto.BookingId).FirstOrDefaultAsync();
            Console.WriteLine(booking.TourId);
            var tour = await _tourService.GetATour(booking.TourId);
            Console.WriteLine(tour.TourName);
            var options = new SessionCreateOptions()
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };
            var item = new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions()
                {
                    UnitAmount = (long)booking.BookingTotal * 100,
                    Currency = "kes",
                    //description of what you are paying for
                    ProductData = new SessionLineItemPriceDataProductDataOptions()
                    {
                        Name = tour.TourName,
                        Description = tour.TourDescription,
                        Images = new List<string> { "https://www.nairobinationalparkkenya.com/wp-content/uploads/2023/03/230020_627ab8d6a6bb8.jpg" }
                    }
                },
                Quantity = 1
            };
            options.LineItems.Add(item);
            //Apply discount
            var DiscountObj = new List<SessionDiscountOptions>()
            {
                new SessionDiscountOptions()
                {
                    Coupon = booking.CouponCode
                }
            };
            if(booking.Discount > 0)
            {//Add Discount if disc is greate than 0
                options.Discounts = DiscountObj;
            }
            //if no discount
            var service = new SessionService();
            Session session = service.Create(options);
            //redirects
            stripeRequestDto.StripeSessionUrl = session.Url;
            stripeRequestDto.StripeSessionId = session.Id;
            //update db
            booking.StripeSessionId = session.Id;
            booking.Status = "Ongoing";
            await _context.SaveChangesAsync();
            return stripeRequestDto;
        }

        public async Task saveChanges()
        {
            await _context.SaveChangesAsync();
        }

            public Task<string> UpdateBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task<bool> validatePayments(Guid BookingId)
        {
            throw new NotImplementedException();
        }
    }
}
