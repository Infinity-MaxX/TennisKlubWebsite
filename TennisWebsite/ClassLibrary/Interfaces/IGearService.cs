using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Models;
using TennisWebsite.ClassLibrary.Models;
namespace TennisWebsite.ClassLibrary.Interfaces;
public interface IGearService
{
    public int Count { get; }
    public bool Status { get; set; }
    public void AddGear(string name, string? description);
    public void BookGear(int bookingID, int gearID);
    //public void BorrowGear(int id);
    public void CheckStatus(int id);
    public void DeleteGear(int id);
    public List<Gear> GetAll();
    public Gear GetGear(int id);
    //public void ReturnGear(int id);
}