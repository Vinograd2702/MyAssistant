﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Common.Extensions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Templates;

namespace sports_service.Core.Application.Commands.Templates.CreateTemplateWorkout
{
    public class CreateTemplateWorkoutCommandHandler 
        : IRequestHandler<CreateTemplateWorkoutCommand, Guid>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public CreateTemplateWorkoutCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task<Guid> Handle(CreateTemplateWorkoutCommand request,
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

            var understudyNameTemplate = await _sportServiseDbContext.TemplateWorkouts
                .FirstOrDefaultAsync(template => template.Name == request.Name, cancellationToken);

            if (understudyNameTemplate != null)
            {
                throw new NameEntityIsAlreadyUsedForThisUserException(request.Name, nameof(TemplateWorkout), request.UserId);
            }

            var entityTemplateWorkout = new TemplateWorkout
            {
                UserId = request.UserId,
                Name = request.Name,
                Description = request.Description,
            };

            _sportServiseDbContext.TemplateWorkouts.Add(entityTemplateWorkout);

            var entityTemplateBlockCardioList = request.TemplatesBlockCardioDTO
                .ToCore(entityTemplateWorkout);

            _sportServiseDbContext.TemplatesBlockCardio.AddRange(entityTemplateBlockCardioList);

            var entityTemplateBlockStrenghtList = request.TemplatesBlockStrenghtDTO
                .ToCore(entityTemplateWorkout);

            _sportServiseDbContext.TemplatesBlockStrenght.AddRange(entityTemplateBlockStrenghtList);


            var entityTemplateBlockSplitList = request.TemplatesBlockSplitDTO
                .ToCore(entityTemplateWorkout);

            _sportServiseDbContext.TemplatesBlockSplit.AddRange(entityTemplateBlockSplitList);


            var entityTemplateBlockWarmUpList = request.TemplatesBlockWarmUpDTO
                .ToCore(entityTemplateWorkout);

            _sportServiseDbContext.TemplatesBlockWarmUp.AddRange(entityTemplateBlockWarmUpList);


            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);

            return entityTemplateWorkout.Id;
        }
    }
}
