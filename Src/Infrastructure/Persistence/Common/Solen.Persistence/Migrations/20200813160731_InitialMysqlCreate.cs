using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solen.Persistence.Migrations
{
    public partial class InitialMysqlCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppResources",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    OrganizationId = table.Column<string>(nullable: true),
                    CreatorName = table.Column<string>(maxLength: 100, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ResourceTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    Size = table.Column<long>(nullable: false),
                    ToDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    TypeName = table.Column<string>(maxLength: 50, nullable: true),
                    NotificationEventName = table.Column<string>(maxLength: 50, nullable: true),
                    TemplateSubject = table.Column<string>(maxLength: 100, nullable: true),
                    TemplateBody = table.Column<string>(nullable: true),
                    IsSystemNotification = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationSigningUps",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Token = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationSigningUps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    MaxStorage = table.Column<long>(nullable: false),
                    MaxFileSize = table.Column<long>(nullable: false),
                    MaxUsers = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseResources",
                columns: table => new
                {
                    CourseId = table.Column<string>(maxLength: 127, nullable: false),
                    ModuleId = table.Column<string>(maxLength: 127, nullable: false),
                    LectureId = table.Column<string>(maxLength: 127, nullable: false),
                    ResourceId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseResources", x => new { x.CourseId, x.ModuleId, x.LectureId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_CourseResources_AppResources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "AppResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    SubscriptionPlanId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DisabledNotificationTemplates",
                columns: table => new
                {
                    OrganizationId = table.Column<string>(nullable: false),
                    NotificationTemplateId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisabledNotificationTemplates", x => new { x.OrganizationId, x.NotificationTemplateId });
                    table.ForeignKey(
                        name: "FK_DisabledNotificationTemplates_NotificationTemplates_Notifica~",
                        column: x => x.NotificationTemplateId,
                        principalTable: "NotificationTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisabledNotificationTemplates_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningPaths",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    OrganizationId = table.Column<string>(nullable: false),
                    IsDeletable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningPaths_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    UserName = table.Column<string>(maxLength: 30, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    OrganizationId = table.Column<string>(nullable: false),
                    InvitedBy = table.Column<string>(maxLength: 50, nullable: true),
                    InvitationToken = table.Column<string>(nullable: true),
                    PasswordToken = table.Column<string>(nullable: true),
                    LearningPathId = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UserStatusName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_LearningPaths_LearningPathId",
                        column: x => x.LearningPathId,
                        principalTable: "LearningPaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 60, nullable: false),
                    Subtitle = table.Column<string>(maxLength: 120, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CourseStatusName = table.Column<string>(maxLength: 50, nullable: true),
                    CreatorId = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    PublicationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    NotificationEvent = table.Column<string>(nullable: true),
                    RecipientId = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(maxLength: 250, nullable: true),
                    Body = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    ExpiryTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseLearnedSkill",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 127, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    CourseId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLearnedSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseLearnedSkill_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnerCourseAccessTimes",
                columns: table => new
                {
                    LearnerId = table.Column<string>(nullable: false),
                    CourseId = table.Column<string>(nullable: false),
                    AccessDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnerCourseAccessTimes", x => new { x.LearnerId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_LearnerCourseAccessTimes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnerCourseAccessTimes_Users_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningPathCourses",
                columns: table => new
                {
                    LearningPathId = table.Column<string>(nullable: false),
                    CourseId = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPathCourses", x => new { x.LearningPathId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_LearningPathCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearningPathCourses_LearningPaths_LearningPathId",
                        column: x => x.LearningPathId,
                        principalTable: "LearningPaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    CourseId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 127, nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 60, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    LectureTypeName = table.Column<string>(maxLength: 50, nullable: false),
                    ModuleId = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnerCompletedLectures",
                columns: table => new
                {
                    LearnerId = table.Column<string>(nullable: false),
                    LectureId = table.Column<string>(nullable: false),
                    CompletionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnerCompletedLectures", x => new { x.LearnerId, x.LectureId });
                    table.ForeignKey(
                        name: "FK_LearnerCompletedLectures_Users_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnerCompletedLectures_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnerLectureAccessHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LearnerId = table.Column<string>(nullable: false),
                    LectureId = table.Column<string>(nullable: false),
                    AccessDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnerLectureAccessHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnerLectureAccessHistories_Users_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnerLectureAccessHistories_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NotificationTemplates",
                columns: new[] { "Id", "IsSystemNotification", "NotificationEventName", "TemplateBody", "TemplateSubject", "TypeName" },
                values: new object[,]
                {
                    { "CoursePublishedEvent-Email", false, "CoursePublishedEvent", "Hi,<br/> <br/>The course «{{data.course_name}}» has just been published by {{data.creator_name}}.<br/> <br/>Enjoy your training course.<br/> <br/>", "A course has been published !", "Email" },
                    { "CoursePublishedEvent-Push", false, "CoursePublishedEvent", "Hi,<br/> <br/>The course «{{data.course_name}}» has just been published by {{data.creator_name}}.<br/> <br/>Enjoy your training course.<br/> <br/>", "A course has been published !", "Push" },
                    { "OrganizationSigningUpInitializedEvent-Email", true, "OrganizationSigningUpInitializedEvent", "Hi,<br/> <br/>Please click on the link below to complete the signing up process :<br/> <br/>{{ data.link_to_complete_signing_up }}<br/> <br/>", "Verify your email and complete your registration", "Email" },
                    { "OrganizationSigningUpCompletedEvent-Email", true, "OrganizationSigningUpCompletedEvent", "Hi {{data.user_name}},<br/> <br/>Thank you for your interest in Solen LMS.Your account has been successfully created.<br/>It’s great to have you here! We hope you and your learners will enjoy using this platform.<br/> <br/>", "Welcome to Solen LMS!", "Email" },
                    { "PasswordForgottenEvent-Email", true, "PasswordForgottenEvent", "Hi,<br/> <br/>This is a system message in reply to your request to change your password.<br/>To reset your password, please open the link below and follow the instructions on the page : <br/> <br/>{{ data.link_to_reset_password }}<br/> <br/>", "Reset Password", "Email" },
                    { "UserSigningUpCompletedEvent-Email", true, "UserSigningUpCompletedEvent", "Hi {{data.user_name}},<br/> <br/>Welcome to  Solen LMS ! Your account has been successfully created.<br/>Enjoy your learning journey !<br/> <br/>", "Welcome to Solen LMS!", "Email" },
                    { "UserSigningUpInitializedEvent-Email", true, "UserSigningUpInitializedEvent", "Hi,<br/> <br/>{{data.invited_by}} has invited you to join Solen LMS.<br/>Please click on the link below to complete the signing up process :<br/> <br/>{{ data.link_to_complete_signing_up }}<br/> <br/>", "{{data.invited_by}} has invited you to join Solen LMS", "Email" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "Admin", "Has all controls and access.", "Admin" },
                    { "Instructor", "Can create Learning Paths, create, publish and unpublish courses", "Instructor" },
                    { "Learner", "Can access to all courses belonging to his learning path", "Learner" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "MaxFileSize", "MaxStorage", "MaxUsers", "Name" },
                values: new object[] { "Free", 314572800L, 5368709120L, 20, "Free Plan" });

            migrationBuilder.CreateIndex(
                name: "IX_AppResources_OrganizationId",
                table: "AppResources",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLearnedSkill_CourseId",
                table: "CourseLearnedSkill",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseResources_ResourceId",
                table: "CourseResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CreatorId",
                table: "Courses",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_DisabledNotificationTemplates_NotificationTemplateId",
                table: "DisabledNotificationTemplates",
                column: "NotificationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnerCompletedLectures_LectureId",
                table: "LearnerCompletedLectures",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnerCourseAccessTimes_CourseId",
                table: "LearnerCourseAccessTimes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnerLectureAccessHistories_LearnerId",
                table: "LearnerLectureAccessHistories",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnerLectureAccessHistories_LectureId",
                table: "LearnerLectureAccessHistories",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPathCourses_CourseId",
                table: "LearningPathCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPaths_OrganizationId",
                table: "LearningPaths",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_ModuleId",
                table: "Lectures",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseId",
                table: "Modules",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecipientId",
                table: "Notifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_SubscriptionPlanId",
                table: "Organizations",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LearningPathId",
                table: "Users",
                column: "LearningPathId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseLearnedSkill");

            migrationBuilder.DropTable(
                name: "CourseResources");

            migrationBuilder.DropTable(
                name: "DisabledNotificationTemplates");

            migrationBuilder.DropTable(
                name: "LearnerCompletedLectures");

            migrationBuilder.DropTable(
                name: "LearnerCourseAccessTimes");

            migrationBuilder.DropTable(
                name: "LearnerLectureAccessHistories");

            migrationBuilder.DropTable(
                name: "LearningPathCourses");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrganizationSigningUps");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "AppResources");

            migrationBuilder.DropTable(
                name: "NotificationTemplates");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LearningPaths");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
