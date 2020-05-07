using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace lesson3
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {

            using(var context = new AlifAcademyContext())
            {
                var modelList = context.Models.Include(i => i.Company).ToList();

            }


            while (true)
            {
                Console.Clear();
                Console.Write("1.Create\n2.Read\n3.Update\n4.Delete\nChoice:");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Create(); break;
                    case "2": Read(); break;
                    case "3": Update(); break;
                    case "4": Delete(); break;
                    default:
                        break;
                }
            }
        }
        private static void Delete()
        {
            try
            {
                using (var context = new AlifAcademyContext())
                {
                    Read("update");
                    Console.WriteLine("Please select");
                    Console.Write("ID:");
                    var companyId = Convert.ToInt32(Console.ReadLine());
                    var company = context.Company.Find(companyId);

                    if (company != null)
                    {
                        Console.Write("Are you sure? Y(yes)/N(no):");
                        var confirm = Console.ReadLine();
                        if (confirm.ToUpper() == "Y") context.Company.Remove(company);

                        if (context.SaveChanges() > 0)
                        {
                            SuccessMessage("Company deleted!");
                        }
                        else
                        {
                            FailMessage("Company not deleted!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FailMessage(ex.Message);
            }
            finally
            {
                ConsoleReadWithPressKeyMessage();
            }
        }
        private static void Update()
        {
            try
            {
                using (var context = new AlifAcademyContext())
                {
                    Read("update");
                    Console.WriteLine("Please select");
                    Console.Write("ID:");
                    var companyId = Convert.ToInt32(Console.ReadLine());
                    var company = context.Company.Find(companyId);

                    //SELECT * FROM COMPANY WHERE ID = companyId

                    if (company != null)
                    {
                        Console.Write("New company name:");
                        var newCompanyName = Console.ReadLine();
                        company.CompanyName = newCompanyName;
                        if (context.SaveChanges() > 0)
                        {
                            SuccessMessage("Company changed!");
                        }
                        else
                        {
                            FailMessage("Company not changed!");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                FailMessage(ex.Message);
            }
            finally
            {
                ConsoleReadWithPressKeyMessage();
            }
        }
        private static void Read(string type = null)
        {
            try
            {
                //using (var workContext = new AlifAcademyContext())
                using (var context = new AlifAcademyContext())
                {
                    var companyList = context.Company.ToList();

                    companyList.ForEach(p =>
                    {
                        Console.WriteLine($"ID:{p.Id}\tCompanyName:{p.CompanyName}");
                    });
                }
            }
            catch (Exception ex)
            {
                FailMessage(ex.Message);
            }
            finally
            {
                if (type != "update")
                {
                    ConsoleReadWithPressKeyMessage();
                }
            }

        }
        private static void Create()
        {
            try
            {
                using (var context = new AlifAcademyContext())
                {
                    Console.Write("Enter new comapny name:");
                    var companyName = Console.ReadLine();
                    Company comp = new Company()
                    {
                        CompanyName = companyName
                    };
                    context.Company.Add(comp);

                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        SuccessMessage("Add company");
                    }
                }
            }
            catch (Exception ex)
            {
                FailMessage(ex.Message);
            }
            finally
            {
                ConsoleReadWithPressKeyMessage();
            }
        }
        private static void ConsoleReadWithPressKeyMessage()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private static void FailMessage(string failText)
        {
            Console.WriteLine($"Fail: {failText}");
        }
        private static void SuccessMessage(string text)
        {
            Console.WriteLine($"Success: {text}");
        }
    }
}
