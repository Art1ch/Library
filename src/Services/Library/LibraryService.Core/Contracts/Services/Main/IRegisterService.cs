using LibraryService.Core.Entities;
using LibraryService.Core.Responses;

namespace LibraryService.Core.Contracts.Services.Main;

public interface IRegisterService
{
    public Task RegisterAsync(UserEntity user);
}