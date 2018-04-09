using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace School1Companion
{
    public class Users : Dictionary<int, User>
    {
        public Users()
        {
            
        }

        public Users(string sCnxn, string sLogPath)
        {
            try
            {
                SqlConnection oCnxn = new SqlConnection(sCnxn);

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spUserInfoFetchAll";

                oCnxn.Open();
                SqlDataReader oReader = oCmd.ExecuteReader();

                while (oReader.Read())
                {
                    User oNewUser = new User();
                    oNewUser.UserID = Convert.ToInt32(oReader["UserID"]);
                    oNewUser.FirstName = oReader["FirstName"].ToString();
                    oNewUser.LastName = oReader["LastName"].ToString();
                    oNewUser.CreatedDate = oReader["CreatedDate"].ToString();
                    if (!this.ContainsKey(oNewUser.UserID))
                        this.Add(oNewUser.UserID, oNewUser);
                }

                oCnxn.Close();
            }
            catch (Exception ex)
            {
                Log.LogError("UsersConstructor", ex.Message, sLogPath);
            }
        }

        public DataTable UsersList(string sCnxn, string sLogPath)
        {
            try
            {
                List<User> oUsers = new List<User>();
                Dictionary<int, User> oUsersNew = new Dictionary<int, User>();

                SqlConnection oCnxn = new SqlConnection(sCnxn);

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spUserInfoFetchAll";

                DataTable dtUser = new DataTable();
                SqlDataAdapter daUser = new SqlDataAdapter();
                daUser.SelectCommand = oCmd;

                oCnxn.Open();
                daUser.SelectCommand.ExecuteNonQuery();
                daUser.Fill(dtUser);
                oCnxn.Close();

                return (dtUser);
            }
            catch (Exception ex)
            {
                Log.LogError("UserList", ex.Message, sLogPath);
                return (null);
            }
        }
    }

    public class User
    {
        #region Properties
        private int _UserID;
        private string _FirstName;
        private string _LastName;
        private string _CreatedDate;

        public int UserID 
        {
            get { return (this._UserID); }

            set { this._UserID = value; }
        }

        public string FirstName
        {
            get { return (this._FirstName); }

            set { this._FirstName = value; }
        }

        public string LastName
        {
            get { return (this._LastName); }

            set { this._LastName = value; }
        }

        public string CreatedDate
        {
            get { return (this._CreatedDate); }

            set { this._CreatedDate = value; }
        }
        #endregion Properties

        public User()
        {
            
        }

        public User(int UserID, string CnxnString, string LogPath)
        {
            try
            {
                SqlConnection oCnxn = new SqlConnection(CnxnString);
                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spUserInfoFetch";

                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.VarChar, 50));
                oCmd.Parameters["@UserID"].Value = UserID;

                oCnxn.Open();
                SqlDataReader oReader = oCmd.ExecuteReader();
                while (oReader.Read())
                {
                    User oNewUser = new User();
                    this.UserID = Convert.ToInt32(oReader["UserID"]);
                    this.FirstName = oReader["FirstName"].ToString();
                    this.LastName = oReader["LastName"].ToString();
                    this.CreatedDate = oReader["CreatedDate"].ToString();
                }

                oCnxn.Close();
            }
            catch (Exception ex)
            {
                Log.LogError("UsersConstructor", ex.Message, LogPath);
            }
        }
    }

}
