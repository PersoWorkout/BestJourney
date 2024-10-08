﻿using Domain.Abstractions;
using Domain.Users;
using Domain.Users.Requests;

namespace Application.Users.Suppliers;

public interface ISupplierService
{
    Task<Result<IEnumerable<User>>> GetAll();
    Task<Result<User>> GetById(string id);
    Task<Result<User>> Update(string id, UpdateSupplierRequest payload);
    Task<Result<object>> Delete(string id);
}
