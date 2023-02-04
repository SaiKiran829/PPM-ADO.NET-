using System;
using PPM.DAL;
using UserInterface;
namespace PPM.Main;
public static class Program
{
    public static void Main(string[] args)
    {
        OpenDB start = new OpenDB();
        start.StartApp();
        UI view = new UI();
        view.View();
    }
}