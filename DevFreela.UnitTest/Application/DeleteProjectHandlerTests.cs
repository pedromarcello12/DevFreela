using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTest.Fakes;
using NSubstitute;

namespace DevFreela.UnitTest.Application
{
    public class DeleteProjectHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Delete_Sucess_NSubstitute()
        {
            // Arrange

            var project = FakeDataHelper.CreateFakeProjectV2();
            var projectRepository = Substitute.For<IProjectRepository>();
            projectRepository.GetById(Arg.Any<int>()).Returns((Project?)project);

            projectRepository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var command = new DeleteProjectCommand(1);
            var handler = new DeleteProjectHandler(projectRepository);
            // Act
            var result = await handler.Handle(command, new CancellationToken());
            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            await projectRepository.Received(1).GetById(1);
        }
        [Fact]
        public async Task ProjectNotExists_Delete_Fail_NSubstitute()
        {
            // Arrange
            var projectRepository = Substitute.For<IProjectRepository>();
            projectRepository.GetById(Arg.Any<int>()).Returns((Project?)null);
            var command = new DeleteProjectCommand(1);
            var handler = new DeleteProjectHandler(projectRepository);
            // Act
            var result = await handler.Handle(command, new CancellationToken());
            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            await projectRepository.Received(1).GetById(1);
        }
    }
}
