using TennisLibrary.Services;
using TennisLibrary.Models;
using TennisWebsite.ClassLibrary.Services;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using TennisLibrary;
namespace TennisTest
{
    [TestClass]
    public class UnitTest1
    {

        public async Task SetupAsync()
        {
            ConnectionManager.TestMode();
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {

                await connection.OpenAsync();

                SqlCommand deleteTennisAttendees = new SqlCommand("DELETE FROM TennisAttendees", connection);
                SqlCommand deleteTennisOrderLine = new SqlCommand("DELETE FROM TennisOrderline", connection);
                SqlCommand deleteTennisBooking = new SqlCommand("DELETE FROM TennisBooking", connection);
                SqlCommand deleteTennisBlog = new SqlCommand("DELETE FROM TennisBlog", connection);
                SqlCommand deleteTennisEvent = new SqlCommand("DELETE FROM TennisEvent", connection);
                SqlCommand deleteTennisOrder = new SqlCommand("DELETE FROM TennisOrder", connection);
                SqlCommand deleteTennisBookedGear = new SqlCommand("DELETE FROM TennisBookedGear", connection);
                SqlCommand deleteTennisUser = new SqlCommand("DELETE FROM TennisUser", connection);
                SqlCommand deleteTennisCourt = new SqlCommand("DELETE FROM TennisCourt", connection);
                SqlCommand deleteTennisPurchaseable = new SqlCommand("DELETE FROM TennisPurchaseable", connection);
                SqlCommand deleteTennisGear = new SqlCommand("DELETE FROM TennisGear", connection);

                await deleteTennisAttendees.ExecuteNonQueryAsync();
                await deleteTennisOrderLine.ExecuteNonQueryAsync();
                await deleteTennisBooking.ExecuteNonQueryAsync();
                await deleteTennisBlog.ExecuteNonQueryAsync();
                await deleteTennisEvent.ExecuteNonQueryAsync();
                await deleteTennisOrder.ExecuteNonQueryAsync();
                await deleteTennisBookedGear.ExecuteNonQueryAsync();
                await deleteTennisUser.ExecuteNonQueryAsync();
                await deleteTennisCourt.ExecuteNonQueryAsync();
                await deleteTennisPurchaseable.ExecuteNonQueryAsync();
                await deleteTennisGear.ExecuteNonQueryAsync();
            }
        }

        [TestMethod]
        public async Task TestSetupAsync()
        {
            await Assert.ThrowsExceptionAsync<SqlException>(new Func<Task>(SetupAsync));
        }

        [TestMethod]
        public async Task TestCreateUserAsync()
        {
            await SetupAsync();
            UserService us = new UserService();
            User user = new User("", "Bob", 'F', "BobMarley", "69696969", "Bob@Bob.Bob", "Bob Street", "Bobby", new DateOnly());
            await us.AddUserAsync(user, "USSR");

            List<User> list = await us.GetAllUsersAsync();
            Assert.IsTrue( list.Count == 1);
        }
    }
}