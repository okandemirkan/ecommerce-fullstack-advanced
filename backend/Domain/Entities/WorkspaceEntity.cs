using Domain.Exceptions;

namespace Domain.Entities
{
    public interface IWorkspaceEntity
    {
        Guid? WorkspaceId { get; }
        void AssignToWorkspace(Guid workspaceId);
    }

    public abstract class WorkspaceEntity<T> : BaseEntity<T>, IWorkspaceEntity
    {
        public Guid? WorkspaceId { get; private set; }

        public void AssignToWorkspace(Guid workspaceId)
        {
            if (workspaceId == Guid.Empty)
                throw new DomainException("Workspace id cannot be empty.");
            if (WorkspaceId.HasValue && WorkspaceId.Value != workspaceId)
                throw new DomainException("Entity already belongs to another workspace.");

            WorkspaceId = workspaceId;
        }
    }
}
