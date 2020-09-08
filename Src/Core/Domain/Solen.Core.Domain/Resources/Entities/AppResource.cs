using System;
using Solen.Core.Domain.Common;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace Solen.Core.Domain.Resources.Entities
{
    public class AppResource
    {
        private AppResource()
        {
        }

        public AppResource(string id, string organizationId, string creatorName, ResourceType resourceType, long size)
        {
            Id = id;
            OrganizationId = organizationId;
            CreatorName = creatorName;
            ResourceTypeName = resourceType.Name;
            Size = size;
            CreationDate = DateTime.Now;
        }
        
        public string Id { get; private set; }
        public string OrganizationId { get; private set; }
        public string CreatorName { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string ResourceTypeName { get; private set; }
        public long Size { get; private set; }
        public bool ToDelete { get; private set; }
        public ResourceType ResourceType => Enumeration.FromName<ResourceType>(ResourceTypeName);
        public virtual void MarkToDelete()
        {
            ToDelete = true;
        }
    }
}