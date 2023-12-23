using DataBases.Models;
using DataBases.Services;

namespace ServerTest
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : PageTest
    {
        
        [SetUp]
        public void SetUp()
        {
            using (ChatContext chatContext = new ChatContext())
            {
                chatContext.Messages.RemoveRange(chatContext.Messages);
                chatContext.Users.RemoveRange(chatContext.Users);

                chatContext.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (ChatContext chatContext = new ChatContext())
            {
                chatContext.Messages.RemoveRange(chatContext.Messages);
                chatContext.Users.RemoveRange(chatContext.Users);

                chatContext.SaveChanges();
            }
        }
        [Test]
        public async Task ServerTest()
        {
            var mock = new MockMessageSource();

            var serv = new Server(mock);
            
            mock.AddServer(serv);

            await serv.Start();

            using (var ctx = new ChatContext())
            {
                Assert.IsTrue(ctx.Users.Count() == 2, "Users not created");

                var user1 = ctx.Users.FirstOrDefault(x => x.Fullname == "Ilya");
                var user2 = ctx.Users.FirstOrDefault(x => x.Fullname == "Irina");

                Assert.IsNotNull(user1, "User not created");
                Assert.IsNotNull(user2, "User not created");

                Assert.IsTrue(user1.MessagesFrom.Count == 1);
                Assert.IsTrue(user2.MessagesFrom.Count == 1);

                Assert.IsTrue(user1.MessagesTo.Count == 1);
                Assert.IsTrue(user2.MessagesTo.Count == 1);

                var msg1 = ctx.Messages.FirstOrDefault(x => x.SenderId  == user1 && x.RecieverId == user2);
                var msg2 = ctx.Messages.FirstOrDefault(x => x.SenderId == user2 && x.RecieverId == user1);

                Assert.AreEqual("Irina's message", msg2.Text);
                Assert.AreEqual("Ilya's message", msg1.Text);
            }







        }
    }
}
