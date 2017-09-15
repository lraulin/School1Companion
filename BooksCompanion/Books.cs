using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BooksCompanion
{
    public class Books : Dictionary<int, Book>
    {
        public Books()
        {

        }

        public Books(string sCnxn, string sLogPath)
        {
            try
            {
                SqlConnection oCnxn = new SqlConnection(sCnxn);

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spBookInfoFetchAll";

                oCnxn.Open();
                SqlDataReader oReader = oCmd.ExecuteReader();

                while (oReader.Read())
                {
                    Book oNewBook = new Book();
                    oNewBook.DateCreated = oReader["DateCreated"].ToString();
                    oNewBook.BookTitle = oReader["BookTitle"].ToString();
                    oNewBook.AuthorName = oReader["AuthorName"].ToString();
                    oNewBook.BookID = Convert.ToInt32(oReader["BookID"]);
                    //if (!this.ContainsKey(oNewBook.BookID))
                    this.Add(oNewBook.BookID, oNewBook);
                }

                oCnxn.Close();
            }
            catch (Exception ex)
            {
                Log oLog = new Log();
                oLog.LogError("BooksConstructor", ex.Message, sLogPath);
            }
        }

        public Books(string sCnxn, string sLogPath, int iSearchID)
        {
            try
            {
                SqlConnection oCnxn = new SqlConnection(sCnxn);

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spBookInfoFetchAll";

                oCnxn.Open();
                SqlDataReader oReader = oCmd.ExecuteReader();

                while (oReader.Read())
                {
                    Book oNewBook = new Book();
                    oNewBook.DateCreated = oReader["DateCreated"].ToString();
                    oNewBook.BookTitle = oReader["BookTitle"].ToString();
                    oNewBook.AuthorName = oReader["AuthorName"].ToString();
                    oNewBook.BookID = Convert.ToInt32(oReader["BookID"]);
                    if (oNewBook.BookID == iSearchID)
                        this.Add(oNewBook.BookID, oNewBook);
                }

                oCnxn.Close();
            }
            catch (Exception ex)
            {
                Log oLog = new Log();
                oLog.LogError("BooksConstructor", ex.Message, sLogPath);
            }
        }

        public DataTable BooksList(string sCnxn, string sLogPath)
        {
            try
            {
                List<Book> oBooks = new List<Book>();
                Dictionary<int, Book> oBooksNew = new Dictionary<int, Book>();

                SqlConnection oCnxn = new SqlConnection(sCnxn);

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spBookInfoFetchAll";

                DataTable dtBook = new DataTable();
                SqlDataAdapter daBook = new SqlDataAdapter();
                daBook.SelectCommand = oCmd;

                oCnxn.Open();
                daBook.SelectCommand.ExecuteNonQuery();
                daBook.Fill(dtBook);
                oCnxn.Close();

                return (dtBook);
            }
            catch (Exception ex)
            {
                Log oLog = new Log();
                oLog.LogError("BookList", ex.Message, sLogPath);
                return (null);
            }
        }
    }

    public class Book
    {
        // ex12a: Create properties with get/set for each field in tblBooks
        #region Properties
        private int _BookID;
        private string _BookTitle;
        private string _AuthorName;
        private int _Length;
        private bool _IsOnAmazon;
        private string _DateCreated;

        public int BookID
        {
            get
            {
                return this._BookID;
            }

            set
            {
                this._BookID = value;
            }
        }

        public string BookTitle
        {
            get
            {
                return this._BookTitle;
            }

            set
            {
                this._BookTitle = value;
            }
        }

        public string AuthorName
        {
            get
            {
                return this._AuthorName;
            }

            set
            {
                this._AuthorName = value;
            }
        }

        public int Length
        {
            get
            {
                return this._Length;
            }

            set
            {
                this._Length = value;
            }
        }

        public bool IsOnAmazon
        {
            get
            {
                return this._IsOnAmazon;
            }

            set
            {
                this._IsOnAmazon = value;
            }
        }

        public string DateCreated
        {
            get
            {
                return this._DateCreated;
            }

            set
            {
                this._DateCreated = value;
            }
        }
        #endregion Properties

        public Book()
        {

        }
        
        public Book(string sCnxn, string sLogPath, int FetchID)
        {
            // ex12b: Create a constructor to fetch ONE book record by ID
            SqlConnection oCnxn = new SqlConnection(sCnxn);

            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oCnxn;
            oCmd.CommandText = "spBookSearchID";
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.Parameters.Add("@BookID", SqlDbType.Int).Value = this._BookID;

            oCnxn.Open();
            SqlDataReader oReader = oCmd.ExecuteReader();

            while (oReader.Read())
            {
                if (FetchID == Convert.ToInt32(oReader["BookID"]))
                {
                    this._DateCreated = oReader["DateCreated"].ToString();
                    this._BookTitle = oReader["BookTitle"].ToString();
                    this._AuthorName = oReader["AuthorName"].ToString();
                    this._BookID = Convert.ToInt32(oReader["BookID"]);
                    this._Length = Convert.ToInt32(oReader["Length"]);
                    this._IsOnAmazon = Convert.ToBoolean(oReader["IsOnAmazon"]);
                }
            }

            oCnxn.Close();

        }

        public void SaveBook(string sCnxn, string sLogPath)
        {
            try
            {
                SqlConnection oCnxn = new SqlConnection(sCnxn);

                SqlCommand oCmd = new SqlCommand();
                oCmd.Connection = oCnxn;
                oCmd.CommandText = "spBookInfoSave";
                oCmd.CommandType = CommandType.StoredProcedure;

                //oCmd.Parameters["@BookTitle"].Value = this._BookTitle;
                //oCmd.Parameters["@AuthorName"].Value = this._AuthorName;
                //oCmd.Parameters["@Length"].Value = this._Length;
                //oCmd.Parameters["@IsOnAmazon"].Value = this._IsOnAmazon;
                //oCmd.Parameters["@BookID"].Value = this._BookID;

                //oCmd.Parameters.AddWithValue("@BookTitle", this._BookTitle);
                //oCmd.Parameters.AddWithValue("@AuthorName", this._AuthorName);
                //oCmd.Parameters.AddWithValue("@Length", this._Length);
                //oCmd.Parameters.AddWithValue("@IsOnAmazon", this._IsOnAmazon);
                //oCmd.Parameters.AddWithValue("@BookID", this._BookID);

                oCmd.Parameters.Add("@BookTitle", SqlDbType.NVarChar, 50).Value = this._BookTitle;
                oCmd.Parameters.Add("@AuthorName", SqlDbType.NVarChar, 50).Value = this._AuthorName;
                oCmd.Parameters.Add("@Length", SqlDbType.Int).Value = this._Length;
                oCmd.Parameters.Add("@IsOnAmazon", SqlDbType.Bit).Value = this._IsOnAmazon;
                oCmd.Parameters.Add("@BookID", SqlDbType.Int).Value = this._BookID;

                oCnxn.Open();
                oCmd.ExecuteNonQuery();
                oCnxn.Close();
            }
            catch (Exception ex)
            {
                Log oLog = new Log();
                oLog.LogError("BookList", ex.Message, sLogPath);
            }
        }
    }
}
