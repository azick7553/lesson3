using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using lesson3.ViewModel;
namespace lesson3
{
    class Program
    {
        //[Obsolete]
        static void Main(string[] args)
        {
            List<int> numberArr = new List<int>(){ 1, 2, 3, 4, 5, 6, 7, -1, -4 };

            var result = numberArr.All(p => p > 0);
            var result1 = numberArr.All(p => p != 0);
            var result2 = numberArr.Any(p => p > 0);

            

            using (var context = new AlifAcademyContext())
            {
                var temp = context.Models.Select(p=> new { Nom = p.ModelName, Aydi = p.Id }).ToList();
                var oneItem = context.Models.Where(p => p.ModelName == "A33333").FirstOrDefault();

                var oneItem1 = context.Models.Where(p => p.ModelName == "A33333").SingleOrDefault();


                var list = (from model in context.Models
                           select model).ToList();

                var newList = from list1 in list
                              where list1.Id == 3
                              select list1;
                //var compList = context.Company.ToList();

                //var compList = from comp in context.Company
                //               where comp.CompanyName == "Apple"
                //               select comp;

                //var compList = from comp in context.Company
                //               select new CompanyViewModel
                //               {
                //                   Id = comp.Id,
                //                   Name = comp.CompanyName
                //               };

                //var modelList = from model in context.Models
                //                join company in context.Company
                //                on model.CompanyId equals company.Id
                //                select new ModelViewModel
                //                {
                //                    Id = model.Id,
                //                    ModelName = model.ModelName,
                //                    ModelComapany = company.CompanyName
                //                };

                //var modelList = from model in context.Models
                //                join company in context.Company
                //                on model.CompanyId equals company.Id into companyDefault
                //                from modelDefault in companyDefault.DefaultIfEmpty()
                //                select new ModelViewModel
                //                {
                //                    Id = model.Id,
                //                    ModelName = model.ModelName,
                //                    ModelComapany = modelDefault.CompanyName
                //                };
                //var modelList = context.Models

                //.Take(10)
                //.Skip(3)

                /*
                 SELECT M.ID, C.COMPANYNAME, M.MODELNAME FROM MODEL M 
                 JOIN COMPANY C ON M.COMPANY.ID = C.ID

                 
                 */
                //int pageNumber = 1;
                //int itemPerPage = 5;
                //while (true)
                //{

                //    var modelList = (from model in context.Models
                //                    join company in context.Company
                //                    on model.CompanyId equals company.Id into companyDefault
                //                    from modelDefault in companyDefault.DefaultIfEmpty()
                //                    select new ModelViewModel
                //                    {
                //                        Id = model.Id,
                //                        ModelName = model.ModelName,
                //                        ModelComapany = modelDefault.CompanyName
                //                    }).Skip(itemPerPage * (pageNumber-1)).Take(itemPerPage);

                //    foreach (var model in modelList)
                //    {
                //        Console.WriteLine($"ID:{model.Id}\tModel Name:{model.ModelName}\tCompany Name:{model.ModelComapany}");
                //    }
                //        Console.WriteLine($"Current page:{pageNumber}");
                //    Console.ReadKey();
                //    pageNumber++;
                //}
                //var modelList = (from model in context.Models
                //                 join company in context.Company
                //                 on model.CompanyId equals company.Id into companyDefault
                //                 from modelDefault in companyDefault.DefaultIfEmpty()
                //                 select new ModelViewModel
                //                 {
                //                     Id = model.Id,
                //                     ModelName = model.ModelName,
                //                     ModelComapany = modelDefault.CompanyName
                //                 }).ToList().GroupBy(p=>p.ModelComapany);
                //foreach(var groupedModel in modelList)
                //{
                //    Console.ForegroundColor = ConsoleColor.Green;
                //    Console.WriteLine(groupedModel.Key);
                //    Console.ResetColor();
                //    foreach (var item in groupedModel)
                //    {
                //        if (item.ModelComapany == groupedModel.Key)
                //        {
                //            Console.WriteLine($"\tID:{item.Id}\tModel Name{item.ModelName}\tCompany Name:{item.ModelComapany}");
                //        }
                //    }
                //}


            }

            //numberArr = numberArr.Where(number => number % 2 == 0).ToArray();

            //numberArr = (from temp in numberArr 
            //             where temp % 2 == 0
            //             select temp).ToArray();
            //Console.ReadKey();
            //using(var context = new AlifAcademyContext())
            //{
            //    var modelList = context.Models.Include(i => i.Company).ToList();

            //}


            //while (true)
            //{
            //    Console.Clear();
            //    Console.Write("1.Create\n2.Read\n3.Update\n4.Delete\nChoice:");
            //    var choice = Console.ReadLine();
            //    switch (choice)
            //    {
            //        case "1": Create(); break;
            //        case "2": Read(); break;
            //        case "3": Update(); break;
            //        case "4": Delete(); break;
            //        default:
            //            break;
            //    }
            //}
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
