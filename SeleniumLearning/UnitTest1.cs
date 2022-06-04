namespace SeleniumLearning
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Initializing SetUp");
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Finishing");
        }
    }
}