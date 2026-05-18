using GoalTracker.Domain.Interfaces.Repositories;
using GoalTracker.Shared;
using MediatR;

namespace GoalTracker.Features.DayComment
{ 
    public record GetDayCommentsQuery(string UserId, DateOnly StartDate, DateOnly EndDate) : IRequest<List<DayCommentDTO>>;
     
    public class GetDayCommentsHandler : IRequestHandler<GetDayCommentsQuery, List<DayCommentDTO>>
    {
        private readonly IDayCommentRepository _repository;

        public GetDayCommentsHandler(IDayCommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DayCommentDTO>> Handle(GetDayCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _repository.GetCommentsForPeriodAsync(request.UserId, request.StartDate, request.EndDate);

            return comments.Select(c => new DayCommentDTO
            {
                Date = c.Date,
                Comment = c.Comment,
                DayScore = c.DayScore
            }).ToList();
        }
     
    public record SaveDayCommentCommand(string UserId, DayCommentDTO Dto) : IRequest; 
    public class SaveDayCommentHandler : IRequestHandler<SaveDayCommentCommand>
    {
        private readonly IDayCommentRepository _repository;

        public SaveDayCommentHandler(IDayCommentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(SaveDayCommentCommand request, CancellationToken cancellationToken)
        {
            await _repository.SaveCommentAsync(request.UserId, request.Dto);
        }
    }
}
}
