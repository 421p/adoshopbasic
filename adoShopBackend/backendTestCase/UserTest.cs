using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Entity.User;
using NUnit.Framework;

namespace backendTestCase
{
    [TestFixture]
    class UserTest
    {
        [Test]
        public void it_should_not_be_able_to_set_a_wrong_role()
        {
            Assert.Throws<Exception>(() =>
            {
                var user = new User
                {
                    Name = "Alina",
                    Password = "Alina",
                    Role = "Wrong role"
                };
            });
        }
    }
}
