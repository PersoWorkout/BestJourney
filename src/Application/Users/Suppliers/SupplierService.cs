﻿using Domain.Abstractions;
using Domain.Users;
using Domain.Users.Requests;

namespace Application.Users.Suppliers;

public class SupplierService(IUserRepository repository) : ISupplierService
{
    private readonly IUserRepository _repository = repository;

    public async Task<Result<object>> Delete(string id)
    {
        if (!Guid.TryParse(id, out var userId))
            return Result<object>.Failure(
                UserError.NotFound);

        var user = await _repository.GetSupplierById(userId);
        if (user is null)
            return Result<object>.Failure(
                UserError.NotFound);

        await _repository.Delete(user);

        return Result<object>.Success();
    }

    public async Task<Result<IEnumerable<User>>> GetAll()
    {
        return Result<IEnumerable<User>>.Success(
            await _repository.GetSuppliers());
    }

    public async Task<Result<User>> GetById(string id)
    {
        if (!Guid.TryParse(id, out var userId))
            return Result<User>.Failure(
                UserError.NotFound);

        var user = await _repository.GetSupplierById(userId);
        if(user is null)
            return Result<User>.Failure(
                UserError.NotFound);

        return Result<User>.Success(user);
    }

    public async Task<Result<User>> Update(string id, UpdateSupplierRequest payload)
    {
        if (!Guid.TryParse(id, out var userId))
            return Result<User>.Failure(
                UserError.NotFound);

        var user = await _repository.GetSupplierById(userId);
        if (user is null)
            return Result<User>.Failure(
                UserError.NotFound);

        user.UpdateSupplier(
            payload.Firstname,
            payload.Lastname,
            payload.Email,
            payload.Password,
            payload.Phone,
            payload.CompanyName,
            payload.WebsiteName,
            payload.WebsiteUrl);

        var result = await _repository.Update(user);

        return Result<User>.Success(result);
    }
}
