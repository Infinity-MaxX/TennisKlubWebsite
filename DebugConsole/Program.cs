using TennisLibrary.Helpers;
using TennisLibrary.Services;
using TennisLibrary.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        UserService testService = new UserService();
        await testService.AddUserAsync(new User("imagePath", "TestMann", 'G', "Testuzer", "phone", "Mail@Nail.aa", "House", "Place", new DateOnly(1864, 12, 1), AccessLevel.Guest), "Passwoooord");
    }
}