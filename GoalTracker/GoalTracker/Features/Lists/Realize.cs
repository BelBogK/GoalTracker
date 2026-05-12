using GoalTracker.Data;
using GoalTracker.Domain.Entities;
using GoalTracker.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoalTracker.Features.Lists
{
    public record GetDoItListsQuery(string UserId ) : IRequest<IEnumerable<DoItListDTO>>;

    public class GetDoItListsQueryHandler
        : IRequestHandler<GetDoItListsQuery, IEnumerable<DoItListDTO>>
    {
        private readonly ApplicationDbContext _context;

        public GetDoItListsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoItListDTO>> Handle(
            GetDoItListsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.DoItLists
                .Include(x=>x.Items)
                .Select(x => new DoItListDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Items=x.Items.Select(i=> new DoItListItemDTO()
                    {
                        Description=i.Description,
                        DoItListId=i.DoItListId,
                        Id=i.Id,
                        IsFinished=i.IsFinished,
                        Link=i.Link,
                        Name=i.Name
                    }).ToList()
                }) 
                .ToListAsync(cancellationToken);
        }
    }
    public record GetDoItListByIdQuery(int Id, string UserId) : IRequest<DoItListDTO?>;

    public class GetDoItListByIdQueryHandler
        : IRequestHandler<GetDoItListByIdQuery, DoItListDTO?>
    {
        private readonly ApplicationDbContext _context;

        public GetDoItListByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DoItListDTO?> Handle(
            GetDoItListByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.DoItLists
                .Where(x => x.Id == request.Id)
                .Select(x => new DoItListDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
    public record CreateDoItListCommand(DoItListDTO DTO, string UserId) : IRequest<int>;

    public class CreateDoItListCommandHandler
        : IRequestHandler<CreateDoItListCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CreateDoItListCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(
            CreateDoItListCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new DoItList
            {
                Name = request.DTO.Name,
                Description = request.DTO.Description,
                UserId=request.UserId
            };
            _context.DoItLists.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
    public record UpdateDoItListCommand(int Id, DoItListDTO DTO, string UserId) : IRequest;

    public class UpdateDoItListCommandHandler
        : IRequestHandler<UpdateDoItListCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateDoItListCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(
            UpdateDoItListCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.DoItLists
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new Exception("DoItList not found");

            entity.Name = request.DTO.Name;
            entity.Description = request.DTO.Description;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
    public record DeleteDoItListCommand(int Id, string UserId) : IRequest;

    public class DeleteDoItListCommandHandler
        : IRequestHandler<DeleteDoItListCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteDoItListCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(
            DeleteDoItListCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.DoItLists
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new Exception("DoItList not found");

            _context.DoItLists.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public record AddDoItListItemCommand(
         string UserId,
    int ListId,   
    DoItListItemDTO DTO) : IRequest<int>;

    public class AddDoItListItemCommandHandler
        : IRequestHandler<AddDoItListItemCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public AddDoItListItemCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(
            AddDoItListItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new DoItListItem
            {
                DoItListId = request.ListId,
                Name = request.DTO.Name,
                IsFinished = request.DTO.IsFinished,
                UserId = request.UserId
            };

            _context.DoItListItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
    public record UpdateDoItListItemCommand(
         string UserId,
    int ListId,
    int ItemId,
    DoItListItemDTO DTO) : IRequest;

    public class UpdateDoItListItemCommandHandler
        : IRequestHandler<UpdateDoItListItemCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateDoItListItemCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(
            UpdateDoItListItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.DoItListItems
                .FirstOrDefaultAsync(
                    x => x.Id == request.ItemId &&
                         x.DoItListId == request.ListId,
                    cancellationToken);

            if (entity is null)
                throw new Exception("DoItListItem not found");

            entity.Name = request.DTO.Name;
            entity.IsFinished = request.DTO.IsFinished;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public record DeleteDoItListItemCommand(
         string UserId,
    int ListId,
    int ItemId) : IRequest;

    public class DeleteDoItListItemCommandHandler
        : IRequestHandler<DeleteDoItListItemCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteDoItListItemCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(
            DeleteDoItListItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.DoItListItems
                .FirstOrDefaultAsync(
                    x => x.Id == request.ItemId &&
                         x.DoItListId == request.ListId,
                    cancellationToken);

            if (entity is null)
                throw new Exception("DoItListItem not found");

            _context.DoItListItems.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
