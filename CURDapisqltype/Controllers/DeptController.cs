using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;

namespace CURDapisqltype.Controllers
{

    [ApiController]
    public class DeptController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DeptController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("api/Dept")]
        [HttpGet]
        public JsonResult Get()
        {

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand("Select public.tst_dates_func()", myCon))
                {
                   // myCommand.CommandType = CommandType.StoredProcedure;
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);
        }

        [Route("api/Dept")]
        [HttpPost]
        public JsonResult Post(Department dep)
        {
            // done using Execute nonquery without data table

            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon")))
            {

                connection.Open();
                NpgsqlCommand myCommand = new NpgsqlCommand("call save_department(:d_name)", connection);
                myCommand.CommandType = CommandType.Text; //don't set stored procedure
                myCommand.Parameters.AddWithValue("d_name", dep.DepartmentName);
                myCommand.ExecuteNonQuery();
                connection.Close();
            }

            return new JsonResult("Added Successfully");
        }


        [Route("api/Dept")]
        [HttpPut]
        public JsonResult Put(Department dep)
        {


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand("call update_department(:d_name, :d_id)", myCon))
                {
                    myCommand.CommandType = CommandType.Text; //don't set stored procedure
                    myCommand.Parameters.AddWithValue("d_id", dep.DepartmentId);
                    myCommand.Parameters.AddWithValue("d_name", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Updated Successfully");
        }


        [Route("api/Dept/{id}")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
      
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand("call delete_department(:d_id)", myCon))
                {
                    myCommand.CommandType = CommandType.Text; //don't set stored procedure
                    myCommand.Parameters.AddWithValue("d_id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
 }

