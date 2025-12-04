using Bogus;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTest.Fakes
{
    public class FakeDataHelper
    {
        private static readonly Faker _faker = new();
        public static Project CreateFakeProjectV1()
        {
            var fakeProject = new Project(
                title: _faker.Commerce.ProductName(),
                description: _faker.Lorem.Paragraph(),
                idClient: _faker.Random.Int(1, 1000),
                idFreelancer: _faker.Random.Int(1, 1000),
                totalCost: _faker.Finance.Amount(100, 10000)
            );
            return fakeProject;
        }
        private static readonly Faker<Project> _projectFaker = new Faker<Project>()
            .CustomInstantiator(faker => new Project(
                title: faker.Commerce.ProductName(),
                description: faker.Lorem.Paragraph(),
                idClient: faker.Random.Int(1, 1000),
                idFreelancer: faker.Random.Int(1, 1000),
                totalCost: faker.Finance.Amount(100, 10000)
            ));
        private static readonly Faker<InsertProjectCommand> _insertProjectCommandFaker = new Faker<InsertProjectCommand>()
            .CustomInstantiator(faker => new InsertProjectCommand(
                title: faker.Commerce.ProductName(),
                description: faker.Lorem.Paragraph(),
                idClient: faker.Random.Int(1, 1000),
                idFreelancer: faker.Random.Int(1, 1000),
                totalCost: faker.Finance.Amount(100, 10000)
            ));
        public static Project CreateFakeProjectV2() => _projectFaker.Generate();
        public static InsertProjectCommand CreateFakeInsertProjectCommand() => _insertProjectCommandFaker.Generate();
        public static DeleteProjectCommand CreateFakeDeleteProjectCommand()
        {
            var fakeId = _faker.Random.Int(1, 1000);
            return new DeleteProjectCommand(fakeId);
        }


    }
}
