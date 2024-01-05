using BookingService.Dto;

namespace BookingService.Services.IService
{
    public interface IUser
    {
        Task<UserDto> GetUserById(Guid Id);
    }
}
