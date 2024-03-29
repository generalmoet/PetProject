﻿using Core.Application.Exceptions;
using Core.Application.Interfaces;
using MediatR;

namespace Core.Application.Users.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserDbContext _context;

    public DeleteUserCommandHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken = default)
    {
        var user = _context.Users.Find(new object[] { request.Id });

        if (user == null) throw new UserNotFoundException("User not found");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
