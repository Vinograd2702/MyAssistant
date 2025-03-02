using MediatR;
using Microsoft.EntityFrameworkCore;
using sports_service.Core.Application.Common.Exceptions;
using sports_service.Core.Application.Interfaces.Repositories;
using sports_service.Core.Domain.Workouts;

namespace sports_service.Core.Application.Commands.Workouts.UpdateWorkoutNote
{
    public class UpdateWorkoutNoteCommandHandler
        : IRequestHandler<UpdateWorkoutNoteCommand>
    {
        private readonly ISportServiseDbContext _sportServiseDbContext;

        public UpdateWorkoutNoteCommandHandler(ISportServiseDbContext sportServiseDbContext)
        {
            _sportServiseDbContext = sportServiseDbContext;
        }

        public async Task Handle(UpdateWorkoutNoteCommand request, 
            CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }

            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(request.Id));
            }

            var workoutEntity = await _sportServiseDbContext.Workouts
                .FirstOrDefaultAsync(w => w.Id == request.Id);

            if (workoutEntity == null)
            {
                throw new NotFoundEntityException(nameof(Workout),
                    request.Id);
            }

            if (workoutEntity.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            workoutEntity.Note = request.NewNote;

            await _sportServiseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
