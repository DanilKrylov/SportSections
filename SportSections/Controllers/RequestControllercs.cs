using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SportSections.ViewModels;
using System.Collections.Generic;

namespace SportSections.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult Report(string sqlString)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=sports_db;Trusted_Connection=True;";
            if (sqlString is null)
            {
                sqlString = "";
            }
            sqlString = sqlString.Trim();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand();
                    command.CommandText = sqlString;
                    command.Connection = connection;
                    var result = command.ExecuteReader();

                    var countOfRows = result.FieldCount;
                    var viewResult = new QueryViewModel()
                    {
                        ColumnNames = new string[countOfRows],
                        InfoSets = new List<string[]>()
                    };

                    for (int i = 0; i < countOfRows; i++)
                    {
                        viewResult.ColumnNames[i] = result.GetName(i);
                    }

                    var count = 0;
                    while (result.Read())
                    {
                        for (int i = 0; i < countOfRows; i++)
                        {
                            if (i == 0)
                            {
                                viewResult.InfoSets.Add(new string[countOfRows]);
                            }

                            viewResult.InfoSets[count][i] = result.GetValue(i).ToString();
                        }

                        count++;
                    }
                    viewResult.SqlQuery = sqlString;

                    return View(viewResult);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult DoRequest(string sqlString)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=sports_db;Trusted_Connection=True;";
            if (sqlString is null)
            {
                sqlString = "";
            }
            sqlString = sqlString.Trim();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand();
                    command.CommandText = sqlString;
                    command.Connection = connection;
                    var result = command.ExecuteReader();

                    var countOfRows = result.FieldCount;
                    var viewResult = new QueryViewModel()
                    {
                        ColumnNames = new string[countOfRows],
                        InfoSets = new List<string[]>()
                    };

                    for (int i = 0; i < countOfRows; i++)
                    {
                        viewResult.ColumnNames[i] = result.GetName(i);
                    }

                    var count = 0;
                    while (result.Read())
                    {
                        for (int i = 0; i < countOfRows; i++)
                        {
                            if (i == 0)
                            {
                                viewResult.InfoSets.Add(new string[countOfRows]);
                            }

                            viewResult.InfoSets[count][i] = result.GetValue(i).ToString();
                        }

                        count++;
                    }
                    viewResult.SqlQuery = sqlString;

                    return View(viewResult);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
