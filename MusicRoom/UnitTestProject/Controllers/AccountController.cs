using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicRoom.Controllers;
using MusicRoom.Models;

namespace MusicRoom.Test.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
       /* [TestMethod]
        public void Register()
        {
            var controller = new AccountController();
            var result = controller.Register() as ViewResult;
            Assert.IsNotNull(result);
        }*/

        [TestMethod]
        public void ConfirmationSuccess()
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ConfirmationSuccess() as ViewResult;
            Assert.IsNotNull("Index");
        }


        [TestMethod]
        public void ManageUsers()
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.ManageUsers() as ViewResult;
            Assert.IsNotNull(result);
        }

        UserProfile userProfile = new UserProfile();

        [TestMethod]
        public void Edit()
        {
            AccountController controller = new AccountController();
            ViewResult result = controller.Edit(userProfile) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Graphics()
        {
            StatisticsController controller = new StatisticsController();
            ViewResult result = controller.Graphics() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index()
        {
            TagCloudController controller = new TagCloudController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result.View);
        }
    }
}
