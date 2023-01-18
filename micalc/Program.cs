using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static Dictionary<string, string> salt = new Dictionary<string, string>()
{
{ "r1d", "A2E371B0-B34B-48A5-8C40-A7133F3B5D88" },
{ "others", "d44fb0960aa0-a5e6-4a30-250f-6d2df50a" }
};

    static string GetSalt(string sn)
    {
        if (!sn.Contains("/"))
        {
            return salt["r1d"];
        }

        var parts = salt["others"].Split("-");
        Array.Reverse(parts);
        return string.Join("-", parts);
    }

    static string CalcPasswd(string sn)
    {
        var passwd = sn + GetSalt(sn);
        using (var md5 = MD5.Create())
        {
            var bytes = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(passwd));
            return BitConverter.ToString(bytes).Replace("-", "").Substring(0, 8);
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Xiaomi Router SSH Password Calculator");
        Console.Write("Enter the Serial Number: ");
        var serial = Console.ReadLine();

        Console.WriteLine("\nSSH Username: root");
        Console.WriteLine("SSH Password: " + CalcPasswd(serial).ToLower());

        Console.ReadKey();
    }
}