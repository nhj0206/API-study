using Company.Models;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System;
using System.Data;

namespace Company.Services
{
    public static class DepartmentService
    {
        static List<Department> departments {get; }
        static string connString;
        static NpgsqlConnection sqlConn;
        
        public static List<Department> GetAll()
        {
            sqlConn = new NpgsqlConnection(connString);
            sqlConn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = sqlConn;
            cmd.CommandText = "SELECT departmentid, departmentname FROM department";

            var reader = cmd.ExecuteReader();
            List<Department> departresult = new List<Department>();

            while(reader.Read())
            {
                Department dp = new Department();
                dp.DepartmentId = reader["departmentid"].ToString();
                dp.DepartmentName = reader["departmentname"].ToString();

                departresult.Add(dp);
            }

            reader.Close();
            sqlConn.Close();
            return departresult;
        }

        public static List<Department> Get(int id)
        {
            sqlConn = new NpgsqlConnection(connString);
            sqlConn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = sqlConn;
            cmd.CommandText = "SELECT departmentid, departmentname FROM department WHERE departmentid = @V1";
            cmd.Parameters.AddWithValue("@V1", id);

            var reader = cmd.ExecuteReader();
            List<Department> departresult = new List<Department>();

            while(reader.Read())
            {
                Department dp = new Department();
                dp.DepartmentId = reader["departmentid"].ToString();
                dp.DepartmentName = reader["departmentname"].ToString();

                departresult.Add(dp);
            }
            reader.Close();
            sqlConn.Close();
            return departresult;
        }

        public static void Add(Department department)
        {
            sqlConn = new NpgsqlConnection(connString);
            sqlConn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = sqlConn;
            cmd.CommandText = "Insert Into department(departmentname) VALUES(@V2);";
            // cmd.Parameters.AddWithValue("@V1", department.DepartmentId); // departmentId 쪽에서오류가 발생하여 insert 시에 serial로 설정된 departmentid는 건들지 않음.
            cmd.Parameters.AddWithValue("@V2", department.DepartmentName);
            cmd.ExecuteNonQuery();
            
            sqlConn.Close();
            return;
        }

        public static void Update(Department department)
        {
            sqlConn = new NpgsqlConnection(connString);
            sqlConn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = sqlConn;
            cmd.CommandText = "UPDATE department SET departmentname = @V2 WHERE departmentid = @V1;";
            cmd.Parameters.AddWithValue("@V1", Convert.ToInt32(department.DepartmentId));
            cmd.Parameters.AddWithValue("@V2", department.DepartmentName);
            cmd.ExecuteNonQuery();

            sqlConn.Close();
            return;
        }

        public static void Delete(int id)
        {
            sqlConn = new NpgsqlConnection(connString);
            sqlConn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = sqlConn;
            cmd.CommandText = "DELETE FROM department WHERE departmentid = @V1;";
            cmd.Parameters.AddWithValue("@V1", id);
            cmd.ExecuteNonQuery();

            sqlConn.Close();
            return;
        }    
    }
}