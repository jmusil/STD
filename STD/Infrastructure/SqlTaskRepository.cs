using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using STD.Infrastructure;
using STD.Models;
using System.Data;
using System.Data.SqlClient;


namespace STD.Infrastructure
{
    public class SqlTaskRepository : ITaskRepository
    {
        private string myConnectionString = ConfigurationManager.ConnectionStrings["TaskDBContext"].ConnectionString;
        private SqlConnection con = null;

        public SqlTaskRepository()
        {
            con = new SqlConnection(myConnectionString);
        }

        public IEnumerable<Task> GetTasks()
        {
            List<Task> result = new List<Task>();
            SqlCommand cmd = null;
            DataTable table = new DataTable();

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tasks";

            try
            {
                con.Open();
                SqlDataAdapter da = null;
                using (da = new SqlDataAdapter(cmd))
                {
                    da.Fill(table);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            result = (from DataRow row in table.Rows
                      select new Task
                      {
                          Id = Convert.ToInt32(row["Id"]),
                          Title = row["Title"].ToString(),
                          Description = row["Description"].ToString(),
                          Note = row["Note"].ToString(),
                          DueDate = Convert.ToDateTime(row["DueDate"]),
                          Finished = Convert.ToBoolean(row["Finished"]),
                          UserName = row["UserName"].ToString()
                      }).ToList();
            return result;
        }

        public Task GetTaskById(int id)
        {
            Task result = new Task();
            SqlCommand cmd = null;
            SqlParameter param = new SqlParameter("@id", id);
            DataTable table = new DataTable();

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tasks WHERE id=@id";
            cmd.Parameters.Add(param);

            try
            {
                con.Open();
                SqlDataAdapter da = null;
                using (da = new SqlDataAdapter(cmd))
                {
                    da.Fill(table);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            result = (from DataRow row in table.Rows
                      select new Task
                      {
                          Id = Convert.ToInt32(row["Id"]),
                          Title = row["Title"].ToString(),
                          Description = row["Description"].ToString(),
                          Note = row["Note"].ToString(),
                          DueDate = Convert.ToDateTime(row["DueDate"]),
                          Finished = Convert.ToBoolean(row["Finished"]),
                          UserName = row["UserName"].ToString()
                      }).SingleOrDefault();
            return result;
        }

        public void FinishTaskById(int id)
        {
            int res = 0; //return value from sql
            SqlCommand cmd = null;
            SqlParameter param = new SqlParameter("@id", id);

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE tasks SET Finished = 'True' WHERE id = @id";
            cmd.Parameters.Add(param);

            try
            {
                con.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        public void ReopenTaskById(int id)
        {
            int res = 0; //return value from sql
            SqlCommand cmd = null;
            SqlParameter param = new SqlParameter("@id", id);

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE tasks SET Finished = 'False' WHERE id = @id";
            cmd.Parameters.Add(param);

            try
            {
                con.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        public void InsertTask(Task task)
        {
            int res = 0; //return value from sql
            SqlCommand cmd = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@title", task.Title ?? ""),
                new SqlParameter("@description", task.Description ?? ""),
                new SqlParameter("@note", task.Note ?? ""),
                new SqlParameter("@finished", task.Finished),
                new SqlParameter("@date", task.DueDate),
                new SqlParameter("@user", task.UserName ?? "")
            };
            
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO tasks VALUES ( @user, @title, @description, @note, @date, @finished)";
            cmd.Parameters.AddRange(parameters);
            
            try
            {
                con.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

        }

        public void EditTask(Task task)
        {
            int res = 0; //return value from sql
            SqlCommand cmd = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter( "@id", task.Id),
                new SqlParameter("@title", task.Title ?? ""),
                new SqlParameter("@description", task.Description ?? ""),
                new SqlParameter("@note", task.Note ?? ""),
                new SqlParameter("@finished", task.Finished),
                new SqlParameter("@date", task.DueDate),
                new SqlParameter("@user", task.UserName ?? "")
            };
            
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE tasks SET UserName = @user, Title = @title, Description = @description, Note = @note, DueDate = @date, Finished = @finished WHERE id = @id";
            cmd.Parameters.AddRange(parameters);

            try
            {
                con.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            { 

                throw;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        public void DeleteTaskById(int id)
        {
            int res = 0; //return value from sql
            SqlCommand cmd = null;
            SqlParameter paramId = new SqlParameter("@id", id);

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM tasks WHERE id = @id";
            cmd.Parameters.Add(paramId);

            try
            {
                con.Open();
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
    }
}
