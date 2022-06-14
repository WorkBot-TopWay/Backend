using AutoMapper;
using TopWay.API.Security.Authorization.Handlers.Interfaces;
using TopWay.API.Security.Domain.Models;
using TopWay.API.Security.Domain.Repositories;
using TopWay.API.Security.Domain.Services;
using TopWay.API.Security.Domain.Services.Communication;
using TopWay.API.Security.Exceptions;
using TopWay.API.Shared.Domain.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;


namespace TopWay.API.Security.Services;

public class ScalerService : IScalerService
{
    private readonly IScalerRepository _scalerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;
    public ScalerService(IScalerRepository scalerRepository, IUnitOfWork unitOfWork, 
        IJwtHandler jwtHandler, IMapper mapper)
    {
        _scalerRepository = scalerRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
    }


    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request)
    {
        var user = await _scalerRepository.FindByEmailAsync(request.Email);
        // Validate
        if (user == null || !BCryptNet.Verify(request.Password, user.PasswordHash))
        {
            throw new AppExceptions("Invalid credentials");
        }
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtHandler.GenerateToken(user);
        return response;
    }

    public async Task<IEnumerable<Scaler>> ListAsync()
    {
        return await _scalerRepository.ListAsync();
    }

    public async Task<Scaler> FindByIdAsync(int id)
    {
        var user = await _scalerRepository.FindByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        return user;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        if(_scalerRepository.ExistsByEmail(request.Email))
        {
            throw new AppExceptions($"User with email {request.Email} already exists");
        }
        var user = _mapper.Map<Scaler>(request);
        user.PasswordHash = BCryptNet.HashPassword(request.Password);
        user.Type = "Scaler";
        try
        {
            await _scalerRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new AppExceptions($"An Error occurred while saving the user: {ex.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var user=await _scalerRepository.FindByIdAsync(id);
        var userWithSameEmail = await _scalerRepository.FindByEmailAsync(request.Email);
        if (userWithSameEmail != null && userWithSameEmail.Id != id)
        {
            throw new AppExceptions($"User with email {request.Email} already exists");
        }
        if(request.Password==null || request.Password=="")
        {
            request.Password=user.PasswordHash;
        }
        if(!string.IsNullOrEmpty(request.Password)&&request.Password!="")
        {
            user.PasswordHash = BCryptNet.HashPassword(request.Password);
        }
        _mapper.Map(request, user);
        try
        {
            _scalerRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new AppExceptions($"An Error occured while updating user: {ex.Message}");
        }

    }

    public async Task DeleteAsync(int id)
    {
        var user = await _scalerRepository.FindByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        try
        {
            _scalerRepository.Delete(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppExceptions($"An Error occured while deleting user: {e.Message}");
        }
    }
}