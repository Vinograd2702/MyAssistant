﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Commands.Exercises.UpdateInfoExerciseType
{
    public class UpdateInfoExerciseTypeCommandHandler 
        : IRequestHandler<UpdateInfoExercisesTypeCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public UpdateInfoExerciseTypeCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(UpdateInfoExercisesTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            var entity = await _sportServiseDbContext.ExerciseTypes
                .FirstOrDefaultAsync(t => t.Id == request.Id
                && t.IsDeleted == false);

            if (entity == null)
            {
                throw new NotFoundEntityException(nameof(ExerciseType), request.Id);
            }

            if (entity.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            if (request.Name == null || request.Name == string.Empty)
            {
                throw new ArgumentException(nameof(request.Name));
            }

            var understudyNameExerciseType = await _sportServiseDbContext.ExerciseTypes
                .FirstOrDefaultAsync(t => t.UserId == request.UserId && t.Name == request.Name
                && t.IsDeleted == false && t.Id != request.Id);

            if (understudyNameExerciseType != null)
            {
                throw new NameEntityIsAlreadyUsedForThisUserException(request.Name, nameof(ExerciseType), request.UserId);
            }

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
