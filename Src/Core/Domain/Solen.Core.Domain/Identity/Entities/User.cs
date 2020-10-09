using System;
using System.Collections.Generic;
using System.Linq;
using Solen.Core.Domain.Common;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;


namespace Solen.Core.Domain.Identity.Entities
{
    public class User
    {
        private readonly List<UserRole> _userRoles = new List<UserRole>();
        private readonly List<LearnerCompletedLecture> _completedLectures = new List<LearnerCompletedLecture>();

        # region Constructors

        private User()
        {
        }

        public User(string email, string organizationId)
        {
            Id = UserNewId;
            Email = email.ToLower();
            OrganizationId = organizationId;
            CreationDate = DateTime.Now;
            UserStatusName = PendingStatus.Instance.Name;
        }

        #endregion

        #region Properties

        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public string OrganizationId { get; private set; }
        public Organization Organization { get; private set; }
        public string InvitedBy { get; private set; }
        public string InvitationToken { get; private set; }
        public string PasswordToken { get; private set; }
        public string LearningPathId { get; private set; }
        public LearningPath LearningPath { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string UserStatusName { get; private set; }
        public UserStatus UserStatus => Enumeration.FromName<UserStatus>(UserStatusName);
        public virtual IEnumerable<UserRole> UserRoles => _userRoles.AsReadOnly();
        public IList<Course> CreatedCourses { get; private set; } = new List<Course>();
        public IEnumerable<LearnerCompletedLecture> CompletedLectures => _completedLectures.AsReadOnly();

        #endregion

        #region Public Methods

        public virtual void SetInvitedBy(string invitedBy)
        {
            if (InvitedBy == null)
                InvitedBy = invitedBy;
        }

        public virtual void UpdateUserName(string userName)
        {
            UserName = userName;
        }

        public virtual void UpdateLearningPath(LearningPath learningPath)
        {
            LearningPathId = learningPath?.Id;
            LearningPath = learningPath;
        }


        public virtual void UpdatePassword(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public virtual void SetInvitationToken(string token)
        {
            InvitationToken = token;
        }

        public virtual void InitInvitationToken()
        {
            InvitationToken = null;
        }

        public virtual void SetPasswordToken(string token)
        {
            PasswordToken = token;
        }

        public virtual void InitPasswordToken()
        {
            PasswordToken = null;
        }

        public virtual void ChangeUserStatus(UserStatus userStatus)
        {
            UserStatusName = userStatus.Name;
        }

        public virtual void AddRoleId(string roleId)
        {
            if (UserRoles.All(x => x.RoleId != roleId))
                _userRoles.Add(new UserRole(Id, roleId));
        }

        public virtual void AddRole(Role role)
        {
            if (role != null && UserRoles.All(x => x.RoleId != role.Id))
                _userRoles.Add(new UserRole(Id, role));
        }

        public virtual void RemoveRoleById(string roleId)
        {
            var userRole = UserRoles.SingleOrDefault(x => x.RoleId == roleId);
            if (userRole != null)
                _userRoles.Remove(userRole);
        }

        #endregion

        #region Private Methods

        private static string UserNewId => new Random().Next(0, 999999999).ToString("D9");

        #endregion
    }
}