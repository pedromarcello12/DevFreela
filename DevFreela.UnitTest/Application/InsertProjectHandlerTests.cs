using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTest.Fakes;
using FluentAssertions;
using MediatR;
using Moq;
using NSubstitute;

namespace DevFreela.UnitTest.Application
{
    public class InsertProjectHandlerTests
    {
        [Fact]
        public async Task InputDataAreOk()
        {
            // Arrange
            const int ID = 1;
            var repository = Substitute.For<IProjectRepository>();
            repository.Add(Arg.Any<Project>()).Returns(Task.FromResult(ID));
            var command = FakeDataHelper.CreateFakeInsertProjectCommand();

            var mediator = Substitute.For<IMediator>();
            var handler = new InsertProjectHandler(mediator, repository);
            // Act
            var result = await handler.Handle(command, new CancellationToken());
            // Assert
            Assert.NotNull(result);
            Assert.Equal(ID, result.Data);
            Assert.True(result.IsSuccess);
            await repository.Received(ID).Add(Arg.Any<Project>());
        }
        [Fact]
        public async Task InputDataAreOk_MOQ()
        {
            // Arrange
            const int ID = 1;
            var mock = new Mock<IProjectRepository>();
            mock.Setup(r => r.Add(It.IsAny<Project>())).ReturnsAsync(ID);
           var repository = Mock.Of<IProjectRepository>(r => r.Add(It.IsAny<Project>()) == Task.FromResult(ID));
            var command = FakeDataHelper.CreateFakeInsertProjectCommand();


            var mediator = Substitute.For<IMediator>();
            var handler = new InsertProjectHandler(mediator, mock.Object);
            // Act
            var result = await handler.Handle(command, new CancellationToken());
            // Assert
            Assert.NotNull(result);
            Assert.Equal(ID, result.Data);
            Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(ID);
            mock.Verify(r => r.Add(It.IsAny<Project>()), Times.Once);
        }
    }
}
