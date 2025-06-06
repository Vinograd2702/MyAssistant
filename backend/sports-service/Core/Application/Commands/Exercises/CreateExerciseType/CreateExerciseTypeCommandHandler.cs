﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Exercises;

namespace sports_service.Core.Application.Commands.Exercises.CreateExerciseType
{
    public class CreateExerciseTypeCommandHandler
        : IRequestHandler<CreateExerciseTypeCommand, Guid>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public CreateExerciseTypeCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<Guid> Handle(CreateExerciseTypeCommand request,
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            if (request.Name == null || request.Name == string.Empty)
            {
                throw new ArgumentException(nameof(request.Name));
            }

            var understudyNameExercise = await _sportServiseDbContext.ExerciseTypes
                .FirstOrDefaultAsync(exercise => exercise.UserId == request.UserId
                && exercise.Name == request.Name && exercise.IsDeleted == false);

            if (understudyNameExercise != null && understudyNameExercise.IsDeleted != true)
            {
                throw new NameEntityIsAlreadyUsedForThisUserException(request.Name, nameof(ExerciseType), request.UserId);
            }

            var entity = new ExerciseType
            {
                UserId = request.UserId,
                Name = request.Name,
                Description = request.Description,
            };

            if (request.ExerciseGroupId != null)
            {
                var group = await _sportServiseDbContext.ExerciseGroups
                    .FirstOrDefaultAsync(group => group.Id == request.ExerciseGroupId);

                if (group == null || group.IsDeleted == true)
                {
                    throw new NotFoundEntityException(nameof(ExerciseGroup), request.ExerciseGroupId);
                }

                entity.ExerciseGroup = group;
            }

            await _sportServiseDbContext.ExerciseTypes.AddAsync(entity);
            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
