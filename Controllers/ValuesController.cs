using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace APIS_practise.Controllers
{

    public  class Employees
    {
        public int EMP_NO { get; set; }
        public string Employee_name { get; set; }
        public string Emp_JOb { get; set; }
        public string Emp_Hireddate { get; set; }

        public static SqlConnection conn()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SVRM5KI;Initial Catalog=SQLTest;User ID=sa;Password=12345");
            con.Open();
            return con;
        }

    }

    public class ValuesController : ApiController
    {
        // GET api/values
        public List<Employees> Get()
        {

            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SVRM5KI;Initial Catalog=SQLTest;User ID=sa;Password=12345");
            //con.Open();
            Employees.conn();
            string q = "select * from emp";
            SqlCommand cmd = new SqlCommand(q,Employees.conn() );
            SqlDataReader rd = cmd.ExecuteReader();
            List<Employees> l = new List<Employees>();
            foreach (var item in rd)
            {
                Employees ep = new Employees();
                l.Add(new Employees { EMP_NO = Convert.ToInt32(rd["empno"]), Employee_name = rd["ename"].ToString(), Emp_JOb = rd["job"].ToString(), Emp_Hireddate = rd["hiredate"].ToString() });
            }
            return l;

        }

        // GET api/values/5
        public List<Employees> Get(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SVRM5KI;Initial Catalog=SQLTest;User ID=sa;Password=12345");
            con.Open();
            string q = "select * from emp where empno='"+id+"'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader rd = cmd.ExecuteReader();
            List<Employees> l = new List<Employees>();
            foreach (var item in rd)
            {
                Employees ep = new Employees();
                l.Add(new Employees { EMP_NO = Convert.ToInt32(rd["empno"]), Employee_name = rd["ename"].ToString(), Emp_JOb = rd["job"].ToString(), Emp_Hireddate = rd["hiredate"].ToString() });
            }
            return l;
        }

        // POST api/values
        public string Post([FromBody] Employees value)
        {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SVRM5KI;Initial Catalog=SQLTest;User ID=sa;Password=12345");
            con.Open();
            string q = "insert into emp(empno,ename,job,hiredate) values(@empno,@ename,@job,@hiredate)";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@empno",Convert.ToString(value.EMP_NO));
            cmd.Parameters.AddWithValue("@ename",Convert.ToString(value.Employee_name));
            cmd.Parameters.AddWithValue("@job" , Convert.ToString(value.Emp_JOb));
            cmd.Parameters.AddWithValue("@hiredate" ,Convert.ToString(value.Emp_Hireddate));
            cmd.ExecuteNonQuery();
            return "Completed Task";
        }

        // PUT api/values/5
        public string Put(int id, [FromBody] Employees value)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SVRM5KI;Initial Catalog=SQLTest;User ID=sa;Password=12345");
            con.Open();
            string q = "update emp set ename='"+value.Employee_name+"',job='"+value.Emp_JOb+"',hiredate='"+value.Emp_Hireddate+"'  where empno='"+id+"'";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();
            return "Update Completed";
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SVRM5KI;Initial Catalog=SQLTest;User ID=sa;Password=12345");
            //con.Open();
            string q = "delete from emp where empno='"+id+"'";
            SqlCommand cmd = new SqlCommand(q, Employees.conn());
            cmd.ExecuteNonQuery();
            return "Delete Completed";
        }
    }
}
