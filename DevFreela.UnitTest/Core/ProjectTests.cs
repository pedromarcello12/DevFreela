using Bogus;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.UnitTest.Fakes;

namespace DevFreela.UnitTest.Core
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectIsCreated_Start_Sucess()
        {
            // Arrange
            var project = new Project(
                "Projeto A",
                "Descrição do projeto",
                1,
                2,
                1000m
            );
            
            // Act
            project.Start();


            // Assert
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
            Assert.Null(project.CompletedAt);
            Assert.True(project.Status == ProjectStatusEnum.InProgress);
            Assert.False(project.Status == ProjectStatusEnum.Created);

        }
        [Fact]
        public void ProjectIsCreated_Start_ThrowsInvalidOperationException()
        {
            // Arrange
            var project = new Project(
                "Projeto A",
                "Descrição do projeto",
                1,
                2,
                1000m
            );
            // Act
            project.Start();
            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() => project.Start());
            Assert.Equal(Project.INVALID_STATUS_MESSAGE, exception.Message);
        }
        [Fact]
        public void ProjectIsInProgress_Complete_Sucess()
        {
            // Arrange
            var project = new Project(
                "Projeto A",
                "Descrição do projeto",
                1,
                2,
                1000m
            );
            // Act
            project.Start();
            project.Complete();
            // Assert
            Assert.Equal(ProjectStatusEnum.Completed, project.Status);
            Assert.NotNull(project.StartedAt);
            Assert.NotNull(project.CompletedAt);
            Assert.True(project.Status == ProjectStatusEnum.Completed);
            Assert.False(project.Status == ProjectStatusEnum.InProgress);
        }
        [Fact]
        public void ProjectIsCreated_Complete_ThrowsInvalidOperationException()
        {
            // Arrange
            var project = new Project(
                "Projeto A",
                "Descrição do projeto",
                1,
                2,
                1000m
            );
            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => project.Complete());
            Assert.Equal(Project.INVALID_STATUS_MESSAGE, exception.Message);
        }
        [Fact]
        public void ProjectIsInProgress_Cancel_Sucess()
        {
            // Arrange
            var project = new Project(
                "Projeto A",
                "Descrição do projeto",
                1,
                2,
                1000m
            );
            // Act
            project.Start();
            project.Cancel();
            // Assert
            Assert.Equal(ProjectStatusEnum.Cancelled, project.Status);
            Assert.NotNull(project.StartedAt);
            Assert.Null(project.CompletedAt);
            Assert.True(project.Status == ProjectStatusEnum.Cancelled);
            Assert.False(project.Status == ProjectStatusEnum.InProgress);
        }
        [Fact]
        public void ProjectIsCreated_Cancel_NoChangeInStatus()
        {
            // Arrange
            var project = FakeDataHelper.CreateFakeProjectV1();
            // Act
            project.Cancel();
            // Assert
            Assert.Equal(ProjectStatusEnum.Created, project.Status);
            Assert.Null(project.StartedAt);
            Assert.Null(project.CompletedAt);
            Assert.True(project.Status == ProjectStatusEnum.Created);
            Assert.False(project.Status == ProjectStatusEnum.Cancelled);
        }
        [Fact]
        public void ProjectIsSuspended_Cancel_Sucess()
        {
            // Arrange
            var project = new Project(
                "Projeto A",
                "Descrição do projeto",
                1,
                2,
                1000m
            );
            // Act
            project.Start();
            // Simulate suspension
            typeof(Project).GetProperty("Status")!.SetValue(project, ProjectStatusEnum.Suspended);
            project.Cancel();
            // Assert
            Assert.Equal(ProjectStatusEnum.Cancelled, project.Status);
            Assert.NotNull(project.StartedAt);
            Assert.Null(project.CompletedAt);
            Assert.True(project.Status == ProjectStatusEnum.Cancelled);
            Assert.False(project.Status == ProjectStatusEnum.Suspended);
        }
        [Fact]
        public void ProjectIsSuspended_Unsuspended()
        {
            // Arrange
            var project = new Project(
                "Projeto A",
                "Descrição do projeto",
                1,
                2,
                1000m
            );
            // Act
            project.Start();
            // Simulate suspension
            typeof(Project).GetProperty("Status")!.SetValue(project, ProjectStatusEnum.Suspended);
            // Simulate unsuspension
            typeof(Project).GetProperty("Status")!.SetValue(project, ProjectStatusEnum.InProgress);
            // Assert
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
            Assert.Null(project.CompletedAt);
            Assert.True(project.Status == ProjectStatusEnum.InProgress);
            Assert.False(project.Status == ProjectStatusEnum.Suspended);
        }
        [Fact]
        public void ProjectIsInProgress_PaymentPending_Complete_Sucess()
        {
            // Arrange
            var project = new Project(
                "Projeto A",
                "Descrição do projeto",
                1,
                2,
                1000m
            );
            // Act
            project.Start();
            // Simulate payment pending
            typeof(Project).GetProperty("Status")!.SetValue(project, ProjectStatusEnum.PaymentPending);
            project.Complete();
            // Assert
            Assert.Equal(ProjectStatusEnum.Completed, project.Status);
            Assert.NotNull(project.StartedAt);
            Assert.NotNull(project.CompletedAt);
            Assert.True(project.Status == ProjectStatusEnum.Completed);
            Assert.False(project.Status == ProjectStatusEnum.PaymentPending);

        }
        [Fact]
        public void ProjectIsPaymentPending_Start_ThrowsInvalidOperationException()
        {
            // Arrange
            var project = new Project(
                "Projeto A",
                "Descrição do projeto",
                1,
                2,
                1000m
            );
            // Act
            project.Start();
            // Simulate payment pending
            typeof(Project).GetProperty("Status")!.SetValue(project, ProjectStatusEnum.PaymentPending);
            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() => project.Start());
            Assert.Equal(Project.INVALID_STATUS_MESSAGE, exception.Message);
        }
        [Fact]
        public void ProjectIsPaymentPending_Cancel_NoChangeInStatus()
        {
            // Arrange
            var project = new Project(
                "Projeto A",
                "Descrição do projeto",
                1,
                2,
                1000m
            );
            // Act
            project.Start();
            // Simulate payment pending
            typeof(Project).GetProperty("Status")!.SetValue(project, ProjectStatusEnum.PaymentPending);
            project.Cancel();
            // Assert
            Assert.Equal(ProjectStatusEnum.PaymentPending, project.Status);
            Assert.NotNull(project.StartedAt);
            Assert.Null(project.CompletedAt);
            Assert.True(project.Status == ProjectStatusEnum.PaymentPending);
            Assert.False(project.Status == ProjectStatusEnum.Cancelled);
        }
    }
}
