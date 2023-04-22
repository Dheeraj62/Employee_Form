using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employee_Form.Models;

namespace Employee_Form.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly SqlConnection _connection;

        public EmployeeController()
        {
            string connectionString = @"Data Source=DESKTOP-KR133AS;Initial Catalog=Employee_DB;Integrated Security=True;";
            _connection = new SqlConnection(connectionString);

        }

        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("spGetEmployees", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return View(dt);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel emp)
        {
            using (SqlCommand cmd = new SqlCommand("spInsertEmployee", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@EmpDOB", emp.EmpDOB);
                cmd.Parameters.AddWithValue("@EmpDOJ", emp.EmpDOJ);
                cmd.Parameters.AddWithValue("@EmpSalary", emp.EmpSalary);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            EmployeeModel emp = new EmployeeModel();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM EmployeeManagement WHERE EmpID=@EmpId", _connection))
            {
                cmd.Parameters.AddWithValue("@EmpId", id);
                _connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    emp.EmpID = Convert.ToInt32(rdr["EmpID"]);
                    emp.EmpName = rdr["EmpName"].ToString();
                    emp.EmpDOB = Convert.ToDateTime(rdr["EmpDOB"]);
                    emp.EmpDOJ = Convert.ToDateTime(rdr["EmpDOJ"]);
                    emp.EmpSalary = Convert.ToDecimal(rdr["EmpSalary"]);
                }
                _connection.Close();
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModel emp)
        {
            using (SqlCommand cmd = new SqlCommand("UpdateEmployee", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", emp.EmpID);
                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@EmpDOB", emp.EmpDOB);
                cmd.Parameters.AddWithValue("@EmpDOJ", emp.EmpDOJ);
                cmd.Parameters.AddWithValue("@EmpSalary", emp.EmpSalary);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (SqlCommand cmd = new SqlCommand("DeleteEmployee", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", id);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
