using System;
using Moq;
using NUnit.Framework;
using Worms.Action;
using Worms.GameModel;
using Worms.Services;

namespace TestWorms
{
    [TestFixture]
    public class BehaviorTest
    {
        [Test]
        public void MoveToVoid()
        {
            var logStub = new Mock<ISimpleLogger>();
            logStub.Setup(logger => logger.LogInfo(It.IsAny<Exception>(),
                It.IsAny<String>(), It.IsAny<object[]>()));

            WorldSimulator simulator = new WorldSimulator(new FoodGenerator(), logStub.Object);

            var stabBehavior = Mock.Of<IBehavior>(fb =>
                fb.GetAction(It.IsAny<Worm>(), It.IsAny<WorldState>()) == new ActionMove(Direction.Left));

            WorldState worldState = new WorldState(new NameGenerator(), stabBehavior);
            Worm testWorm = new Worm("Test", 0, 0);

            worldState.AddWorm(testWorm);

            simulator.InitState(worldState);
            simulator.MakeStep();

            Assert.True(testWorm.X == 0 && testWorm.Y == -1 && testWorm.Health == 99);
        }

        [Test]
        public void MoveToWorm()
        {
            var logStub = new Mock<ISimpleLogger>();
            logStub.Setup(logger => logger.LogInfo(It.IsAny<Exception>(),
                It.IsAny<String>(), It.IsAny<object[]>()));

            WorldSimulator simulator = new WorldSimulator(new FoodGenerator(), logStub.Object);
            Worm testWorm1 = new Worm("Test1", 0, 0);
            Worm testWorm2 = new Worm("Test2", 0, -1);

            var stabBehavior = Mock.Of<IBehavior>(fb =>
                fb.GetAction(testWorm1, It.IsAny<WorldState>()) == new ActionMove(Direction.Left));

            WorldState worldState = new WorldState(new NameGenerator(), stabBehavior);

            worldState.AddWorm(testWorm1);
            worldState.AddWorm(testWorm2);

            simulator.InitState(worldState);
            simulator.MakeStep();

            Assert.True(testWorm1.X == 0 && testWorm1.Y == 0 && testWorm1.Health == 99 &&
                        testWorm2.X == 0 && testWorm2.Y == -1 && testWorm2.Health == 99);
        }

        [Test]
        public void MoveToEat()
        {
            var stabFoodGenerator = Mock.Of<IFoodGenerator>(fb =>
                fb.GenerateFood(It.IsAny<System.Collections.Generic.List<Food>>()) == new Food(0, 0));

            WorldSimulator simulator = new WorldSimulator(stabFoodGenerator, new SimpleFileLogger());

            var stabBehavior = Mock.Of<IBehavior>(fb =>
                fb.GetAction(It.IsAny<Worm>(), It.IsAny<WorldState>()) == new ActionMove(Direction.Left));

            WorldState worldState = new WorldState(new NameGenerator(), stabBehavior);
            Worm testWorm = new Worm("Test", 0, 0);
            worldState.AddWorm(testWorm);

            simulator.InitState(worldState);
            simulator.MakeStep();

            Assert.True(testWorm.X == 0 && testWorm.Y == -1 && testWorm.Health == 109);
        }

        [Test]
        public void ReproductionSuccess()
        {
            var logStub = new Mock<ISimpleLogger>();
            logStub.Setup(logger => logger.LogInfo(It.IsAny<Exception>(),
                It.IsAny<String>(), It.IsAny<object[]>()));

            WorldSimulator simulator = new WorldSimulator(new FoodGenerator(), logStub.Object);

            var stabBehavior = Mock.Of<IBehavior>(fb =>
                fb.GetAction(It.IsAny<Worm>(), It.IsAny<WorldState>()) == new ActionReproduction(Direction.Left));

            WorldState worldState = new WorldState(new NameGenerator(), stabBehavior);
            Worm testWorm = new Worm("Test", 0, 0);
            worldState.AddWorm(testWorm);

            simulator.InitState(worldState);
            simulator.MakeStep();

            Assert.True(testWorm.X == 0 && testWorm.Y == 0 && testWorm.Health == 89 && worldState.Worms.Count == 2 &&
                        worldState.Worms[1].X == 0 && worldState.Worms[1].Y == -1 && worldState.Worms[1].Health == 10);
        }

        [Test]
        public void ReproductionField()
        {
            var logStub = new Mock<ISimpleLogger>();
            logStub.Setup(logger => logger.LogInfo(It.IsAny<Exception>(),
                It.IsAny<String>(), It.IsAny<object[]>()));

            WorldSimulator simulator = new WorldSimulator(new FoodGenerator(), logStub.Object);

            Worm testWorm1 = new Worm("Test", 0, 0);
            Worm testWorm2 = new Worm("Test", 0, -1);

            var stabBehavior = Mock.Of<IBehavior>(fb =>
                fb.GetAction(testWorm1, It.IsAny<WorldState>()) == new ActionReproduction(Direction.Left));

            WorldState worldState = new WorldState(new NameGenerator(), stabBehavior);
            worldState.AddWorm(testWorm1);
            worldState.AddWorm(testWorm2);

            simulator.InitState(worldState);
            simulator.MakeStep();

            Assert.True(testWorm1.X == 0 && testWorm1.Y == 0 && testWorm1.Health == 99 && worldState.Worms.Count == 2);
        }
    }
}