namespace Solen.Core.Application.Common.Security
{
    public interface ICurrentUserAccessor
    {
        string UserId { get; }
        string Username { get; }
        string UserEmail { get; }
        string OrganizationId { get; }
        string LearningPathId { get; }
    }
}